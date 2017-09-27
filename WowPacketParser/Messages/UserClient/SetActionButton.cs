using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetActionButton
    {
        public ulong Action;
        public byte Index;

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleSetActionButton(Packet packet)
        {
            packet.ReadByte("Button");
            var data = packet.ReadInt32();
            packet.AddValue("Type", (ActionButtonType)((data & 0xFF000000) >> 24));
            packet.AddValue("ID", data & 0x00FFFFFF);
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleSetActionButton510(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(0, 7, 6, 1, 3, 5, 2, 4);
            packet.ParseBitStream(actionId, 3, 0, 1, 4, 7, 2, 6, 5);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleSetActionButton530(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(0, 4, 7, 2, 5, 3, 1, 6);
            packet.ParseBitStream(actionId, 7, 3, 0, 2, 1, 4, 5, 6);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleSetActionButton542(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(3, 5, 2, 1, 0, 6, 4, 7);
            packet.ParseBitStream(actionId, 4, 0, 7, 2, 1, 3, 6, 5);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetActionButton547(Packet packet)
        {
            packet.ReadByte("Slot Id");
            var actionId = packet.StartBitStream(4, 7, 6, 3, 2, 0, 5, 1);
            packet.ParseBitStream(actionId, 3, 6, 1, 5, 7, 4, 2, 0);
            packet.AddValue("Action Id", BitConverter.ToUInt32(actionId, 0));
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSetActionButton602(Packet packet)
        {
            uint action = packet.ReadUInt32();
            uint type = packet.ReadUInt32();

            packet.AddValue("Action ", action);
            packet.AddValue("Type ", type);
            packet.ReadByte("Slot Id");
        }

    }
}
