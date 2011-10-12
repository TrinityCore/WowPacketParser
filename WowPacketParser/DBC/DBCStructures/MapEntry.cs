using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.DBCStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MapEntry
    {
        public uint ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private readonly uint[] _InternalName;
        public uint MapType;
        public uint Unk1;
        public uint Unk2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _MapName;
        public uint MapNameFlag;
        public uint LinkedZone;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _HordeIntro;
        public uint HordeIntroFlag;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = DBCStore.DBC.MaxDBCLocale)]
        private readonly uint[] _AllianceIntro;
        public uint AllianceIntroFlag;
        public uint MultiMapId;
        public uint Unk3;
        public uint EntranceMap;
        public float EntranceX;
        public float EntranceY;
        public uint Unk4;
        public uint RequiredAddon;
        public uint UnkTime;
        public uint MaxPlayers;

        public string GetMapName()
        {
            return MapName;
        }

        private string MapName
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_MapName[0], out aux);
                return aux;
            }
        }

        public string HordeIntro
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_HordeIntro[0], out aux);
                return aux;
            }
        }

        public string AllianceIntro
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_AllianceIntro[0], out aux);
                return aux;
            }
        }

        public string InternalName
        {
            get
            {
                var aux = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_InternalName[0], out aux);
                return aux;
            }
        }
    }
}
