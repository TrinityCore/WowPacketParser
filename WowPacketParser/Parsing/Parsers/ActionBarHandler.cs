using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_ACTION_BUTTONS)]
        public static void HandleInitialButtons(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                // State = 0: Looks to be sent when initial action buttons get sent, however on Trinity we use 1 since 0 had some difficulties
                // State = 1: Used in any SMSG_ACTION_BUTTONS packet with button data on Trinity. Only used after spec swaps on retail.
                // State = 2: Clears the action bars client sided. This is sent during spec swap before unlearning and before sending the new buttons
                if (packet.ReadByte("Packet Type") == 2 && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                    return;
            }

            var buttonCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 144 : 132;

            var startAction = new StartAction
                              {
                                  Actions = new List<Store.Objects.Action>(buttonCount)
                              };

            for (var i = 0; i < buttonCount; i++)
            {
                var action = new Store.Objects.Action
                             {
                                 Button = (uint)i
                             };

                var packed = packet.ReadInt32();

                if (packed == 0)
                    continue;

                action.Id = (uint)(packed & 0x00FFFFFF);
                packet.WriteLine("Action " + i + ": " + action.Id);

                action.Type = (ActionButtonType)((packed & 0xFF000000) >> 24);
                packet.WriteLine("Type " + i + ": " + action.Type);

                startAction.Actions.Add(action);
            }

            if (SessionHandler.LoggedInCharacter != null && SessionHandler.LoggedInCharacter.FirstLogin)
                Storage.StartActions.Add(new Tuple<Race, Class>(SessionHandler.LoggedInCharacter.Race, SessionHandler.LoggedInCharacter.Class), startAction, packet.TimeSpan);
        }


        [Parser(Opcode.CMSG_SET_ACTIONBAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("Action Bar");
        }
    }
}
