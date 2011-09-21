using System;
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

        /// <summary>
        /// Returns the formated spell name with rank (if it exists)
        /// </summary>
        public string GetMapName()
        {
            return MapName;
        }

        private string MapName
        {
            get
            {
                var name = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_MapName[0], out name);
                return name;
            }
        }

        public string HordeIntro
        {
            get
            {
                var intro = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_HordeIntro[0], out intro);
                return intro;
            }
        }

        public string AllianceIntro
        {
            get
            {
                var intro = string.Empty;
                DBCStore.DBC.MapStrings.TryGetValue(_AllianceIntro[0], out intro);
                return intro;
            }
        }
    }
}
