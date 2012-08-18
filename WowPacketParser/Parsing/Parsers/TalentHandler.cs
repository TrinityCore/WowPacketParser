using System;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.DataStructures;

namespace PacketParser.Parsing.Parsers
{
    public static class TalentHandler
    {
        public static void ReadTalentInfo(ref Packet packet)
        {
            packet.ReadUInt32("Free Talent count");
            var speccount = packet.ReadByte("Spec count");
            packet.ReadByte("Active Spec");
            packet.StoreBeginList("Specs");
            for (var i = 0; i < speccount; ++i)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadUInt32("TalentBranchSpec", i);
                var count2 = packet.ReadByte("Spec Talent Count ", i);
                packet.StoreBeginList("Talents", i);
                for (var j = 0; j < count2; ++j)
                {
                    packet.ReadUInt32("Talent Id", i, j);
                    packet.ReadByte("Rank", i, j);
                }
                packet.StoreEndList();

                var glyphs = packet.ReadByte("Glyph count", i);
                packet.StoreBeginList("Glyphs", i);
                for (var j = 0; j < glyphs; ++j)
                    packet.ReadUInt16("Glyph", i, j);
                packet.StoreEndList();
            }
            packet.StoreEndList();
        }

        public static void ReadInspectPart(ref Packet packet)
        {
            var slotMask = packet.ReadUInt32("Slot Mask");
            var slot = 0;
            packet.StoreBeginList("Slots");
            while (slotMask > 0)
            {
                if ((slotMask & 0x1) > 0)
                {
                    packet.Store("Slot", (EquipmentSlotType)slot, slot);
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", slot);
                    var enchantMask = packet.ReadUInt16("Enchant Mask", slot);
                    if (enchantMask > 0)
                    {
                        var enchCnt = 0;
                        packet.StoreBeginList("Enchantments", slot);
                        while (enchantMask > 0)
                        {
                            if ((enchantMask & 0x1) > 0)
                            {
                                packet.ReadUInt16("Enchantment", slot, enchCnt);
                            }
                            enchantMask >>= 1;
                            ++enchCnt;
                        }
                        packet.StoreEndList();
                    }
                    packet.ReadUInt16("Unk Uint16", slot);
                    packet.ReadPackedGuid("Creator GUID", slot);
                    packet.ReadUInt32("Unk Uint32", slot);
                }
                ++slot;
                slotMask >>= 1;
            }
            packet.StoreEndList();
        }
        [Parser(Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET)]
        public static void HandleTalentsInvoluntarilyReset(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_INSPECT_TALENT)]
        public static void HandleInspectTalent(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadGuid("GUID");
            else
                packet.ReadPackedGuid("GUID");

            ReadTalentInfo(ref packet);
            ReadInspectPart(ref packet);

            if (packet.CanRead()) // otherwise it would fail for players without a guild
            {
                packet.ReadGuid("Guild GUID");
                packet.ReadUInt32("Guild Level");
                packet.ReadUInt64("Guild Xp");
                packet.ReadUInt32("Guild Members");
            }
        }

        [Parser(Opcode.SMSG_INSPECT_RESULTS_UPDATE)]
        public static void HandleInspectResultsUpdate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // confirmed for 4.3.4
                packet.ReadPackedGuid("GUID");
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                packet.ReadGuid("GUID");
            else
                packet.ReadPackedGuid("GUID");

            ReadInspectPart(ref packet);
        }

        [Parser(Opcode.MSG_TALENT_WIPE_CONFIRM)]
        public static void HandleTalent(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ServerToClient)
                packet.ReadUInt32("Gold");
        }

        [Parser(Opcode.SMSG_TALENTS_INFO)]
        public static void HandleTalentsInfo(Packet packet)
        {
            var pet = packet.ReadBoolean("Pet Talents");
            if (pet)
            {
                packet.ReadUInt32("Unspent Talent");
                var count = packet.ReadByte("Talent Count");
                packet.StoreBeginList("Talents");
                for (var i = 0; i < count; ++i)
                {
                    packet.ReadUInt32("Talent ID", i);
                    packet.ReadByte("Rank", i);
                }
                packet.StoreEndList();
            }
            else
                ReadTalentInfo(ref packet);
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET))
                packet.ReadGuid("GUID");

            var count = packet.ReadUInt32("Talent Count");
            packet.StoreBeginList("Talents");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Talent ID", i);
                packet.ReadUInt32("Rank", i);
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTalentPreviewTalents434(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_LEARN_PREVIEW_TALENTS_PET))
                packet.ReadGuid("GUID");
            else
                packet.ReadUInt32("Tab Page");

            var count = packet.ReadUInt32("Talent Count");
            packet.StoreBeginList("talents");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Talent ID", i);
                packet.ReadUInt32("Rank", i);
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.CMSG_LEARN_TALENT)]
        public static void HandleLearnTalent(Packet packet)
        {
            packet.ReadUInt32("Talent ID");
            packet.ReadUInt32("Rank");
        }

        [Parser(Opcode.SMSG_TALENTS_ERROR)]
        public static void HandleTalentError(Packet packet)
        {
            packet.ReadEnum<TalentError>("Talent Error", TypeCode.Int32);
        }

        //[Parser(Opcode.CMSG_UNLEARN_TALENTS)]

        //[Parser(Opcode.CMSG_PET_LEARN_TALENT)]
        //[Parser(Opcode.CMSG_PET_UNLEARN_TALENTS)]
        //[Parser(Opcode.CMSG_SET_ACTIVE_TALENT_GROUP_OBSOLETE)]
        //[Parser(Opcode.CMSG_SET_PRIMARY_TALENT_TREE)] 4.0.6a
    }
}
