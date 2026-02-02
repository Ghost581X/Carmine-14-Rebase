using Content.Shared.Trigger.Components.Triggers;
using Content.Shared.Weapons.Hitscan.Events;

namespace Content.Shared.Trigger.Systems;

public sealed class TriggerOnHitscanSystem : TriggerOnXSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<TriggerOnHitscanHitComponent, HitscanRaycastFiredEvent>(OnHit);
        SubscribeLocalEvent<TriggerOnHitscanFiredComponent, HitscanRaycastFiredEvent>(OnFired);
    }

    private void OnHit(Entity<TriggerOnHitscanHitComponent> ent, ref HitscanRaycastFiredEvent args)
    {
        if (args.HitEntity == null) //CARMINE: MONOPORT: transplanted hitscan system, no longer stored in .Data
            return;

        Trigger.Trigger(ent.Owner, args.HitEntity, ent.Comp.KeyOut); //CARMINE: MONOPORT: transplanted hitscan system, no longer stored in .Data
    }

    private void OnFired(Entity<TriggerOnHitscanFiredComponent> ent, ref HitscanRaycastFiredEvent args)
    {
        Trigger.Trigger(ent.Owner, args.Shooter, ent.Comp.KeyOut); //CARMINE: MONOPORT: transplanted hitscan system, no longer stored in .Data
    }
}
