using Content.Server.Worldgen.Prototypes;
using Content.Shared._Mono.GCQueue;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server._Mono.GCQueue; //carmine: rename namespace

/// <summary>
///     This is used for whether or not a GCable object is "dirty". Firing GCDirtyEvent on the object is the correct way to
///     set this up.
/// </summary>
[RegisterComponent]
[Access(typeof(GCQueueSystem))]
public sealed partial class GCAbleObjectComponent : Component
{
    /// <summary>
    ///     Which queue to insert this object into when GCing
    /// </summary>
    [DataField("queue", required: true, customTypeSerializer: typeof(PrototypeIdSerializer<GCQueuePrototype>))]
    public string Queue = default!;

    [ViewVariables(VVAccess.ReadOnly)]
    [DataField("linkedGridEntity")]
    public EntityUid LinkedGridEntity = EntityUid.Invalid;
}

