using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class CorpseData : ICorpseData
    {
        public uint DynamicFlags { get; set; }
        public WowGuid Owner { get; set; }
        public WowGuid PartyGUID { get; set; }
        public WowGuid GuildGUID { get; set; }
        public uint DisplayID { get; set; }
        public uint[] Items { get; } = new uint[19];
        public byte Unused { get; set; }
        public byte RaceID { get; set; }
        public byte Sex { get; set; }
        public byte SkinID { get; set; }
        public byte FaceID { get; set; }
        public byte HairStyleID { get; set; }
        public byte HairColorID { get; set; }
        public byte FacialHairStyleID { get; set; }
        public uint Flags { get; set; }
        public int FactionTemplate { get; set; }
        public byte[] CustomDisplayOption { get; } = new byte[3];
    }
}

