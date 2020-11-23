using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class SocketedGem : ISocketedGem
    {
        public int ItemID { get; set; }
        public ushort[] BonusListIDs { get; } = new ushort[16];
        public byte Context { get; set; }
    }
}

