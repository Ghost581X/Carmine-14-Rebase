using Content.Shared.Sound.Components;
using Robust.Shared.GameStates;

namespace Content.Shared._Carmine.Body;

/// <summary>
/// Whenever a <see cref="InhaleLocationEvent"/> and internals are on, play a sound in PVS range.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class EmitSoundOnInternalsActiveComponent : BaseEmitSoundComponent { }

