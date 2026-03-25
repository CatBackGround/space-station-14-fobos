using Content.Shared.DeadSpace.Implants.Radiance.Components;
using Content.Shared.DeadSpace.Overlays;
using Content.Shared.StatusIcon.Components;
using Robust.Shared.Prototypes;
using Content.Client.Overlays;

namespace Content.Client.DeadSpace.Overlays;

public sealed class ShowRadianceIconsSystem : EquipmentHudSystem<ShowRadianceIconsComponent>
{
    [Dependency] private readonly IPrototypeManager _prototype = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RadianceComponent, GetStatusIconsEvent>(OnGetStatusIconsEvent);
    }

    private void OnGetStatusIconsEvent(EntityUid uid, RadianceComponent component, ref GetStatusIconsEvent ev)
    {
        if (!IsActive)
            return;

        if (_prototype.Resolve(component.RadianceStatusIcon, out var iconPrototype))
            ev.StatusIcons.Add(iconPrototype);
    }
}
