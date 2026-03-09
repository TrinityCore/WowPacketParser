using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            PacketChat chatPacket = packet.Holder.Chat = new PacketChat();
            var text = new CreatureText
            {
                Type = (ChatMessageType)packet.ReadByteE<ChatMessageTypeNew>("SlashCmd"),
                Language = packet.ReadUInt32E<Language>("Language"),
                SenderGUID = packet.ReadPackedGuid128("SenderGUID")
            };

            packet.ReadPackedGuid128("SenderGuildGUID");
            packet.ReadPackedGuid128("WowAccountGUID");
            text.ReceiverGUID = packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualAddress");
            packet.ReadUInt32("SenderVirtualAddress");
            if (ClientVersion.Branch != ClientBranch.Retail && ClientVersion.RemovedInVersion(ClientVersionBuild.V3_4_2_50129) || ClientVersion.RemovedInVersion(ClientBranch.Retail, ClientVersionBuild.V10_1_0_49407))
                packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("AchievementID");
            var chatFlags = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                chatFlags = packet.ReadUInt16("ChatFlags");
            packet.ReadSingle("DisplayTime");
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V10_1_0_49407) || ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_2_50129))
                packet.ReadInt32<SpellId>("SpellID");

            var senderNameLen = packet.ReadBits(11);
            var receiverNameLen = packet.ReadBits(11);
            var prefixLen = packet.ReadBits(5);
            var channelLen = packet.ReadBits(7);
            var textLen = packet.ReadBits(12);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_2_7_54577))
            {
                int flagLen = 14;
                if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V10_0_7_48676) || ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_2_50129))
                    flagLen = 15;
                chatFlags = packet.ReadBits("ChatFlags", flagLen);
            }

            packet.ReadBit("HideChatLog");
            packet.ReadBit("FakeSenderName");
            var hasBroadcastTextID = packet.ReadBit("HasBroadcastTextID");
            var hasChannelGuid = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                hasChannelGuid = packet.ReadBit("HasChannelGUID");

            var hasEncounterEventID = false;
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V12_0_0_65390))
                hasEncounterEventID = packet.ReadBit("HasEncounterEventID");

            text.SenderName = packet.ReadWoWString("Sender Name", senderNameLen);
            text.ReceiverName = packet.ReadWoWString("Receiver Name", receiverNameLen);
            packet.ReadWoWString("Addon Message Prefix", prefixLen);
            packet.ReadWoWString("Channel Name", channelLen);

            chatPacket.Text = text.Text = packet.ReadWoWString("Text", textLen);
            chatPacket.Sender = text.SenderGUID.ToUniversalGuid();
            chatPacket.Target = text.ReceiverGUID.ToUniversalGuid();
            chatPacket.Language = (int) text.Language;
            chatPacket.Type = (int) text.Type;
            chatPacket.Flags = chatFlags;

            if (hasBroadcastTextID)
                packet.ReadUInt32("BroadcastTextID");

            if (hasChannelGuid)
                packet.ReadPackedGuid128("ChannelGUID");

            if (hasEncounterEventID)
                packet.ReadUInt32("EncounterEventID");

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            PacketEmote packetEmote = packet.Holder.Emote = new PacketEmote();
            var guid = packet.ReadPackedGuid128("GUID");
            var emote = packet.ReadInt32E<EmoteType>("Emote ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                var count = packet.ReadUInt32("SpellVisualKitCount");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                    packet.ReadInt32("SequenceVariation");

                for (var i = 0; i < count; ++i)
                    packet.ReadUInt32("SpellVisualKitID", i);
            }

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);

            packetEmote.Emote = (int) emote;
            packetEmote.Sender = guid.ToUniversalGuid();
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleSendTextEmote(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32E<EmoteTextType>("EmoteID");
            packet.ReadInt32("SoundIndex");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503) ||
                ClientVersion.IsBurningCrusadeClassicClientVersionBuild(ClientVersion.Build) ||
                ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
            {
                var count = packet.ReadUInt32("SpellVisualKitCount");
                if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V9_2_0_42423) ||
                    ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_2_42065) ||
                    ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                    ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166))
                    packet.ReadInt32("SequenceVariation");

                for (var i = 0; i < count; ++i)
                    packet.ReadUInt32("SpellVisualKitID", i);
            }
        }
    }
}
