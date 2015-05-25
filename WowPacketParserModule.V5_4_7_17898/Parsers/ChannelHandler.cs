using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            var channelLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.ReadByteE<ChatNotificationType>("Notification Type");

            if (type == ChatNotificationType.InvalidName) // hack, because of some silly reason this type
                packet.ReadBytes(3);                      // has 3 null bytes before the invalid channel name

            packet.ReadCString("Channel Name");

            switch (type)
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
                case ChatNotificationType.TrialRestricted:
                {
                    packet.ReadGuid("GUID");
                    packet.ReadInt32("RealmId");
                    break;
                }
                case ChatNotificationType.YouJoined:
                {
                    packet.ReadByteE<ChannelFlag>("Flags");
                    packet.ReadInt32("Channel Id");
                    packet.ReadInt32("Unk");
                    break;
                }
                case ChatNotificationType.YouLeft:
                {
                    packet.ReadInt32("Channel Id");
                    packet.ReadBool("Unk");
                    break;
                }
                case ChatNotificationType.PlayerNotFound:
                case ChatNotificationType.ChannelOwner:
                case ChatNotificationType.PlayerNotBanned:
                case ChatNotificationType.PlayerInvited:
                case ChatNotificationType.PlayerInviteBanned:
                {
                    packet.ReadCString("Player Name");
                    break;
                }
                case ChatNotificationType.ModeChange:
                {
                    packet.ReadGuid("GUID");
                    packet.ReadByteE<ChannelMemberFlag>("Old Flags");
                    packet.ReadByteE<ChannelMemberFlag>("New Flags");
                    break;
                }
                case ChatNotificationType.PlayerKicked:
                case ChatNotificationType.PlayerBanned:
                case ChatNotificationType.PlayerUnbanned:
                {
                    packet.ReadGuid("Bad");
                    packet.ReadGuid("Good");
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

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            var passwordLength = packet.ReadBits(7);
            packet.ReadBit("Joined by zone update");
            var channelLength = packet.ReadBits(7);
            packet.ReadBit("Has Voice");
            packet.ReadWoWString("Channel Pass", passwordLength);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            var channelLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.ReadByte("Type");
            packet.ReadCString("Channel Name");
            packet.ReadByteE<ChannelFlag>("Flags");
            var count = packet.ReadInt32("Counter");
            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("Player GUID " + i);
                packet.ReadByteE<ChannelMemberFlag>("Player Flags " + i);
                packet.ReadUInt32("unk");
            }
        }
    }
}