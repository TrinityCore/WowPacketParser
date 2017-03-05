using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_CHAT_NOT_IN_PARTY)]
        public static void HandleChatNotInParty(Packet packet)
        {
            packet.Translator.ReadInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.Translator.ReadUInt32<ZoneId>("Zone Id");
            packet.Translator.ReadInt32("Message Length");
            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_REPORT_IGNORED)]
        public static void HandleChatIgnored(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmoteClient(Packet packet)
        {
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var emote = packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            var guid = packet.Translator.ReadGuid("GUID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.Translator.ReadInt32E<EmoteType>("Emote ID");
            packet.Translator.ReadInt32("Name length");
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText
            {
                Type = packet.Translator.ReadByteE<ChatMessageType>("Type"),
                Language = packet.Translator.ReadInt32E<Language>("Language"),
                SenderGUID = packet.Translator.ReadGuid("GUID")
            };

            packet.Translator.ReadInt32("Constant time");

            switch (text.Type)
            {
                case ChatMessageType.Channel:
                {
                    packet.Translator.ReadCString("Channel Name");
                    goto case ChatMessageType.Say;
                }
                case ChatMessageType.Say:
                case ChatMessageType.Yell:
                case ChatMessageType.Party:
                case ChatMessageType.PartyLeader:
                case ChatMessageType.Raid:
                case ChatMessageType.RaidLeader:
                case ChatMessageType.RaidWarning:
                case ChatMessageType.Guild:
                case ChatMessageType.Officer:
                case ChatMessageType.Emote:
                case ChatMessageType.TextEmote:
                case ChatMessageType.Whisper:
                case ChatMessageType.WhisperInform:
                case ChatMessageType.System:
                case ChatMessageType.Battleground:
                case ChatMessageType.BattlegroundLeader:
                case ChatMessageType.Achievement:
                case ChatMessageType.GuildAchievement:
                case ChatMessageType.Restricted:
                case ChatMessageType.Dnd:
                case ChatMessageType.Afk:
                case ChatMessageType.Ignored:
                {
                    packet.Translator.ReadGuid("Sender GUID");
                    break;
                }
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                {
                    var target = packet.Translator.ReadGuid("Sender GUID");
                    switch (target.GetHighType())
                    {
                        case HighGuidType.Creature:
                        case HighGuidType.Vehicle:
                        case HighGuidType.GameObject:
                        case HighGuidType.Transport:
                        case HighGuidType.Pet:
                            packet.Translator.ReadInt32("Sender Name Length");
                            packet.Translator.ReadCString("Sender Name");
                            break;
                    }
                    break;
                }
                case ChatMessageType.MonsterSay:
                case ChatMessageType.MonsterYell:
                case ChatMessageType.MonsterParty:
                case ChatMessageType.MonsterEmote:
                case ChatMessageType.MonsterWhisper:
                case ChatMessageType.RaidBossEmote:
                case ChatMessageType.RaidBossWhisper:
                case ChatMessageType.BattleNet:
                {
                    packet.Translator.ReadInt32("Name Length");
                    text.SenderName = packet.Translator.ReadCString("Name");
                    text.ReceiverGUID = packet.Translator.ReadGuid("Receiver GUID");
                    switch (text.ReceiverGUID.GetHighType())
                    {
                        case HighGuidType.Creature:
                        case HighGuidType.Vehicle:
                        case HighGuidType.GameObject:
                        case HighGuidType.Transport:
                            packet.Translator.ReadInt32("Receiver Name Length");
                            text.ReceiverName = packet.Translator.ReadCString("Receiver Name");
                            break;
                    }
                    break;
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914) && text.Language == Language.Addon)
                packet.Translator.ReadCString("Addon Message Prefix");

            packet.Translator.ReadInt32("Text Length");
            text.Text = packet.Translator.ReadCString("Text");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadInt16E<ChatTag>("Chat Tag");
            else
                packet.Translator.ReadByteE<ChatTag>("Chat Tag");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
            {
                if (text.Type == ChatMessageType.RaidBossEmote || text.Type == ChatMessageType.RaidBossWhisper)
                {
                    packet.Translator.ReadSingle("Unk single");
                    packet.Translator.ReadByte("Unk byte");
                }
            }

            if (text.Type == ChatMessageType.Achievement || text.Type == ChatMessageType.GuildAchievement)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");

            uint entry = 0;
            if (text.SenderGUID.GetObjectType() == ObjectType.Unit)
                entry = text.SenderGUID.GetEntry();
            else if (text.ReceiverGUID != null && text.ReceiverGUID.GetObjectType() == ObjectType.Unit)
                entry = text.ReceiverGUID.GetEntry();

            if (entry != 0)
                Storage.CreatureTexts.Add(entry, text, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            var type = packet.Translator.ReadInt32E<ChatMessageType>("Type");

            packet.Translator.ReadInt32E<Language>("Language");

            switch (type)
            {
                case ChatMessageType.Whisper:
                {
                    packet.Translator.ReadCString("Recipient");
                    break;
                }
                case ChatMessageType.Channel:
                {
                    packet.Translator.ReadCString("Channel");
                    break;
                }
            }

            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY_LEADER)]
        public static void HandleMessageChatParty(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatParty434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatRaidWarning434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.Translator.ReadUInt32E<ChatMessageType>("Type");
            packet.Translator.ReadCString("Message");
            packet.Translator.ReadCString("Receivers Name");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageWhisper434(Packet packet)
        {
            packet.Translator.ReadUInt32E<ChatMessageType>("Type");
            var recvName = packet.Translator.ReadBits(10);
            var msgLen = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("Receivers Name", recvName);
            packet.Translator.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_PARTY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_RAID, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_BATTLEGROUND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddon(Packet packet)
        {
            packet.Translator.ReadCString("Message");
            packet.Translator.ReadCString("Prefix");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_BATTLEGROUND, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddon434(Packet packet)
        {
            var length1 = packet.Translator.ReadBits(9);
            var length2 = packet.Translator.ReadBits(5);
            packet.Translator.ReadWoWString("Message", length1);
            packet.Translator.ReadWoWString("Prefix", length2);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_PARTY, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_RAID, ClientVersionBuild.V4_3_3_15354)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_OFFICER, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddonRaid434(Packet packet)
        {
            var length1 = packet.Translator.ReadBits(5);
            var length2 = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Prefix", length1);
            packet.Translator.ReadWoWString("Message", length2);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddonWhisper(Packet packet)
        {
            packet.Translator.ReadCString("Prefix");
            packet.Translator.ReadCString("Target Name");
            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddonWhisper434(Packet packet)
        {
            var msgLen = packet.Translator.ReadBits(9);
            var prefixLen = packet.Translator.ReadBits(5);
            var targetLen = packet.Translator.ReadBits(10);
            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Prefix", prefixLen);
            packet.Translator.ReadWoWString("Target Name", targetLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageEmote(Packet packet)
        {
            packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageEmote434(Packet packet)
        {
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_OFFICER)]
        public static void HandleClientChatMessageSay(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadWoWString("Message", packet.Translator.ReadBits(9));
            else
                packet.Translator.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatAfk(Packet packet)
        {
            packet.Translator.ReadCString("Away Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatAfk434(Packet packet)
        {
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Away Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatBattleground434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language"); // not confirmed
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatDND434(Packet packet)
        {
            var len = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageChannel(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            packet.Translator.ReadCString("Message");
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.Translator.ReadInt32E<Language>("Language");
            var channelNameLen = packet.Translator.ReadBits(10);
            var msgLen = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("Message", msgLen);
            packet.Translator.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.SMSG_GM_MESSAGECHAT)] // Similar to SMSG_MESSAGECHAT
        public static void HandleGMMessageChat(Packet packet)
        {
            var type = packet.Translator.ReadByteE<ChatMessageType>("Type");
            packet.Translator.ReadInt32E<Language>("Language");
            packet.Translator.ReadGuid("GUID 1");
            packet.Translator.ReadInt32("Constant time");
            packet.Translator.ReadInt32("GM Name Length");
            packet.Translator.ReadCString("GM Name");
            packet.Translator.ReadGuid("GUID 2");
            packet.Translator.ReadInt32("Message Length");
            packet.Translator.ReadCString("Message");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadInt16E<ChatTag>("Chat Tag");
            else
                packet.Translator.ReadByteE<ChatTag>("Chat Tag");

            if (type == ChatMessageType.Achievement || type == ChatMessageType.GuildAchievement)
                packet.Translator.ReadInt32<AchievementId>("Achievement Id");
        }

        [Parser(Opcode.SMSG_CHAT_RESTRICTED)]
        public static void HandleChatRestricted(Packet packet)
        {
            packet.Translator.ReadByteE<ChatRestrictionType>("Restriction");
        }
    }
}
