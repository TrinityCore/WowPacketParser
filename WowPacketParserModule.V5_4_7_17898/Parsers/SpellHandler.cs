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

namespace WowPacketParser.V5_4_7_17898.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnSpell(Packet packet)
        {
            packet.ReadBit("Unk Bits");
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_REMOVED_SPELL)]
        public static void HandleRemovedSpell(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID", i);
        }
    }
}
