using Content.Shared.Tools.Systems;
using Content.Shared.Implants.Components;
using Content.Shared.DeadSpace.EquipmentAuthorization;
using Content.Shared.DeadSpace.EquipmentAuthorization.Components;
using Content.Shared.Inventory.Events;
using Content.Shared.Inventory;
using Content.Server.Popups;
using Timer = Robust.Shared.Timing.Timer;
using Robust.Shared.Prototypes;
using Content.Shared.Interaction;
using System.Linq;

namespace Content.Server.DeadSpace.EquipmentAuthorization;

public sealed class EquipmentAuthorizationSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventory = default!;
    [Dependency] private readonly PopupSystem _popupSystem = default!;
    [Dependency] private readonly SharedToolSystem _toolSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ImplantEquipmentAuthorizationComponent, GotEquippedEvent>(OnEquipped);
        SubscribeLocalEvent<ImplantEquipmentAuthorizationComponent, InteractUsingEvent>(OnInteractUsing);
        SubscribeLocalEvent<ImplantEquipmentAuthorizationComponent, ResetImpLockDoAfterEvent>(OnResetImpLockDoAfter);
    }

    private IReadOnlyList<EntityUid> GetImplants(EntityUid target)
    {
        if (!TryComp<ImplantedComponent>(target, out var implanted))
            return Array.Empty<EntityUid>();

        return implanted.ImplantContainer.ContainedEntities;
    }

    private bool HasOwnerImplant(EntityUid target, EntityUid? ownerImplantUid)
    {
        if (ownerImplantUid is not { } implantUid)
            return false;

        return GetImplants(target).Contains(implantUid);
    }

    private EntityUid? FirstGetImplant(EntityUid target, EntProtoId implantId)
    {
        var implants = GetImplants(target);

        foreach (var ent in implants)
        {
            var proto = Prototype(ent);
            if (proto is not null && proto == implantId)
                return ent;
        }
        return null;
    }

    private void OnResetImpLockDoAfter(EntityUid uid, ImplantEquipmentAuthorizationComponent component, ResetImpLockDoAfterEvent args)
    {
        if (args.Cancelled)
            return;

        component.IsLocked = false;
        component.OwnerImplantUid = null;

        args.Handled = true;
    }

    private void OnInteractUsing(Entity<ImplantEquipmentAuthorizationComponent> ent, ref InteractUsingEvent args)
    {
        if (!_toolSystem.HasQuality(args.Used, ent.Comp.ResetImpLockTool))
            return;

        if (!_toolSystem.UseTool(
                args.Used,
                args.User,
                ent,
                (float)ent.Comp.ResetImpLockDelay.TotalSeconds,
                ent.Comp.ResetImpLockTool,
                new ResetImpLockDoAfterEvent()))
        {
            return;
        }

        args.Handled = true;
    }

    private void OnEquipped(EntityUid uid, ImplantEquipmentAuthorizationComponent component, GotEquippedEvent args)
    {
        if (!component.IsLocked)
        {
            var implant = FirstGetImplant(args.Equipee, component.Implant);
            if (implant is not { } implantUid)
            {
                Timer.Spawn(0,
                    () =>
                    {
                        _popupSystem.PopupEntity(Loc.GetString("equipment-authorization-no-implant"), args.Equipee, args.Equipee);
                        _inventory.TryUnequip(args.Equipee, args.Slot, true, true);
                    });
                return;
            }

            component.OwnerImplantUid = implantUid;
            component.IsLocked = true;
            return;
        }

        if (HasOwnerImplant(args.Equipee, component.OwnerImplantUid))
        {
            return;
        }
        else
        {
            Timer.Spawn(0,
                () =>
                {
                    _popupSystem.PopupEntity(Loc.GetString("equipment-authorization-denied-wrong-implant"), args.Equipee, args.Equipee);
                    _inventory.TryUnequip(args.Equipee, args.Slot, true, true);
                });
            return;
        }
    }
}
