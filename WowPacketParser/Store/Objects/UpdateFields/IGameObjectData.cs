using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IGameObjectData
    {
        WowGuid CreatedBy { get; }
        uint Flags { get; }
        Quaternion ParentRotation { get; }
        int FactionTemplate { get; }
        sbyte State { get; }
        sbyte TypeID { get; }
        byte PercentHealth { get; }
    }
}
