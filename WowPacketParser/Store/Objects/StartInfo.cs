using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class StartInfo
    {
        public Race Race;

        public Class Class;

        public StartPos StartPos;

        public List<StartAction> StartActions; 

        public List<StartSpell> StartSpells;

        public List<StartItem> StartItems;
    }
}
