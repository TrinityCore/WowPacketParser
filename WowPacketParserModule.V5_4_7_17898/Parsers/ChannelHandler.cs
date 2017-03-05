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
            var channelLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.Translator.ReadByteE<ChatNotificationType>("Notification Type");

            if (type == ChatNotificationType.InvalidName) // hack, because of some silly reason this type
                packet.Translator.ReadBytes(3);                      // has 3 null bytes before the invalid channel name

            packet.Translator.ReadCString("Channel Name");

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
                    packet.Translator.ReadGuid("GUID");
                    packet.Translator.ReadInt32("RealmId");
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
            packet.Translator.ReadInt32("Channel Id");
            var passwordLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit("Joined by zone update");
            var channelLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadBit("Has Voice");
            packet.Translator.ReadWoWString("Channel Pass", passwordLength);
            packet.Translator.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.Translator.ReadInt32("Channel Id");
            var channelLength = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Channel Name", channelLength);
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
                packet.Translator.ReadUInt32("unk");
            }
        }
    }
}