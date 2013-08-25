using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            var counter = packet.ReadBits(2);
            var unk_bit = !packet.ReadBit();
            for (var i = 0; i < counter; ++i)
                packet.ReadBits("unk value0", 2, i);

            var HasCastCount = !packet.ReadBit();
            packet.ReadBit("Fake bit? Has TargetGUID"); // TargetGUID
            var hasbit1C = !packet.ReadBit();
            var hasMovment = packet.ReadBit();
            var hasbit78 = !packet.ReadBit();
            var hasbitF8 = !packet.ReadBit();
            var hasGUID2 = packet.ReadBit();
            var hasbitFC = !packet.ReadBit();
            var hasbit18 = !packet.ReadBit();
            var hasGUID3 = packet.ReadBit();
            packet.ReadBit("Fake bit? Has GUID0"); // GUID0
            var hasSpellId = !packet.ReadBit();

            var GUID0 = new byte[8];
            var TargetGUID = new byte[8];
            var GUID2 = new byte[8];
            var GUID3 = new byte[8];

            GUID0 = packet.StartBitStream(0, 5, 1, 7, 4, 3, 6, 2);
            if (hasGUID3)
                GUID3 = packet.StartBitStream(2, 5, 3, 7, 4, 1, 0, 6);

            if (hasGUID2)
                GUID2 = packet.StartBitStream(6, 2, 4, 7, 3, 5, 0, 1);

            TargetGUID = packet.StartBitStream(3, 0, 2, 7, 6, 4, 1, 5);

            if (unk_bit)
                packet.ReadEnum<CastFlag>("Cast Flags", 20);

            if (hasbit1C)
                packet.ReadBits("hasbit1C", 5);

            uint len78 = 0;
            if (hasbit78)
                len78 = packet.ReadBits("hasbit78", 7);
            packet.ResetBitReader();

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadInt32("unk value1", i);
                packet.ReadInt32("unk value2", i);
            }

            if (hasGUID3)
            {
                packet.ReadXORBytes(GUID3, 7, 5, 3);
                packet.ReadSingle("float68");
                packet.ReadXORBytes(GUID3, 0, 2, 1, 4, 6);
                packet.ReadSingle("float70");
                packet.ReadSingle("float6C");
                packet.WriteGuid("GUID3", GUID3);
            }

            packet.ParseBitStream(TargetGUID, 2, 0, 5, 6, 7, 3, 4, 1);
            packet.WriteGuid("Target GUID", TargetGUID);

            if (hasGUID2)
            {
                packet.ReadXORBytes(GUID2, 5, 7);
                packet.ReadSingle("float4C");
                packet.ReadSingle("float48");
                packet.ReadXORBytes(GUID2, 3, 1);
                packet.ReadSingle("float50");
                packet.ReadXORBytes(GUID2, 2, 6, 4, 0);
                packet.WriteGuid("GUID2", GUID2);
            }

            packet.ParseBitStream(GUID0, 7, 2, 6, 4, 1, 0, 3, 5);
            packet.WriteGuid("GUID0", GUID0);

            if (hasbit78)
                packet.ReadWoWString("String", (int)len78);

            if (HasCastCount)
                packet.ReadByte("Cast Count");

            if (hasbit18)
                packet.ReadInt32("Int18");

            if (hasMovment)
                MovementHandler.ReadClientMovementBlock(ref packet);

            if (hasSpellId)
                packet.ReadInt32("SpellId");

            if (hasbitF8)
                packet.ReadSingle("FloatF8");

            if (hasbitFC)
                packet.ReadSingle("FloatFC");
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);
            packet.ReadBit("Unk Bit");

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
                spells.Add((uint)spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }
    }
}
