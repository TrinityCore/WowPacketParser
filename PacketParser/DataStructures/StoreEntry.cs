using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PacketParser.Enums;
using PacketParser.Processing;

namespace PacketParser.DataStructures
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
            return PacketFileProcessor.Current.GetProcessor<NameStore>().GetName(_type, _data, true);
        }

        public static implicit operator Int32(StoreEntry e)
        {
            return e._data;
        }

        public static implicit operator UInt32(StoreEntry e)
        {
            return (UInt32)e._data;
        }
    }
}
