using WowPacketParser.DBC.Store;
using WowPacketParser.DBC.Structures;

namespace WowPacketParser.DBC
{
    public class Loader
    {
        public Loader()
        {
            DBC.Store.Main.Spell = Reader.ReadDBC<SpellEntry>(null);
        }
    }
}
