using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            packet.ReadBit("CreateVoiceSession");
            packet.ReadBit("Internal");

            var channelLength = packet.ReadBits(7);
            var passwordLength = packet.ReadBits(7);

            packet.ResetBitReader();

            packet.ReadWoWString("ChannelName", channelLength);
            packet.ReadWoWString("Password", passwordLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var bits544 = packet.ReadBits(7);
            var bits24 = packet.ReadBits(10);

            packet.ReadByte("ChannelFlags");
            packet.ReadInt32("ChatChannelID");
            packet.ReadInt64("InstanceID");

            packet.ReadWoWString("Channel", bits544);
            packet.ReadWoWString("ChannelWelcomeMsg", bits24);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.ReadBits(7);
            packet.ReadBit("Suspended");
            packet.ReadInt32("ChatChannelID");
            packet.ReadWoWString("Channel", bits20);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.ReadBitsE<ChatNotificationType>("Type", 6);
            var channelLength = packet.ReadBits(7);
            var senderLength = packet.ReadBits(6);

            packet.ReadPackedGuid128("SenderGuid");
            packet.ReadPackedGuid128("BnetAccountID");

            packet.ReadInt32("SenderVirtualRealm");

            packet.ReadPackedGuid128("TargetGuid");

            packet.ReadInt32("TargetVirtualRealm");
            packet.ReadInt32("ChatChannelID");

            if (type == ChatNotificationType.ModeChange)
            {
                packet.ReadByteE<ChannelMemberFlag>("OldFlags");
                packet.ReadByteE<ChannelMemberFlag>("NewFlags");
            }

            packet.ReadWoWString("Channel", channelLength);
            packet.ReadWoWString("Sender", senderLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.ReadBit("Display");
            var bits108 = packet.ReadBits(7);

            packet.ReadByte("ChannelFlags");
            var int20 = packet.ReadInt32("MembersCount");

            packet.ReadWoWString("Channel", bits108);

            for (var i = 0; i < int20; i++)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadUInt32("VirtualRealmAddress", i);
                packet.ReadByte("Flags", i);
            }
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.ReadInt32("ZoneChannelID");
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            packet.ReadByte("ChannelType");
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.SMSG_AVAILABLE_VOICE_CHANNEL)]
        public static void HandleAvailableVoiceChannel(Packet packet)
        {
            packet.ReadPackedGuid128("SessionGUID");
            packet.ReadPackedGuid128("LocalGUID");
            packet.ReadByte("ChannelType");
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.ReadBit("EnableVoiceChat");
            packet.ReadBit("EnableMicrophone");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.ReadPackedGuid128("LocalGUID");
            packet.ReadPackedGuid128("SessionGUID");
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
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
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
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD)]
        public static void HandleChannelPassword(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(7);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.ReadPackedGuid128("AddedUserGUID");

            packet.ReadByteE<ChannelFlag>("ChannelFlags");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");

            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RemovedUserGUID");

            packet.ReadByteE<ChannelFlag>("ChannelFlags");

            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UpdatedUserGUID");

            packet.ReadByteE<ChannelFlag>("ChannelFlags");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");

            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }
    }
}
