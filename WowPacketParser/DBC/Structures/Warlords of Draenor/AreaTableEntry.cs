using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class AreaTableEntry
    {
        public uint ID;                                         // 0
        public uint MapID;                                      // 1
        public uint ParentAreaID;                               // 2 if 0 then it's zone, else it's zone id of this area

        //uint      AreaBit;                                    // 3, main index
        //uint      Flags[2];                                   // 4-5,
        //uint      SoundProviderPref;                          // 6,
        //uint      SoundProviderPrefUnderwater;                // 7,
        //uint      AmbienceID;                                 // 8,
        //uint      ZoneMusic;                                  // 9,
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 7)]
        public int[] PlaceHolder1;

        public string ZoneName;                                 // 10 - Internal name

        //uint      IntroSound;                                 // 11
        //uint      ExplorationLevel;                           // 12
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 2)]
        public int[] PlaceHolder2;

        public string AreaName;                                 // 13 - In-game name

        //uint      FactionGroupMask;                           // 14
        //uint      LiquidTypeID[4];                            // 15-18
        //float     AmbientMultiplier;                          // 19
        //uint      MountFlags;                                 // 20
        //uint      UWIntroMusic;                               // 21
        //uint      UWZoneMusic;                                // 22
        //uint      UWAmbience;                                 // 23
        //uint      WorldPvPID;                                 // 24 World_PVP_Area.dbc
        //uint      PvPCombastWorldStateID;                     // 25
        //uint      WildBattlePetLevelMin;                      // 26
        //uint      WildBattlePetLevelMax;                      // 27
        //uint      WindSettingsID;                             // 28
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 15)]
        public int[] PlaceHolder3;
    }
}
