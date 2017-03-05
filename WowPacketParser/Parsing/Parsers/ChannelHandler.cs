using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL)]
        public static void HandleChannelSilencing(Packet packet)
        {
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadCString("Player Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_OWNER)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_VOICE_ON)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_VOICE_OFF)]
        [Parser(Opcode.CMSG_SET_CHANNEL_WATCH)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST)]
        public static void HandleChannelMisc(Packet packet)
        {
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.Translator.ReadByte("Type");
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadByteE<ChannelFlag>("Flags");
            var count = packet.Translator.ReadInt32("Counter");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadGuid("Player GUID " + i);
                packet.Translator.ReadByteE<ChannelMemberFlag>("Player Flags " + i);
            }
        }

        [Parser(Opcode.SMSG_CHANNEL_MEMBER_COUNT)]
        public static void HandleChannelMemberCount(Packet packet)
        {
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadByteE<ChannelFlag>("Flags");
            packet.Translator.ReadInt32("Unk int32");
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.Translator.ReadByteE<ChatNotificationType>("Notification Type");

            if (type == ChatNotificationType.InvalidName) // hack, because of some silly reason this type
                packet.Translator.ReadBytes(3);                      // has 3 null bytes before the invalid channel name

            packet.Translator.ReadCString("Channel Name");

            switch(type)
            {
                case ChatNotificationType.PlayerAlreadyMember:
                case ChatNotificationType.Invite:
                case ChatNotificationType.ModerationOn:
                case ChatNotificationType.ModerationOff:
                case ChatNotificationType.AnnouncementsOn:
                case ChatNotificationType.AnnouncementsOff:
                case ChatNotificationType.PasswordChanged:
                case ChatNotificationType.OwnerChanged:
                case ChatNotificationType.Joined:
                case ChatNotificationType.Left:
                case ChatNotificationType.VoiceOn:
                case ChatNotificationType.VoiceOff:
                {
                    packet.Translator.ReadGuid("GUID");
                    break;
                }
                case ChatNotificationType.YouJoined:
                {
                    packet.Translator.ReadByteE<ChannelFlag>("Flags");
                    packet.Translator.ReadInt32("Channel Id");
                    packet.Translator.ReadInt32("Unk");
                    break;
                }
                case ChatNotificationType.YouLeft:
                {
                    packet.Translator.ReadInt32("Channel Id");
                    packet.Translator.ReadBool("Unk");
                    break;
                }
                case ChatNotificationType.PlayerNotFound:
                case ChatNotificationType.ChannelOwner:
                case ChatNotificationType.PlayerNotBanned:
                case ChatNotificationType.PlayerInvited:
                case ChatNotificationType.PlayerInviteBanned:
                {
                    packet.Translator.ReadCString("Player Name");
                    break;
                }
                case ChatNotificationType.ModeChange:
                {
                    packet.Translator.ReadGuid("GUID");
                    packet.Translator.ReadByteE<ChannelMemberFlag>("Old Flags");
                    packet.Translator.ReadByteE<ChannelMemberFlag>("New Flags");
                    break;
                }
                case ChatNotificationType.PlayerKicked:
                case ChatNotificationType.PlayerBanned:
                case ChatNotificationType.PlayerUnbanned:
                {
                    packet.Translator.ReadGuid("Bad");
                    packet.Translator.ReadGuid("Good");
                    break;
                }
                case ChatNotificationType.TrialRestricted:
                {
                    packet.Translator.ReadGuid("GUID");
                    break;
                }
                case ChatNotificationType.WrongPassword:
                case ChatNotificationType.NotMember:
                case ChatNotificationType.NotModerator:
                case ChatNotificationType.NotOwner:
                case ChatNotificationType.Muted:
                case ChatNotificationType.Banned:
                case ChatNotificationType.InviteWrongFaction:
                case ChatNotificationType.WrongFaction:
                case ChatNotificationType.InvalidName:
                case ChatNotificationType.NotModerated:
                case ChatNotificationType.Throttled:
                case ChatNotificationType.NotInArea:
                case ChatNotificationType.NotInLfg:
                    break;
            }
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.Translator.ReadInt32("Channel Id");
            packet.Translator.ReadBool("Has Voice");
            packet.Translator.ReadBool("Joined by zone update");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
            {
                packet.Translator.ReadCString("Channel Pass");
                packet.Translator.ReadCString("Channel Name");
            }
            else
            {
                packet.Translator.ReadCString("Channel Name");
                packet.Translator.ReadCString("Channel Pass");
            }
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_BAN)] // 4.3.4
        public static void HandleChannelBan(Packet packet)
        {
            var channelLength = packet.Translator.ReadBits(8);
            var nameLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Channel", channelLength);
            packet.Translator.ReadWoWString("Player to ban", nameLength);
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelJoin434(Packet packet)
        {
            packet.Translator.ReadInt32("Channel Id");
            packet.Translator.ReadBit("Has Voice");
            packet.Translator.ReadBit("Joined by zone update");
            var channelLength = packet.Translator.ReadBits(8);
            var passwordLength = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Channel Name", channelLength);
            packet.Translator.ReadWoWString("Channel Pass", passwordLength);
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.Translator.ReadInt32("Channel Id");
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByteE<ChannelFlag>("Flags");
            packet.Translator.ReadInt32("Counter");
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByteE<ChannelMemberFlag>("Member Flags");
            packet.Translator.ReadByteE<ChannelFlag>("Flags");
            packet.Translator.ReadInt32("Counter");
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelPassword(Packet packet)
        {
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadCString("Password");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelPassword434(Packet packet)
        {
            var channelLen = packet.Translator.ReadBits(8);
            var passLen = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("Channel Name", channelLen);
            packet.Translator.ReadWoWString("Password", passLen);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelInvite(Packet packet)
        {
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelInvite434(Packet packet)
        {
            var nameLen = packet.Translator.ReadBits(7);
            var channelLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Name", nameLen);
            packet.Translator.ReadWoWString("Channel Name", channelLen);
        }
    }
}
