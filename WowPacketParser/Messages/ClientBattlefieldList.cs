using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlefieldList
    {
        public byte MaxLevel;
        public bool PvpAnywhere;
        public ulong BattlemasterGuid;
        public bool IsRandomBG;
        public byte MinLevel;
        public bool HasHolidayWinToday;
        public int BattlemasterListID;
        public bool HasRandomWinToday;
        public List<int> Battlefields;
    }
}
