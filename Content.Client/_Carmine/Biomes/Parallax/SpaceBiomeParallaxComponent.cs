using JetBrains.Annotations;
using Robust.Shared.GameStates;

namespace Content.Client._Carmine.Parallax;

/// <summary>
/// Handles clientside parallax for travelling between space biomes.
/// This is ensurecomp'd and queried by SpaceBiomeSystem
/// THIS IS APPLIED TO PLAYER'S LOCALENTITY ON THE CLIENT, NOT ON MAPS OR ANYTHING ELSE!!!
/// </summary>
[RegisterComponent]
public sealed partial class SpaceBiomeParallaxComponent : Component
{
    [DataField]
    public string Parallax = "Default";
    //for smooth change between old and new parallax
    [DataField]
    public string? SwappedParallax;

    [ViewVariables(VVAccess.ReadWrite)]
    public float SwapDuration; //in seconds

    [ViewVariables(VVAccess.ReadWrite)]
    public float SwapTimer;
    [ViewVariables(VVAccess.ReadWrite)]
    public bool IsSwapping => SwappedParallax != null;
}
