using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AuthSuccessInfo
    {
        public uint VirtualRealmAddress;
        public List<VirtualRealmInfo> VirtualRealms;
        public uint TimeRemain;
        public uint TimeOptions;
        public uint TimeRested;
        public byte ActiveExpansionLevel;
        public byte AccountExpansionLevel;
        public bool IsExpansionTrial;
        public uint TimeSecondsUntilPCKick;
        public List<RaceClassAvailability> AvailableRaces;
        public List<RaceClassAvailability> AvailableClasses;
        public List<AvailableCharacterTemplateSet> Templates;
        public bool ForceCharacterTemplate;
        public ushort NumPlayersHorde; // Optional
        public ushort NumPlayersAlliance; // Optional
        public bool IsVeteranTrial;
        public uint CurrencyID;
    }
}
