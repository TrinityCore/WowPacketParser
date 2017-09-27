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
                packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            else
                packet.ReadByteE<ChannelFlag>("ChannelFlags");
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var bits544 = packet.ReadBits(7);
            var bits24 = packet.ReadBits(10);

            ReadChannelFlags(packet);
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

            ReadChannelFlags(packet);
            var int20 = packet.ReadInt32("MembersCount");

            packet.ReadWoWString("Channel", bits108);

            for (var i = 0; i < int20; i++)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadUInt32("VirtualRealmAddress", i);
                packet.ReadByte("Flags", i);
            }
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

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.ReadPackedGuid128("LocalGUID");
            packet.ReadPackedGuid128("SessionGUID");
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            packet.ReadPackedGuid128("AddedUserGUID");

            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            ReadChannelFlags(packet);
            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            packet.ReadPackedGuid128("RemovedUserGUID");

            ReadChannelFlags(packet);
            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }

        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UpdatedUserGUID");

            packet.ReadByteE<ChannelMemberFlag>("UserFlags");
            ReadChannelFlags(packet);
            packet.ReadInt32("ChannelID");

            var len = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", len);
        }
    }
}
