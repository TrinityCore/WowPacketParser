using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Action = WowPacketParser.Store.Objects.Action;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            const int buttonCount = 132;

            var startAction = new StartAction { Actions = new List<Action>(buttonCount) };

            for (var i = 0; i < buttonCount; ++i)
            {
                var action = new Action();
                //packet.ReadUInt64("Action");

                action.Button = (uint)i;

                action.Id = packet.ReadUInt32();
                var type = packet.ReadUInt32();

                packet.AddValue("Action " + i, action.Id);
                packet.AddValue("Type " + i, type);

                if (type == 0)
                    startAction.Actions.Add(action);
            }

            packet.ReadByte("Packet Type");

            if (CoreParsers.SessionHandler.LoginGuid != null)
            {
                WoWObject character;
                if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
                {
                    var player = character as Player;
                    if (player != null && player.FirstLogin)
                        Storage.StartActions.Add(new Tuple<Race, Class>(player.Race, player.Class), startAction, packet.TimeSpan);
                }
            }
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            //packet.ReadUInt64("Action");
            var action = packet.ReadUInt32();
            var type = packet.ReadUInt32();

            packet.AddValue("Action ", action);
            packet.AddValue("Type ", type);
            packet.ReadByte("Slot Id");
        }
    }
}
