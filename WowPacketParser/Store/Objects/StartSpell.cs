using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class StartSpell
    {
        private uint _spell;

        public uint Spell
        {
            get { return _spell; }
            set
            {
                _spell = value;
                SpellName = StoreGetters.GetName(StoreNameType.Spell, (int)value);
            }
        }

        public string SpellName;
    }
}
