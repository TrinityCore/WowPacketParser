using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class StartInfo
    {
        public Race Race; // TODO Remove

        public Class Class; // TODO Remove

        public StartPos StartPos;

        public List<StartAction> StartActions; 

        public List<StartSpell> StartSpells;

        public List<StartItem> StartItems;
    }
}
