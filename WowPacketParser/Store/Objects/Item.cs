using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("item")]
    public sealed class Item : WoWObject
    {
        [DBFieldName("Class")]
        public ItemClass Class;

        [DBFieldName("SubClass")]
        public uint SubClass;

        [DBFieldName("SoundOverrideSubclass")]
        public int SoundOverrideSubclass;

        [DBFieldName("Material")]
        public Material Material;

        [DBFieldName("InventoryType")]
        public InventoryType InventoryType;

        [DBFieldName("Sheath")]
        public SheathType Sheath;

        [DBFieldName("FileDataID")]
        public uint FileDataID;

        [DBFieldName("GroupSoundsID")]
        public uint GroupSoundsID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
