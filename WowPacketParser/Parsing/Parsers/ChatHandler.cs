using System;
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
            packet.ReadInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_DEFENSE_MESSAGE)]
        public static void HandleDefenseMessage(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadInt32("Message Length");
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_IGNORED)]
        public static void HandleChatIgnored(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmoteClient(Packet packet)
        {
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var emote = packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            var guid = packet.ReadGuid("GUID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.TryAdd(guid.GetEntry(), Tuple.Create(emote, packet.Time));
        }

        [Parser(Opcode.CMSG_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            packet.ReadInt32("Text Emote ID");
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32("Text Emote ID");
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            packet.ReadInt32("Name length");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOT_FOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_PARTY)]
        public static void HandleMessageChatParty(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadCString("Text");
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText();

            text.Type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);
            text.Language = packet.ReadEnum<Language>("Language", TypeCode.Int32);
            var guid = packet.ReadGuid("GUID");

            uint entry = 0;
            if (guid.GetObjectType() == ObjectType.Unit)
                entry = guid.GetEntry();

            packet.ReadInt32("Constant time");

            switch (text.Type)
            {
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
                case ChatMessageType.Channel:
                case ChatMessageType.Battleground:
                case ChatMessageType.BattlegroundLeader:
                case ChatMessageType.Achievement:
                case ChatMessageType.GuildAchievement:
                case ChatMessageType.Restricted:
                case ChatMessageType.Dnd:
                {
                    if (text.Type == ChatMessageType.Channel)
                        packet.ReadCString("Channel Name");

                    packet.ReadGuid("Sender GUID");
                    break;
                }
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                {
                    var target = packet.ReadGuid("Sender GUID");
                    switch (target.GetHighType())
                    {
                        case HighGuidType.Unit:
                        case HighGuidType.Vehicle:
                        case HighGuidType.GameObject:
                        case HighGuidType.Transport:
                        case HighGuidType.Pet:
                            packet.ReadInt32("Sender Name Length");
                            packet.ReadCString("Sender Name");
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
                    packet.ReadInt32("Name Length");
                    text.Comment = packet.ReadCString("Name");

                    var target = packet.ReadGuid("Receiver GUID");
                    switch (target.GetHighType())
                    {
                        case HighGuidType.Unit:
                        case HighGuidType.Vehicle:
                        case HighGuidType.GameObject:
                        case HighGuidType.Transport:
                            packet.ReadInt32("Receiver Name Length");
                            text.Comment += " to " + packet.ReadCString("Receiver Name");
                            break;
                    }
                    break;
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914) && text.Language == Language.Addon)
                packet.ReadCString("Addon Message Prefix");

            packet.ReadInt32("Text Length");
            text.Text = packet.ReadCString("Text");
            packet.ReadEnum<ChatTag>("Chat Tag", TypeCode.Byte);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
            {
                if (text.Type == ChatMessageType.RaidBossEmote || text.Type == ChatMessageType.RaidBossWhisper)
                {
                    packet.ReadSingle("Unk single");
                    packet.ReadByte("Unk byte");
                }
            }

            if (text.Type == ChatMessageType.Achievement || text.Type == ChatMessageType.GuildAchievement)
                packet.ReadInt32("Achievement ID");

            if (entry != 0)
                Storage.CreatureTexts.TryAdd(entry, Tuple.Create(text, packet.Time));
        }

        [Parser(Opcode.CMSG_MESSAGECHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            var type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Int32);

            packet.ReadEnum<Language>("Language", TypeCode.Int32);

            switch (type)
            {
                case ChatMessageType.Whisper:
                {
                    packet.ReadCString("Recipient");
                    break;
                }
                case ChatMessageType.Channel:
                {
                    packet.ReadCString("Channel");
                    break;
                }
            }

            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadEnum<ChatMessageType>("Type", TypeCode.UInt32);
            packet.ReadCString("Message");
            packet.ReadCString("Receivers Name");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_PARTY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_RAID)]
        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_BATTLEGROUND)]
        public static void HandleClientChatMessageAddon(Packet packet)
        {
            packet.ReadCString("Message");
            packet.ReadCString("Prefix");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_ADDON_WHISPER)]
        public static void HandleClientChatMessageAddonWhisper(Packet packet)
        {
            packet.ReadCString("Prefix");
            packet.ReadCString("Target Name");
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_EMOTE)]
        public static void HandleClientChatMessageEmote(Packet packet)
        {
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_GUILD)]
        [Parser(Opcode.CMSG_MESSAGECHAT_YELL)]
        [Parser(Opcode.CMSG_MESSAGECHAT_SAY)]
        [Parser(Opcode.CMSG_MESSAGECHAT_RAID)]
        [Parser(Opcode.CMSG_MESSAGECHAT_OFFICER)]
        public static void HandleClientChatMessageSay(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            // 4.3.0 some kind of strlen is send before string, size of 2 bytes
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_AFK)]
        public static void HandleMessageChatAfk(Packet packet)
        {
            packet.ReadCString("Away Message");
        }

        [Parser(Opcode.CMSG_MESSAGECHAT_CHANNEL)]
        public static void HandleClientChatMessageChannel(Packet packet)
        {
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadCString("Message");
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.SMSG_GM_MESSAGECHAT)] // Similar to SMSG_MESSAGECHAT
        public static void HandleGMMessageChat(Packet packet)
        {
            packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);
            packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadGuid("GUID 1");
            packet.ReadInt32("Constant time");
            packet.ReadInt32("GM Name Length");
            packet.ReadCString("GM Name");
            packet.ReadGuid("GUID 2");
            packet.ReadInt32("Message Length");
            packet.ReadCString("Message");
            packet.ReadEnum<ChatTag>("Chat Tag", TypeCode.Byte);
        }

        [Parser(Opcode.SMSG_CHAT_RESTRICTED)]
        public static void HandleChatRestricted(Packet packet)
        {
            packet.ReadEnum<ChatRestrictionType>("Restriction", TypeCode.Byte);
        }
    }
}
