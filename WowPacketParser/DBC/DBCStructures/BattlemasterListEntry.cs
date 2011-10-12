using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.DBCStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BattlemasterListEntry
    {
        public uint ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Map;
        public uint Type;
        public uint AllowedToJoinAsGroup;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _Name;
        public uint NameFlag;
        public uint MaxSize;
        public uint HolidayWorldState;
        public uint MinLevel;
        public uint MaxLevel;

        public string GetName()
        {
            return Name;
        }

        private string Name
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.BattlemasterListStrings.TryGetValue(_Name[0], out aux);
                return aux;
            }
        }
    }
}
