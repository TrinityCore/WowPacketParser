using System;
using PacketParser.Enums;
using PacketParser.DataStructures;
using PacketParser.Misc;

namespace PacketParser.Parsing.Parsers
{
    public static class VoiceChatHandler
    {
        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.ReadBoolean("Voice Enabled");
            packet.ReadBoolean("Microphone Enabled");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE)]
        public static void HandleVoiceRosterUpdate(Packet packet)
        {
            packet.ReadGuid("Group GUID");
            packet.ReadInt16("Channel ID");
            packet.ReadByte("Channel Type"); // 0: channel, 2: party
            packet.ReadCString("Channel Name");
            packet.Store("Encryption Key", Utilities.ByteArrayToHexString(packet.ReadBytes(16)));
            packet.Store("IP", packet.ReadIPAddress());
            packet.ReadInt16("Voice Server Port");

            var count = packet.ReadByte("Player Count");

            packet.ReadGuid("Leader GUID");

            packet.ReadEnum<UnknownFlags>("Leader Flags 1", TypeCode.Byte);

            packet.ReadEnum<UnknownFlags>("Leader Flags 2", TypeCode.Byte);

            packet.StoreBeginList("Players");
            for (var i = 0; i < count - 1; i++)
            {
                packet.ReadGuid("Player GUID");
                packet.ReadByte("Index");
                packet.ReadEnum<UnknownFlags>("Flags 1", TypeCode.Byte);
                packet.ReadEnum<UnknownFlags>("Flags 2", TypeCode.Byte);
            }
            packet.StoreEndList();
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.ReadGuid("Player GUID");
            packet.ReadGuid("Group GUID");
        }

        [Parser(Opcode.SMSG_VOICE_SET_TALKER_MUTED)]
        public static void HandleSetTalkerMuted(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_VOICE_PARENTAL_CONTROLS)]
        public static void HandleVoiceParentalControls(Packet packet)
        {
            packet.ReadBoolean("Disable All");
            packet.ReadBoolean("Disable Microphone");
        }

        [Parser(Opcode.SMSG_AVAILABLE_VOICE_CHANNEL)]
        public static void HandleAvailableVoiceChannel(Packet packet)
        {
            packet.ReadGuid("Group GUID");
            packet.ReadByte("Channel Type");
            packet.ReadCString("Channel Name");
            packet.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            packet.ReadInt32("Channel ID");
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_ADD_VOICE_IGNORE)]
        public static void HandleAddVoiceIgnore(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_DEL_VOICE_IGNORE)]
        public static void HandleDelVoiceIgnore(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_VOICE_CHAT_STATUS)]
        public static void HandleVoiceStatus(Packet packet)
        {
            packet.ReadByte("Status");
        }
    }
}
