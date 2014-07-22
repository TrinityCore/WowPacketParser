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

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            var guid = packet.StartBitStream(0, 5, 6, 3, 7, 4, 1, 2);
            packet.ParseBitStream(guid, 5, 6, 4, 7, 3, 0);
            packet.ReadByte("Combo Points");
            packet.ParseBitStream(guid, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        [Parser(Opcode.SMSG_ATTACKERSTATEUPDATE)]
        [Parser(Opcode.SMSG_ATTACKSTART)]
        [Parser(Opcode.SMSG_ATTACKSTOP)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : CMSG_???");
                packet.ReadToEnd();
            }
        }
    }
}
