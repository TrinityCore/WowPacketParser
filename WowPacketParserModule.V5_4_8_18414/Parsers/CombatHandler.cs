using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellHitInfo = WowPacketParser.Enums.SpellHitInfo;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACKSWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TOGGLE_PVP)]
        public static void HandleCTogglePvp(Packet packet)
        {
            packet.ReadBit("Value");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        [Parser(Opcode.SMSG_ATTACKSTART)]
        [Parser(Opcode.SMSG_ATTACKSTOP)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
