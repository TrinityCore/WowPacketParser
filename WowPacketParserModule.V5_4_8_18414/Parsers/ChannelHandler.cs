using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHANNEL_SILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_SILENCE_ALL)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_ALL)]
        public static void HandleChannelSilencing(Packet packet)
        {
            /*packet.ReadCString("Channel Name");
            packet.ReadCString("Player Name");*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_CHANNEL_LIST)]
        [Parser(Opcode.CMSG_CHANNEL_OWNER)]
        [Parser(Opcode.CMSG_CHANNEL_ANNOUNCEMENTS)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_OFF)]
        [Parser(Opcode.CMSG_SET_CHANNEL_WATCH)]
        [Parser(Opcode.CMSG_DECLINE_CHANNEL_INVITE)]
        [Parser(Opcode.CMSG_CHANNEL_DISPLAY_LIST)]
        public static void HandleChannelMisc(Packet packet)
        {
            //packet.ReadCString("Channel Name");
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CHANNEL_MEMBER_COUNT)]
        public static void HandleChannelMemberCount(Packet packet)
        {
            /*packet.ReadCString("Channel Name");
            packet.ReadEnum<ChannelFlag>("Flags", TypeCode.Byte);
            packet.ReadInt32("Unk int32");*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            /*var type = packet.ReadEnum<ChatNotificationType>("Notification Type", TypeCode.Byte);

            if (type == ChatNotificationType.InvalidName) // hack, because of some silly reason this type
                packet.ReadBytes(3);                      // has 3 null bytes before the invalid channel name

            packet.ReadCString("Channel Name");

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
                    packet.ReadGuid("GUID");
                    packet.ReadInt32("unk");
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
                case ChatNotificationType.Unknown1:
                {
                    packet.ReadGuid("GUID");
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
            }*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_CHANNEL_BAN)]
        public static void HandleChannelBan(Packet packet)
        {
            /*var channelLength = packet.ReadBits(8);
            var nameLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel", channelLength);
            packet.ReadWoWString("Player to ban", nameLength);*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_JOIN_CHANNEL)]
        public static void HandleJoinChannel(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadInt32("Channel Id");
                packet.ReadBit("Joined by zone update");
                var len1 = packet.ReadBits("len1", 7);
                var len2 = packet.ReadBits("len2", 7);
                packet.ReadBit("Has Voice");
                packet.ResetBitReader();
                packet.ReadWoWString("Channel Name", len1);
                packet.ReadWoWString("Channel Pass", len2);
            }
            else
            {
                packet.WriteLine("              : SMSG_GMRESPONSE_RECEIVED");
                var count = packet.ReadBits("count", 20);
                var guid = new byte[count][];
                var unk2085 = new bool[count];
                var unk1045 = new bool[count];
                var unk1048 = new bool[count];
                var unk1061 = new uint[count];
                var unk17 = new uint[count];
                for (var i = 0; i < count; i++)
                {
                    guid[i] = new byte[8];
                    unk17[i] = packet.ReadBits("unk17*4", 11, i);
                    unk2085[i] = !packet.ReadBit("!unk2085*4", i);
                    unk1061[i] = packet.ReadBits("unk1061*4", 10, i);
                    unk1045[i] = !packet.ReadBit("!unk1045*4", i);
                    unk1048[i] = !packet.ReadBit("!unk1048*4", i);
                    guid[i] = packet.StartBitStream(5, 0, 1, 2, 4, 7, 6, 3);
                }
                for (var i = 0; i < count; i++)
                {
                    packet.ParseBitStream(guid[i], 7, 0, 5, 4, 3, 6, 2, 1);
                    packet.ReadInt32("unk36", i);
                    packet.ReadWoWString("Ticket", unk17[i], i);
                    packet.ReadInt32("unk52", i);
                    if (unk2085[i])
                        packet.ReadInt32("unk2085*4", i);
                    packet.ReadInt32("unk20", i);
                    if (unk1045[i])
                        packet.ReadInt32("unk1045*4", i);
                    packet.ReadWoWString("Response", unk1061[i], i);
                    packet.WriteGuid("Guid", guid[i], i);
                }
                packet.ReadInt32("unk32");
                packet.ReadInt32("unk36");
            }
        }

        [Parser(Opcode.CMSG_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            /*packet.ReadInt32("Channel Id");
            packet.ReadCString("Channel Name");*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            /*packet.ReadGuid("GUID");
            packet.ReadEnum<ChannelFlag>("Flags", TypeCode.Byte);
            packet.ReadInt32("Counter");
            packet.ReadCString("Channel Name");*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            /*packet.ReadGuid("GUID");
            packet.ReadEnum<ChannelMemberFlag>("Member Flags", TypeCode.Byte);
            packet.ReadEnum<ChannelFlag>("Flags", TypeCode.Byte);
            packet.ReadInt32("Counter");
            packet.ReadCString("Channel Name");*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_CHANNEL_PASSWORD)]
        public static void HandleChannelPassword(Packet packet)
        {
            /*var channelLen = packet.ReadBits(8);
            var passLen = packet.ReadBits(7);

            packet.ReadWoWString("Channel Name", channelLen);
            packet.ReadWoWString("Password", passLen);*/
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_CHANNEL_INVITE)]
        public static void HandleChannelInvite(Packet packet)
        {
            /*var nameLen = packet.ReadBits(7);
            var channelLen = packet.ReadBits(8);

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("Channel Name", channelLen);*/
            packet.ReadToEnd();
        }
    }
}
