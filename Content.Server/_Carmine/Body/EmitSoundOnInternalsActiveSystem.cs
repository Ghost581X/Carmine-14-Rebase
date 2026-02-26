using Robust.Shared.Audio.Systems;
using Content.Server.Body.Systems;
using Content.Shared.Body.Systems;
using Content.Shared._Carmine.Body;

namespace Content.Server._Carmine.Body;

public sealed class EmitSoundOnInternalsActiveSystem : EntitySystem
{

    [Dependency] private readonly SharedInternalsSystem _internals = default!;

    [Dependency] private readonly SharedAudioSystem _audio = default!;


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EmitSoundOnInternalsActiveComponent, InhaleLocationEvent>(OnInhaleLocation);
    }

    public void OnInhaleLocation(Entity<EmitSoundOnInternalsActiveComponent> ent, ref InhaleLocationEvent args)
    {
        if (_internals.AreInternalsWorking(ent))
            _audio.PlayPvs(ent.Comp.Sound, ent.Owner);
    }

}
