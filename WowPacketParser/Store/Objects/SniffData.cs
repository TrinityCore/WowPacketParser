using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class SniffData : ICloneable
    {
        public SniffFileInfo FileInfo = new SniffFileInfo();

        public double TimeStamp;

        public StoreNameType ObjectType = StoreNameType.None;

        public int Id = 0;

        public String Data = string.Empty;

        public int Number = 0;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
