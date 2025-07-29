using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.SMSG_USERLIST_ADD)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.ReadPackedGuid128("AddedUserGUID");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RemovedUserGUID");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UpdatedUserGUID");
            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadUInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
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

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED)]
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

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.ReadBits(7);
            packet.ReadBit("Suspended");
            packet.ReadInt32("ChatChannelID");
            packet.ReadWoWString("Channel", bits20);
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
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
    }
}
