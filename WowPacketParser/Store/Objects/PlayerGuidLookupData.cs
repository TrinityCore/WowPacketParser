using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public record PlayerGuidLookupData
    {
        public Race Race;
        public Gender Gender;
        public Class Class;
        public byte Level;
        public string Name;
    }
}
