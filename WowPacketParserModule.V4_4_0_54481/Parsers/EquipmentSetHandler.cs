using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class EquipmentSetHandler
    {
        [Parser(Opcode.SMSG_EQUIPMENT_SET_ID)]
        public static void HandleEquipmentSetSaved(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadUInt64("GUID");

            packet.ReadInt32("Type");
            packet.ReadUInt32("SetID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadUInt64("GUID");
        }

        private const int NumSlots = 19;

        [Parser(Opcode.SMSG_LOAD_EQUIPMENT_SET)]
        public static void HandleEquipmentSetList(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Type", i);
                packet.ReadUInt64("Guid", i);
                packet.ReadUInt32("SetID", i);
                uint ignoreMask = packet.ReadUInt32("IgnoreMask");

                for (var j = 0; j < NumSlots; j++)
                {
                    bool ignore = (ignoreMask & (1 << j)) != 0;
                    packet.ReadPackedGuid128("Pieces" + (ignore ? " (Ignored)" : ""), i, j);
                    packet.ReadInt32("Appearances", i);
                }

                for (var j = 0; j < 2; j++)
                    packet.ReadInt32("Enchants", i, j);

                packet.ReadInt32("SecondaryShoulderApparanceID");
                packet.ReadInt32("SecondaryShoulderSlot");
                packet.ReadInt32("SecondaryWeaponAppearanceID");
                packet.ReadInt32("SecondaryWeaponSlot");

                packet.ResetBitReader();
                var hasAssignedSpecIndex = packet.ReadBit("HasAssignedSpecIndex");
                var setNameLen = packet.ReadBits(8);
                var setIconLen = packet.ReadBits(9);

                if (hasAssignedSpecIndex)
                    packet.ReadInt32("AssignedSpecIndex", i);

                packet.ReadWoWString("SetName", setNameLen, i);
                packet.ReadWoWString("SetIcon", setIconLen, i);
            }
        }

        [Parser(Opcode.SMSG_USE_EQUIPMENT_SET_RESULT)]
        public static void HandleUseEquipmentSetResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadInt32("Reason");

            packet.ReadUInt64("Guid");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_4_1_57294))
                packet.ReadByte("Result");
        }

        [Parser(Opcode.CMSG_DELETE_EQUIPMENT_SET)]
        public static void HandleDeleteEquipmentSet(Packet packet)
        {
            packet.ReadUInt64("ID");
        }

        [Parser(Opcode.CMSG_SAVE_EQUIPMENT_SET)]
        public static void HandleEquipmentSetSave(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadUInt64("Guid");
            packet.ReadInt32("SetID");
            int ignoreMask = packet.ReadInt32("IgnoreMask");

            for (var i = 0; i < NumSlots; i++)
            {
                bool ignore = (ignoreMask & (1 << i)) != 0;
                packet.ReadPackedGuid128("Item Guid" + (ignore ? " (Ignored)" : ""), i);
                packet.ReadInt32("Appearance");
            }

            for (var j = 0; j < 2; j++)
                packet.ReadInt32("Enchants", j);

            packet.ReadInt32("SecondaryShoulderApparanceID");
            packet.ReadInt32("SecondaryShoulderSlot");
            packet.ReadInt32("SecondaryWeaponAppearanceID");
            packet.ReadInt32("SecondaryWeaponSlot");

            packet.ResetBitReader();

            var hasSpecIndex = packet.ReadBit("HasSpecIndex");
            var setNameLen = packet.ReadBits(8);
            var setIconLen = packet.ReadBits(9);

            if (hasSpecIndex)
                packet.ReadInt32("AssignedSpecIndex");

            packet.ReadWoWString("SetName", setNameLen);
            packet.ReadWoWString("SetIcon", setIconLen);
        }

        [Parser(Opcode.CMSG_USE_EQUIPMENT_SET)]
        public static void HandleUseEquipmentSet(Packet packet)
        {
            ItemHandler.ReadInvUpdate(packet, "InvUpdate");
            for (int i = 0; i < NumSlots; i++)
            {
                packet.ReadPackedGuid128("Item");
                packet.ReadByte("ContainerSlot");
                packet.ReadByte("Slot");
            }

            packet.ReadUInt64("GUID");
        }
    }
}
