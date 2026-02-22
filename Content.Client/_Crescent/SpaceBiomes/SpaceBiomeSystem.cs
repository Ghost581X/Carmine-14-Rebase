using Content.Shared._Crescent.SpaceBiomes;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Robust.Client.Player;
using Robust.Client.GameObjects;
using Content.Shared.Shuttles.Components;
using Content.Client.Salvage;
using System.Numerics;
using Content.Client.EarcutNet;
using System.Linq;
using Robust.Shared.Toolshed.TypeParsers;
using Content.Client.Parallax;
using Content.Shared.Parallax;
using Content.Client._Carmine.Parallax;

namespace Content.Client._Crescent.SpaceBiomes;

public sealed class SpaceBiomeSystem : EntitySystem
{
    [Dependency] private readonly IPlayerManager _playerMan = default!;
    [Dependency] private readonly IPrototypeManager _protMan = default!;
    [Dependency] private readonly TransformSystem _formSys = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly ParallaxSystem _parallaxSys = default!;
    [Dependency] private readonly SharedMapSystem _mapSys = default!;

    private float _updTimer;
    private const float UpdateInterval = 0.5f; // in seconds - how often the checks for this system run

    private ProtoId<SpaceBiomePrototype>? _cachedBiomeProto;
    private EntityUid? _cachedGrid;
    private EntityUid? _cachedMap;

    public override void Initialize()
    {
        base.Initialize();
        //carmine edit - we dont have these enabled right now so i'm ghonna leave em commented until we do, cuz we might never have ftl or salvage
        // SubscribeLocalEvent<FTLMapComponent, SpaceBiomeMapChangeMessage>(OnFTLMapChanged);
        // SubscribeLocalEvent<SalvageExpeditionComponent, SpaceBiomeMapChangeMessage>(OnSalvageMapChanged);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (!_timing.IsFirstTimePredicted) //otherwise this will tick like 5x faster on client. thanks prediction
            return;
        //update timers
        _updTimer += frameTime;
        if (_updTimer < UpdateInterval)
            return;
        _updTimer -= UpdateInterval;

        // 0. grab the local player ent
        if (_playerMan.LocalEntity == null) //this should never be null i thinky
            return;

        var localPlayerUid = _playerMan.LocalEntity.Value;
        var xform = Transform(localPlayerUid);
        //var ourCoord = xform.Coordinates; //HAHA THIS IS ACTUALLY GRID COORDS (RELATIVE TO PARENT), NOT WORLD COORDS!! FOR BIOME PROCESSING!!!! FUCK!!!!!!!
        var ourCoord = _formSys.GetWorldPosition(xform);
        // 1. grab the local grid, if any. if not, send msg signalling we entered space
        var newGrid = xform.GridUid;

        if (newGrid != _cachedGrid) //if true, we have changed grids since last update
        {
            _cachedGrid = newGrid;
            var message = new PlayerParentChangedMessage(newGrid); //if this is null it notifies that we're in space
            RaiseLocalEvent(localPlayerUid, ref message, true);

        }
        // 2. query all spacebiomes present & check if we are PointInPolygon for that biome's vertexes
        var biomeProtos = _protMan.EnumeratePrototypes<SpaceBiomePrototype>();
        ProtoId<SpaceBiomePrototype>? newBiomeProto = null;
        foreach (var biome in biomeProtos)
        {
            if (biome.VertexCoordinates.Count == 0) //if the biome has no coords then we don't bother doing anything
                continue;

            if (PointInPolygon(new Vector2(ourCoord.X, ourCoord.Y), biome.VertexCoordinates))
            {
                newBiomeProto = biome.ID;
            }
        }
        // 3. check the mapid and check if its different than the cached mapid from the last update
        EntityUid? newMap = _formSys.GetMap(localPlayerUid);
        // 4. this is the actual checking bit
        // if the map changed then it cant be the same source from last update, so we do _cachedSource = newSource anyway.
        if (_cachedMap != newMap || _cachedBiomeProto != newBiomeProto)
        {
            var mapSwapMsg = new SpaceBiomeMapChangeMessage();
            if (newMap != null) //if the new map is null then :godo: we are borked anyway
            {
                RaiseLocalEvent((EntityUid)newMap, ref mapSwapMsg, true);
            }
            _cachedMap = newMap;
            _cachedBiomeProto = newBiomeProto;
            SpaceBiomePrototype biome;
            if (mapSwapMsg.Biome != null)
                biome = _protMan.Index<SpaceBiomePrototype>(mapSwapMsg.Biome);
            else
                biome = _protMan.Index<SpaceBiomePrototype>(newBiomeProto ?? "BiomeDefault");
            //note: this is where the parallax should swap. eventually.
            var biomeSwapMsg = new SpaceBiomeSwapMessage(biome);
            Logger.Info("LOCAL PLAYER UID: " + localPlayerUid.ToString());
            _parallaxSys.SwapParallax(localPlayerUid, EnsureComp<SpaceBiomeParallaxComponent>(localPlayerUid), biome.Parallax, biome.SwapDuration); //carmine edit
            RaiseLocalEvent(localPlayerUid, ref biomeSwapMsg, true);

        }
    }

    //taken from https://www.geeksforgeeks.org/dsa/how-to-check-if-a-given-point-lies-inside-a-polygon/
    // this algorithm checks if your point is inside the listed polygon
    // it does this by effectively checking "does it fit within the Y range of the polygon"
    // if so, then take our point and draw an infinitely long line to the right.
    // if our point passes a polygon's EDGE, then flip the "are we inside" boolean
    // go to that link and itll make alot more sense.
    private bool PointInPolygon(Vector2 point, List<Vector2> polygon)
    {
        int numVertices = polygon.Count;
        double x = point.X, y = point.Y;
        bool inside = false;

        // Store the first Vector2 in the polygon and initialize the second Vector2
        Vector2 p1 = polygon[0], p2;

        // Loop through each edge in the polygon
        for (int i = 1; i <= numVertices; i++)
        {
            // Get the next Vector2 in the polygon
            p2 = polygon[i % numVertices];

            // Check if the Vector2 is above the minimum y coordinate of the edge
            if (y > Math.Min(p1.Y, p2.Y))
            {
                // Check if the Vector2 is below the maximum y coordinate of the edge
                if (y <= Math.Max(p1.Y, p2.Y))
                {
                    // Check if the Vector2 is to the left of the maximum x coordinate of the edge
                    if (x <= Math.Max(p1.X, p2.X))
                    {
                        // Calculate the x-intersection of the line connecting the Vector2 to the edge
                        double xIntersection = (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;

                        // Check if the Vector2 is on the same line as the edge or to the left of the x-intersection
                        if (p1.X == p2.X || x <= xIntersection)
                        {
                            // Flip the inside flag
                            inside = !inside;
                        }
                    }
                }
            }

            // Store the current Vector2 as the first Vector2 for the next iteration
            p1 = p2;
        }

        // Return the value of the inside flag
        return inside;
    }

    //carmine edit - we dont have these enabled right now so i'm ghonna leave em commented until we do, cuz we might never have ftl or salvage
    // private void OnFTLMapChanged(Entity<FTLMapComponent> ent, ref SpaceBiomeMapChangeMessage args)
    // {
    //     args.Biome = ent.Comp.Biome;
    // }

    // private void OnSalvageMapChanged(Entity<SalvageExpeditionComponent> ent, ref SpaceBiomeMapChangeMessage args)
    // {
    //     args.Biome = ent.Comp.Biome;
    // }
}
