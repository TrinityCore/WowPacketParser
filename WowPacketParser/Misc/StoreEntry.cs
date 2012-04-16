using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public struct StoreEntry
    {
        public StoreNameType _type;
        public int _data;
        public StoreEntry(StoreNameType type, int Data)
        {
            _type = type;
            _data = Data;
        }

        public override string ToString()
        {
            return StoreGetters.GetName(_type, _data, true);
        }
    }
}
