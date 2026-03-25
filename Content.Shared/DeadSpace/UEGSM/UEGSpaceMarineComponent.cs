using Content.Shared.StatusIcon;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.DeadSpace.UEGSM;

/// <summary>
/// This is used for tagging a mob as a space marine.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class UEGSpaceMarineComponent : Component
{
    /// <summary>
    /// The status/faction icon for UEG Space Marines
    /// </summary>
    [DataField("uegsmStatusIcon", customTypeSerializer: typeof(PrototypeIdSerializer<FactionIconPrototype>))]
    public string UEGSMStatusIcon = "UEGSMFaction";
}
