using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class SocketedGem : ISocketedGem
    {
        public int ItemID { get; set; }
        public ushort[] BonusListIDs { get; } = new ushort[16];
        public byte Context { get; set; }
    }
}

