
namespace WowPacketParser.Store.Objects
{
    public enum BagSlotFlags : uint
    {
        None                = 0x00,
        DisableAutoSort     = 0x01,
        PriorityEquipment   = 0x02,
        PriorityConsumables = 0x04,
        PriorityTradeGoods  = 0x08,
        PriorityJunk        = 0x10,
        PriorityQuestItems  = 0x20,
        ExcludeJunkSell     = 0x40,
    }
}
