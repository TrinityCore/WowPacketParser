using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class SniffData
    {
        public SniffFileInfo FileInfo = new SniffFileInfo();

        public StoreNameType ObjectType = StoreNameType.None;

        public String Data1 = string.Empty;

        public String Data2 = string.Empty;
    }
}
