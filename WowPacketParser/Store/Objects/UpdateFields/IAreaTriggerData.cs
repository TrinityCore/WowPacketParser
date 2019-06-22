using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerData
    {
        int SpellID { get; }
        uint TimeToTarget { get; }
        uint TimeToTargetScale { get; }
        uint DecalPropertiesID { get; }
    }
}
