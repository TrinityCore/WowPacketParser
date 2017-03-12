using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLoadEquipmentSet
    {
        public List<EquipmentSetData> SetData;
    }
}
