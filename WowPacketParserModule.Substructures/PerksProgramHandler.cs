using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.Substructures
{
    public static class PerksProgramHandler
    {
        public static void ReadPerksVendorItem(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_1_7_61491))
                packet.ReadTime64("AvailableUntil", indexes);
            packet.ReadInt32("VendorItemID", indexes);
            packet.ReadInt32("MountID", indexes);
            packet.ReadInt32("BattlePetSpeciesID", indexes);
            packet.ReadInt32("TransmogSetID", indexes);
            packet.ReadInt32("ItemModifiedAppearanceID", indexes);
            packet.ReadInt32("TransmogIllusionID", indexes);
            packet.ReadInt32("ToyID", indexes);
            packet.ReadInt32("Price", indexes);
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_0_7_58123) || ClientVersion.AddedInVersion(ClientBranch.Cata, ClientVersionBuild.V4_4_2_59185) || ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_15_6_58797) || ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_4_59817))
                packet.ReadInt32("OriginalPrice", indexes);
            if (ClientVersion.Branch != ClientBranch.Retail || ClientVersion.RemovedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_1_7_61491))
                packet.ReadTime64("AvailableUntil", indexes);
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_1_0_59347) || ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_15_7_60000) || ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_4_59817))
                packet.ReadInt32("WarbandSceneID", indexes);
            packet.ReadBit("Disabled", indexes);
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_0_5_57171) || ClientVersion.AddedInVersion(ClientBranch.Cata, ClientVersionBuild.V4_4_2_59185) || ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_4_59817))
                packet.ReadBit("DoesNotExpire", indexes);
        }

        public static void ReadPerksVendorItem550(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();

            packet.ReadTime64("AvailableUntil", indexes);
            packet.ReadInt32("VendorItemID", indexes);
            packet.ReadInt32("MountID", indexes);
            packet.ReadInt32("BattlePetSpeciesID", indexes);
            packet.ReadInt32("TransmogSetID", indexes);
            packet.ReadInt32("ItemModifiedAppearanceID", indexes);
            packet.ReadInt32("TransmogIllusionID", indexes);
            packet.ReadInt32("ToyID", indexes);
            packet.ReadInt32("Price", indexes);
            packet.ReadInt32("OriginalPrice", indexes);
            packet.ReadInt32("WarbandSceneID", indexes);
            packet.ReadBit("Disabled", indexes);
            packet.ReadBit("DoesNotExpire", indexes);
        }
    }
}
