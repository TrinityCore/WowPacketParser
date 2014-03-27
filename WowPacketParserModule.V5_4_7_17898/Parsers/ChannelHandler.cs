using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            var pwLen = packet.ReadBits(7);
            packet.ReadBit();
            var nameLen = packet.ReadBits(7);
            packet.ReadBit();

            packet.ReadWoWString("Password", pwLen);
            packet.ReadWoWString("Channel Name", nameLen);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.ReadEnum<ChatNotificationType>("Notification Type", TypeCode.Byte);

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
                case ChatNotificationType.Unknown1:
                    {
                        packet.ReadGuid("GUID");
                        packet.ReadInt32("RealmId");
                        break;
                    }
                case ChatNotificationType.YouJoined:
                    {
                        packet.ReadEnum<ChannelFlag>("Flags", TypeCode.Byte);
                        packet.ReadInt32("Channel Id");
                        packet.ReadInt32("Unk");
                        break;
                    }
                case ChatNotificationType.YouLeft:
                    {
                        packet.ReadInt32("Channel Id");
                        packet.ReadBoolean("Unk");
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
                        packet.ReadEnum<ChannelMemberFlag>("Old Flags", TypeCode.Byte);
                        packet.ReadEnum<ChannelMemberFlag>("New Flags", TypeCode.Byte);
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
    }
}