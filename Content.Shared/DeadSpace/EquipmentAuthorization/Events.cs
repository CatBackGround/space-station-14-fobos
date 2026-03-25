using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared.DeadSpace.EquipmentAuthorization
{
    [Serializable, NetSerializable]
    public sealed partial class ResetImpLockDoAfterEvent : SimpleDoAfterEvent
    {
    }
}
