using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCancelAura(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

                packet.ReadBit("Unk");
                var guid = packet.StartBitStream(6, 5, 1, 0, 4, 3, 2, 7);
                packet.ParseBitStream(guid, 3, 2, 1, 0, 4, 7, 5, 6);
                packet.WriteGuid("Guid", guid);
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            var unk40 = packet.ReadBit("unk40");
            var count = packet.ReadBits("count", 24);
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            var guid2 = new byte[count][];
            var unk164 = new byte[count];
            var unk156 = new byte[count];
            var unk336 = new uint[count];
            var unk144 = new byte[count];
            var unk400 = new uint[count];
            var unk200 = new byte[count];
            for (var i = 0 ; i < count; i++)
            {
                unk200[i] = packet.ReadBit("unk200", i);
                if (unk200[i] > 0)
                {
                    unk336[i] = packet.ReadBits("unk336", 22, i);
                    unk144[i] = packet.ReadBit("unk144", i);
                    if (unk144[i] > 0)
                        guid2[i] = packet.StartBitStream(3, 4, 6, 1, 5, 2, 0, 7);

                    unk400[i] = packet.ReadBits("unk400", 22, i);
                    unk164[i] = packet.ReadBit("unk164", i);
                    unk156[i] = packet.ReadBit("unk156", i);
                }
            }
            for (var i = 0; i < count; i++)
            {
                if (unk200[i] > 0)
                {
                    if (unk144[i] > 0)
                    {
                        packet.ParseBitStream(guid2[i], 3, 2, 1, 6, 4, 0, 5, 7);
                        packet.WriteGuid("Guid2", guid2[i], i);
                    }
                    packet.ReadByte("unk124", i);
                    packet.ReadInt16("unk152", i);
                    packet.ReadInt32("unk144", i);
                    if (unk156[i] > 0)
                        packet.ReadInt32("unk272", i);
                    if (unk164[i] > 0)
                        packet.ReadInt32("unk304", i);
                    for (var j = 0; j < unk400[i]; j++)
                        packet.ReadSingle("unk416", i, j);
                    packet.ReadByte("unk134", i);
                    packet.ReadInt32("unk176", i);
                    for (var j = 0; j < unk336[i]; j++)
                        packet.ReadSingle("unk352", i, j);
                }
                packet.ReadByte("unk112", i);
            }
            packet.ParseBitStream(guid, 2, 6, 7, 1, 3, 4, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadBit("Unk 1bit");
            var count = packet.ReadBits("Count", 22);

            for (int i = 0; i < count; i++)
            {
                packet.ReadUInt32("Spell", i);
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ReadBit("Byte16");
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_SPELL_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var unk40 = !packet.ReadBit("!unk40");
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var count = packet.ReadBits("count", 21);
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk80", i);
                packet.ReadInt32("unk84", i);
            }
            packet.ParseBitStream(guid, 5, 3, 7);
            if (unk40)
                packet.ReadByte("unk40");
            packet.ParseBitStream(guid, 4, 1, 0, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadToEnd();
        }

        [HasSniffData]
        [Parser(Opcode.CMSG_CANCEL_CAST)]
        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        public static void HandleCmsgNull(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CAST_FAILED)]
        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadToEnd();
            }
            else MovementHandler.HandleMoveStartBackWard(packet);
        }
    }
}
