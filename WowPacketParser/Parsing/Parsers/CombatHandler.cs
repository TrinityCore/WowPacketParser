using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var reaction = (AIReaction)packet.ReadInt32();
            Console.WriteLine("Reaction: " + reaction);
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);

            var amount = packet.ReadByte();
            Console.WriteLine("Combo Points: " + amount);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTALDAMAGELOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var type = (EnvironmentDamageFlags)packet.ReadByte();
            Console.WriteLine("Type: " + type);

            var damage = packet.ReadInt32();
            Console.WriteLine("damage: " + damage);

            var absorb = packet.ReadInt32();
            Console.WriteLine("absorb: " + absorb);

            var resist = packet.ReadInt32();
            Console.WriteLine("resist: " + resist);
        }

    }
}
