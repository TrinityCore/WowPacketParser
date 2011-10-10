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
            // State = 0: Looks to be sent when initial action buttons get sent, however on Trinity we use 1 since 0 had some difficulties
            // State = 1: Used in any SMSG_ACTION_BUTTONS packet with button data on Trinity. Only used after spec swaps on retail.
            // State = 2: Clears the action bars client sided. This is sent during spec swap before unlearning and before sending the new buttons
            var type = packet.ReadByte("Packet Type");

            if (type != 2)
            {
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
}
