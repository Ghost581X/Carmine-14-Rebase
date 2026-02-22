using System.Numerics;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Array;

namespace Content.Server.Worldgen.Prototypes;

/// <summary>
///     This is a prototype for biome selection, allowing the component list of a chunk to be amended based on the output
///     of noise channels at that location.
/// </summary>
[Prototype("worldgenSpaceBiome")] // carmine - rename to differentiate from ambientspacebiome
public sealed partial class BiomePrototype : IPrototype, IInheritingPrototype
{
    /// <inheritdoc />
    [ParentDataField(typeof(AbstractPrototypeIdArraySerializer<BiomePrototype>))]
    public string[]? Parents { get; private set; }

    /// <inheritdoc />
    [NeverPushInheritance]
    [AbstractDataField]
    public bool Abstract { get; private set; }

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; private set; } = default!;

    /// <summary>
    ///     The valid ranges of noise values under which this biome can be picked.
    /// </summary>
    [DataField("noiseRanges", required: true)]
    public Dictionary<string, List<Vector2>> NoiseRanges = default!;

    /// <summary>
    ///     Higher priority biomes get picked before lower priority ones.
    /// </summary>
    [DataField("priority", required: true)]
    public int Priority { get; private set; }

    /// <summary>
    ///     The components that get added to the target map.
    /// </summary>
    [DataField("chunkComponents")]
    [AlwaysPushInheritance]
    public ComponentRegistry ChunkComponents = new();

    // EE - min/max biome ranges begin
    /// <summary>
    ///     Minimum X coordinate (IN CHUNKS, *128) value to spawn this biome.
    /// </summary>
    [DataField]
    public int? MinX;

    /// <summary>
    ///     Minimum Y coordinate (IN CHUNKS, *128) value to spawn this biome.
    /// </summary>
    [DataField]
    public int? MinY;

    /// <summary>
    ///     Maximum X coordinate (IN CHUNKS, *128) value to spawn this biome.
    /// </summary>
    [DataField]
    public int? MaxX;

    /// <summary>
    ///     Maximum Y coordinate (IN CHUNKS, *128) value to spawn this biome.
    /// </summary>
    [DataField]
    public int? MaxY;
    // EE - min/max biome ranges end

    // Frontier: distances begin
    /// <summary>
    ///     The valid range of biome coordinate lengths (3000, 4000) => 5000
    ///     Chunks with center points within this range may be generated with this biome.
    /// </summary>

    [ViewVariables]
    public Vector2? DistanceRangeSquared { get; private set; }
    // Frontier: distances end

    //TODO: Get someone to make this a method on componentregistry that does it Correctly.
    /// <summary>
    ///     Applies the worldgen config to the given target (presumably a map.)
    /// </summary>
    public void Apply(EntityUid target, ISerializationManager serialization, IEntityManager entityManager)
    {
        // Add all components required by the prototype. Engine update for this whenst.
        foreach (var data in ChunkComponents.Values)
        {
            var comp = (Component) serialization.CreateCopy(data.Component, notNullableOverride: true);
            entityManager.AddComponent(target, comp);
        }
    }
}

