using Robust.Shared.GameStates;

namespace Content.Shared.DeadSpace.Implants.Radiance.Components;

/// <summary>
/// Component given to an entity to mark it is a radiance implant.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class RadianceImplantComponent : Component;
