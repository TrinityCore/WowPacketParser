using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            const int buttonCount = 132;

            var buttons = new byte[buttonCount][];

            for (int i = 0; i < buttonCount; i++)
            {
                buttons[i] = new byte[8];
                buttons[i][2] = packet.ReadBit();
            }

            for (int i = 0; i < buttonCount; i++)
                buttons[i][7] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][3] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][0] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][6] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][1] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][4] = packet.ReadBit();
            for (int i = 0; i < buttonCount; i++)
                buttons[i][5] = packet.ReadBit();

            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 7);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 0);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 6);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 5);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 1);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 2);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 4);
            for (int i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 3);

            packet.ReadByte("Packet Type");

            for (int i = 0; i < buttonCount; i++)
            {
                int actionId = BitConverter.ToInt32(buttons[i], 0);

                if (actionId == 0)
                    continue;

                PlayerCreateInfoAction action = new PlayerCreateInfoAction
                {
                    Button = (uint)i,
                    Action = (uint)actionId,
                    Type = 0 // removed in MoP
                };

                packet.AddValue("Action " + i, action.Action);

                WoWObject character;
                if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
                {
                    Player player = character as Player;
                    if (player != null && player.FirstLogin)
                    {
                        action.Race = player.Race;
                        action.Class = player.Class;
                        Storage.StartActions.Add(action, packet.TimeSpan);
                    }
                }
            }
        }
    }
}
