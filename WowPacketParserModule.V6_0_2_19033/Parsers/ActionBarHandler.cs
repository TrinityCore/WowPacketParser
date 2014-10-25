using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using CoreObjects = WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            const int buttonCount = 132;

            var startAction = new CoreObjects.StartAction { Actions = new List<CoreObjects.Action>(buttonCount) };

            for (var i = 0; i < buttonCount; ++i)
            {
                var action = new CoreObjects.Action();
                var packed = packet.ReadInt64();
                if (packed == 0)
                    continue;

                action.Button = (uint)i;

                action.Id = (uint)(packed & 0x0FFFFFFF);
                action.Type = (ActionButtonType)(packed & 0xF0000000);
                packet.AddValue("Action " + i, action.Id);
                packet.AddValue("Type " + i, action.Type);

                startAction.Actions.Add(action);
            }

            packet.ReadByte("Packet Type");

            CoreObjects.WoWObject character;
            if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
            {
                var player = character as CoreObjects.Player;
                if (player != null && player.FirstLogin)
                    Storage.StartActions.Add(new Tuple<Race, Class>(player.Race, player.Class), startAction, packet.TimeSpan);
            }
        }
    }
}
