using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreObjects = WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.CMSG_SET_ACTIONBAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("Action Bar");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
