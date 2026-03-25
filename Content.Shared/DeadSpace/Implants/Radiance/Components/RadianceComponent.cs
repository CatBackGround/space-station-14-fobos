using Robust.Shared.GameStates;
using Content.Shared.StatusIcon;
using Robust.Shared.Prototypes;

namespace Content.Shared.DeadSpace.Implants.Radiance.Components;

/// <summary>
/// Component of special implant for UEG spacemarines.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class RadianceComponent : Component
{
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public ProtoId<SecurityIconPrototype> RadianceStatusIcon = "RadianceIcon";
}
