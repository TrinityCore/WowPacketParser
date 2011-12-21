using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class SniffData
    {
        public SniffFileInfo FileInfo { get; set; }

        public double TimeStamp { get; set; }

        public StoreNameType ObjectType { get; set; }

        public int Id { get; set; }

        public String Data { get; set; }

        public int Number { get; set; }
    }
}