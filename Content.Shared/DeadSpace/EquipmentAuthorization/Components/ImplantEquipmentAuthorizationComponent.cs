using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Content.Shared.Tools;

namespace Content.Shared.DeadSpace.EquipmentAuthorization.Components;

[RegisterComponent, AutoGenerateComponentState]
public sealed partial class ImplantEquipmentAuthorizationComponent : Component
{

    [DataField, AutoNetworkedField]
    public EntityUid? OwnerImplantUid;

    /// <summary>
    /// Used for implanters that start with specific implants
    /// </summary>
    [DataField("implant", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string Implant = "EquipmentAuthorizationImplant";

    [DataField]
    public bool IsLocked = false;

    /// <summary>
    /// Amount of times in seconds it takes to reset lock.
    /// </summary>
    [DataField]
    public TimeSpan ResetImpLockDelay = TimeSpan.FromSeconds(1);

    /// <summary>
    /// The tool quality needed to reset lock.
    /// </summary>
    [DataField]
    public ProtoId<ToolQualityPrototype> ResetImpLockTool = "ResetImpLock";
}
