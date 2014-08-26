﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("vehicle_template_accessory")]
    public class VehicleTemplateAccessory
    {
        [DBFieldName("accessory_entry")]
        public uint AccessoryEntry;

        [DBFieldName("seat_id")]
        public int SeatId;
    }
}
