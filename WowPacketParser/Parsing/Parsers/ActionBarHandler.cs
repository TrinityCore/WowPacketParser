using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Action = WowPacketParser.Store.Objects.Action;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleInitialButtons(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                // State = 0: Looks to be sent when initial action buttons get sent, however on Trinity we use 1 since 0 had some difficulties
                // State = 1: Used in any SMSG_ACTION_BUTTONS packet with button data on Trinity. Only used after spec swaps on retail.
                // State = 2: Clears the action bars client sided. This is sent during spec swap before unlearning and before sending the new buttons
                if (packet.ReadByte("Packet Type") == 2)
                    return;
            }

            var buttonCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 144 : 132;

            var startAction = new StartAction { Actions = new List<Action>(buttonCount) };

            for (var i = 0; i < buttonCount; i++)
            {
                var action = new Action { Button = (uint)i };

                var packed = packet.ReadInt32();

                if (packed == 0)
                    continue;

                action.Id = (uint)(packed & 0x00FFFFFF);
                packet.AddValue("Action", action.Id, i);

                action.Type = (ActionButtonType)((packed & 0xFF000000) >> 24);
                packet.AddValue("Type", action.Type, i);

                startAction.Actions.Add(action);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadByte("Packet Type");

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartActions.Add(new Tuple<Race, Class>(player.Race, player.Class), startAction, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleActionButtons(Packet packet)
        {
            const int buttonCount = 132;

            var startAction = new StartAction { Actions = new List<Action>(buttonCount) };

            var buttons = new byte[buttonCount][];

            for (var i = 0; i < buttonCount; i++)
            {
                buttons[i] = new byte[8];
                buttons[i][4] = packet.ReadBit();
            }

            for (var i = 0; i < buttonCount; i++)
                buttons[i][0] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][7] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][2] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][6] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][3] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][1] = packet.ReadBit();
            for (var i = 0; i < buttonCount; i++)
                buttons[i][5] = packet.ReadBit();

            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 0);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 3);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 5);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 7);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 6);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 1);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 4);
            for (var i = 0; i < buttonCount; i++)
                packet.ReadXORByte(buttons[i], 2);

            packet.ReadByte("Packet Type");

            for (int i = 0; i < buttonCount; i++)
            {
                var actionId = BitConverter.ToInt32(buttons[i], 0);

                if (actionId == 0)
                    continue;

                var action = new Action
                {
                    Button = (uint)i,
                    Id = (uint)actionId,
                    Type = 0 // removed in MoP
                };

                packet.AddValue("Action", action.Id, i);
                startAction.Actions.Add(action);
            }

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartActions.Add(new Tuple<Race, Class>(player.Race, player.Class), startAction, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_SET_ACTION_BAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("Action Bar");
        }
    }
}
