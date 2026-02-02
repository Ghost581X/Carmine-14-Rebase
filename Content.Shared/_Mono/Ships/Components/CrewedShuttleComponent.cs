// CARMINE: PORT FROM MONOLITH BEGIN
namespace Content.Shared._Mono.Ships.Components;

/// <summary>
/// This is used for knowing whether a shuttle should be forced to be crewed.
/// </summary>
[RegisterComponent]
public sealed partial class CrewedShuttleComponent : Component
{
    [DataField]
    public List<EntityUid> ShuttleConsoles = new();

    [DataField]
    public List<EntityUid> GunneryConsoles = new();
}
// CARMINE: PORT FROM MONOLITH END
