using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChannelHandler
    {
        private static void ReadChannelFlags(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                packet.Translator.ReadUInt32E<ChannelFlag>("ChannelFlags");
            else
                packet.Translator.ReadByteE<ChannelFlag>("ChannelFlags");
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.Translator.ReadInt32("Channel Id");
            packet.Translator.ReadBit("CreateVoiceSession");
            packet.Translator.ReadBit("Internal");

            var channelLength = packet.Translator.ReadBits(7);
            var passwordLength = packet.Translator.ReadBits(7);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("ChannelName", channelLength);
            packet.Translator.ReadWoWString("Password", passwordLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var bits544 = packet.Translator.ReadBits(7);
            var bits24 = packet.Translator.ReadBits(10);

            ReadChannelFlags(packet);
            packet.Translator.ReadInt32("ChatChannelID");
            packet.Translator.ReadInt64("InstanceID");

            packet.Translator.ReadWoWString("Channel", bits544);
            packet.Translator.ReadWoWString("ChannelWelcomeMsg", bits24);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit("Suspended");
            packet.Translator.ReadInt32("ChatChannelID");
            packet.Translator.ReadWoWString("Channel", bits20);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.Translator.ReadBitsE<ChatNotificationType>("Type", 6);
            var channelLength = packet.Translator.ReadBits(7);
            var senderLength = packet.Translator.ReadBits(6);

            packet.Translator.ReadPackedGuid128("SenderGuid");
            packet.Translator.ReadPackedGuid128("BnetAccountID");

            packet.Translator.ReadInt32("SenderVirtualRealm");

            packet.Translator.ReadPackedGuid128("TargetGuid");

            packet.Translator.ReadInt32("TargetVirtualRealm");
            packet.Translator.ReadInt32("ChatChannelID");

            if (type == ChatNotificationType.ModeChange)
            {
                packet.Translator.ReadByteE<ChannelMemberFlag>("OldFlags");
                packet.Translator.ReadByteE<ChannelMemberFlag>("NewFlags");
            }

            packet.Translator.ReadWoWString("Channel", channelLength);
            packet.Translator.ReadWoWString("Sender", senderLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.Translator.ReadBit("Display");
            var bits108 = packet.Translator.ReadBits(7);

            ReadChannelFlags(packet);
            var int20 = packet.Translator.ReadInt32("MembersCount");

            packet.Translator.ReadWoWString("Channel", bits108);

            for (var i = 0; i < int20; i++)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadUInt32("VirtualRealmAddress", i);
                packet.Translator.ReadByte("Flags", i);
            }
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.Translator.ReadInt32("ZoneChannelID");
            var bits108 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            packet.Translator.ReadByte("ChannelType");
            var bits108 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.SMSG_AVAILABLE_VOICE_CHANNEL)]
        public static void HandleAvailableVoiceChannel(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SessionGUID");
            packet.Translator.ReadPackedGuid128("LocalGUID");
            packet.Translator.ReadByte("ChannelType");
            var bits108 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.Translator.ReadBit("EnableVoiceChat");
            packet.Translator.ReadBit("EnableMicrophone");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("LocalGUID");
            packet.Translator.ReadPackedGuid128("SessionGUID");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_SET_OWNER)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_MODERATOR)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_MUTE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNMUTE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_KICK)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_BAN)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNBAN)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.Translator.ReadBits(7);
            var lenName = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("ChannelName", lenChannelName);
            packet.Translator.ReadWoWString("Name", lenName);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_VOICE_ON)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_VOICE_OFF)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_OWNER)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var bits108 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD)]
        public static void HandleChannelPassword(Packet packet)
        {
            var lenChannelName = packet.Translator.ReadBits(7);
            var lenName = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("ChannelName", lenChannelName);
            packet.Translator.ReadWoWString("Name", lenName);
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("AddedUserGUID");

            packet.Translator.ReadByteE<ChannelMemberFlag>("UserFlags");
            ReadChannelFlags(packet);
            packet.Translator.ReadInt32("ChannelID");

            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("RemovedUserGUID");

            ReadChannelFlags(packet);
            packet.Translator.ReadInt32("ChannelID");

            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UpdatedUserGUID");

            packet.Translator.ReadByteE<ChannelMemberFlag>("UserFlags");
            ReadChannelFlags(packet);
            packet.Translator.ReadInt32("ChannelID");

            var len = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("ChannelName", len);
        }
    }
}
