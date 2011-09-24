using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_ACTION_BUTTONS)]
        public static void HandleInitialButtons(Packet packet)
        {
            packet.ReadByte("Talent Spec");

            for (var i = 0; i < 144; i++)
            {
                var packed = packet.ReadInt32();

                if (packed == 0)
                    continue;

                var action = packed & 0x00FFFFFF;
                Console.WriteLine("Action " + i + ": " + action);

                var type = (ActionButtonType)((packed & 0xFF000000) >> 24);
                Console.WriteLine("Type " + i + ": " + type);
            }
        }
    }
}
