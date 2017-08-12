﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public class VehicleTemplateAccessory : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("accessory_entry")]
        public uint? AccessoryEntry;

        [DBFieldName("seat_id", true)]
        public int? SeatId;

        [DBFieldName("minion")]
        public bool? IsMinion;

        [DBFieldName("description")]
        public string Description;

        [DBFieldName("summontype")]
        public uint? SummonType;

        [DBFieldName("summontimer")]
        public uint? SummonTimer;
    }
}