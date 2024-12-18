using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.Substructures
{
    public static class PerksProgramHandler
    {
        public static void ReadPerksVendorItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadInt32("VendorItemID", indexes);
            packet.ReadInt32("MountID", indexes);
            packet.ReadInt32("BattlePetSpeciesID", indexes);
            packet.ReadInt32("TransmogSetID", indexes);
            packet.ReadInt32("ItemModifiedAppearanceID", indexes);
            packet.ReadInt32("TransmogIllusionID", indexes);
            packet.ReadInt32("ToyID", indexes);
            packet.ReadInt32("Price", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_7_58123))
                packet.ReadInt32("OriginalPrice", indexes);
            packet.ReadTime64("AvailableUntil", indexes);
            packet.ReadBit("Disabled", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_5_57171))
                packet.ReadBit("DoesNotExpire", indexes);
        }
    }
}
