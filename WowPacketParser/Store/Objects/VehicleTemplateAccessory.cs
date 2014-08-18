﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("vehicle_template_accessory")]
    public class VehicleTemplateAccessory
    {
        
        [DBFieldName("accessory_entry")]
        public uint accessoryEntry;

        [DBFieldName("seat_id")]
        public int seatId;
    }
}
