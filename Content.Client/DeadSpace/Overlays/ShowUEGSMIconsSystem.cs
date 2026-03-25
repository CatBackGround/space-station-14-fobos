using Content.Shared.DeadSpace.Overlays;
using Content.Shared.DeadSpace.UEGSM;
using Content.Shared.StatusIcon;
using Content.Shared.StatusIcon.Components;
using Robust.Shared.Prototypes;
using Content.Client.Overlays;

namespace Content.Client.DeadSpace.Overlays;

public sealed class ShowUEGSMIconsSystem : EquipmentHudSystem<ShowUEGSMIconsComponent>
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<UEGSpaceMarineComponent, GetStatusIconsEvent>(OnGetStatusIconsEvent);
    }

    private void OnGetStatusIconsEvent(EntityUid uid, UEGSpaceMarineComponent component, ref GetStatusIconsEvent ev)
    {
        if (!IsActive)
            return;

        if (_prototype.TryIndex<FactionIconPrototype>(component.UEGSMStatusIcon, out var iconPrototype))
            ev.StatusIcons.Add(iconPrototype);
    }
}
