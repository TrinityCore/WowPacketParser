using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.DBCStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LFGDungeonsEntry
    {
        public uint ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Name;
        public uint NameFlag;
        public uint MininumLevel;
        public uint MaxinumLevel;
        public uint RecommendedLevel;
        public uint RecommendedMinimumLevel;
        public uint RecommendedMaxinumLevel;
        public uint Map;
        public uint Difficulty;
        public uint Flags;
        public uint Type;
        public uint Unk1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly uint[] _IconName;
        public uint Expansion;
        public uint Unk2;
        public uint GroupType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Description;
        public uint DescriptionFlags;

        public string GetName()
        {
            return Name;
        }

        private string Name
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.LFGDungeonsStrings.TryGetValue(_Name[0], out aux);
                return aux;
            }
        }

        public string IconName
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.LFGDungeonsStrings.TryGetValue(_IconName[0], out aux);
                return aux;
            }
        }

        public string Description
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.LFGDungeonsStrings.TryGetValue(_Description[0], out aux);
                return aux;
            }
        }
    }
}
