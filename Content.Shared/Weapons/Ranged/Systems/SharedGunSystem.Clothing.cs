using Content.Shared.Containers;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Events;

namespace Content.Shared.Weapons.Ranged.Systems;

public partial class SharedGunSystem
{
    private void InitializeClothing()
    {
        SubscribeLocalEvent<ClothingSlotAmmoProviderComponent, TakeAmmoEvent>(OnClothingTakeAmmo);
        SubscribeLocalEvent<ClothingSlotAmmoProviderComponent, CheckShootPrototypeEvent>(OnClothingCheckProto); // Mono
        SubscribeLocalEvent<ClothingSlotAmmoProviderComponent, GetAmmoCountEvent>(OnClothingAmmoCount);
    }

    private void OnClothingTakeAmmo(EntityUid uid, ClothingSlotAmmoProviderComponent component, TakeAmmoEvent args)
    {
        var getConnectedContainerEvent = new GetConnectedContainerEvent();
        RaiseLocalEvent(uid, ref getConnectedContainerEvent);
        if(!getConnectedContainerEvent.ContainerEntity.HasValue)
            return;

        RaiseLocalEvent(getConnectedContainerEvent.ContainerEntity.Value, args);
    }

    private void OnClothingAmmoCount(EntityUid uid, ClothingSlotAmmoProviderComponent component, ref GetAmmoCountEvent args)
    {
        var getConnectedContainerEvent = new GetConnectedContainerEvent();
        RaiseLocalEvent(uid, ref getConnectedContainerEvent);
        if (!getConnectedContainerEvent.ContainerEntity.HasValue)
            return;

        RaiseLocalEvent(getConnectedContainerEvent.ContainerEntity.Value, ref args);
    }
    // Mono
    private void OnClothingCheckProto(Entity<ClothingSlotAmmoProviderComponent> ent, ref CheckShootPrototypeEvent args)
    {
        return; //this is used for vacuum cleaners only and we don't care // .2 | 2026 for carmine
    }
}
