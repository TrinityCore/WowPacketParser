using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoiceChatHandler
    {
        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.ReadBool("Voice Enabled");
            packet.ReadBool("Microphone Enabled");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE)]
        public static void HandleVoiceRosterUpdate(Packet packet)
        {
            packet.ReadGuid("Group GUID");
            packet.ReadInt16("Channel ID");
            packet.ReadByte("Channel Type"); // 0: channel, 2: party
            packet.ReadCString("Channel Name");
            packet.ReadBytes("Encryption Key", 16);
            packet.ReadIPAddress("IP");
            packet.ReadInt16("Voice Server Port");

            var count = packet.ReadByte("Player Count");

            packet.ReadGuid("Leader GUID");

            var leaderFlags1 = packet.ReadByte();
            packet.AddValue("Leader Flags 1", "0x" + leaderFlags1.ToString("X2"));

            var leaderFlags2 = packet.ReadByte();
            packet.AddValue("Leader Flags 2", "0x" + leaderFlags2.ToString("X2"));

            for (var i = 0; i < count - 1; i++)
            {
                packet.ReadGuid("Player GUID");
                packet.ReadByte("Index");
                var flags1 = packet.ReadByte();
                packet.AddValue("Flags 1", "0x" + flags1.ToString("X2"));
                var flags2 = packet.ReadByte();
                packet.AddValue("Flags 2", "0x" + flags2.ToString("X2"));
            }
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
            packet.ReadBool("Disable All");
            packet.ReadBool("Disable Microphone");
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

        [Parser(Opcode.CMSG_VOICE_ADD_IGNORE)]
        public static void HandleAddVoiceIgnore(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_VOICE_DEL_IGNORE)]
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
