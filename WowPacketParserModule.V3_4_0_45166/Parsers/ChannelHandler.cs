﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.SMSG_CHANNEL_LIST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelSendList(Packet packet)
        {
            packet.ReadBit("Display");
            var channelLength = packet.ReadBits(7);

            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");

            var membersCount = packet.ReadInt32("MembersCount");

            packet.ReadWoWString("Channel", channelLength);

            for (var i = 0; i < membersCount; i++)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadUInt32("VirtualRealmAddress", i);
                packet.ReadByte("Flags", i);
            }
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = packet.ReadBitsE<ChatNotificationType>("Type", 6);
            var channelLength = packet.ReadBits(7);
            var senderLength = packet.ReadBits(6);

            packet.ReadPackedGuid128("SenderGuid");
            packet.ReadPackedGuid128("BnetAccountID");

            packet.ReadInt32("SenderVirtualRealm");

            packet.ReadPackedGuid128("TargetGuid");

            packet.ReadUInt32("TargetVirtualRealm");
            packet.ReadInt32("ChatChannelID");

            if (type == ChatNotificationType.ModeChange)
            {
                packet.ReadByteE<ChannelMemberFlag>("OldFlags");
                packet.ReadByteE<ChannelMemberFlag>("NewFlags");
            }

            packet.ReadWoWString("Channel", channelLength);
            packet.ReadWoWString("Sender", senderLength);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var channelLen = packet.ReadBits(7);
            uint channelWelcomeMsgLen = packet.ReadBits(11);

            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadByte("Unknown107");
            packet.ReadInt32("ChatChannelID");
            packet.ReadUInt64("InstanceID");
            packet.ReadPackedGuid128("ChannelGUID");
            packet.ReadWoWString("Channel", channelLen);
            packet.ReadWoWString("ChannelWelcomeMsg", channelWelcomeMsgLen);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.ReadBits(7);
            packet.ReadBit("Suspended");
            packet.ReadInt32("ChatChannelID");
            packet.ReadWoWString("Channel", bits20);
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("ChatChannelId");

            var channelLength = packet.ReadBits(7);
            var passwordLength = packet.ReadBits(7);

            packet.ReadWoWString("ChannelName", channelLength);
            packet.ReadWoWString("Password", passwordLength);
        }

        [Parser(Opcode.SMSG_USERLIST_ADD, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.ReadPackedGuid128("AddedUserGUID");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RemovedUserGUID");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_UPDATE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelUserListUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UpdatedUserGUID");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_ANNOUNCEMENTS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DECLINE_INVITE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_OWNER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var length = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", length);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_BAN, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_KICK, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_MODERATOR, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_ALL, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNBAN, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNMODERATOR, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_ALL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelPassword(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(7);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.ReadInt32("ZoneChannelID");
            var length = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", length);
        }
    }
}
