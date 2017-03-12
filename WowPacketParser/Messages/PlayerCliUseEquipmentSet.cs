using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliUseEquipmentSet
    {
        public InvUpdate Inv;
        public EquipmentSetItem[/*19*/] Items;
    }
}
