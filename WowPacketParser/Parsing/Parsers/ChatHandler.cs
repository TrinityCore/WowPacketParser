using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

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
            packet.ReadEnum<EmoteType>("Emote ID", TypeCode.Int32);
            packet.ReadGuid("GUID");
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
            var type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);
            var language = packet.ReadEnum<Language>("Language", TypeCode.Int32);
            packet.ReadGuid("GUID");
            packet.ReadInt32("Constant time");

            switch (type)
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
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                case ChatMessageType.BattlegroundLeader:
                case ChatMessageType.Achievement:
                case ChatMessageType.GuildAchievement:
                case ChatMessageType.Restricted:
                case ChatMessageType.Dnd:
                {
                    if (type == ChatMessageType.Channel)
                        packet.ReadCString("Channel Name");

                    packet.ReadGuid("Sender GUID");
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
                    packet.ReadCString("Name");

                    var target = packet.ReadGuid("Receiver GUID");
                    if (target.GetHighType() == HighGuidType.Unit || target.GetHighType() == HighGuidType.Vehicle)
                    {
                        packet.ReadInt32("Receiver Name Length");
                        packet.ReadCString("Receiver Name");
                    }

                    if (target.GetHighType() == HighGuidType.GameObject)
                    {
                        packet.ReadInt32("Unk Int32 GO");
                        packet.ReadByte("Unk Byte GO");
                    }
                    break;
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545) && language == Language.Addon)
                packet.ReadCString("Addon Message Prefix");

            packet.ReadInt32("Text Length");
            packet.ReadCString("Text");
            packet.ReadEnum<ChatTag>("Chat Tag", TypeCode.Byte);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
            {
                if (type == ChatMessageType.RaidBossEmote || type == ChatMessageType.RaidBossWhisper)
                {
                    packet.ReadSingle("Unk single");
                    packet.ReadByte("Unk byte");
                }
            }

            if (type == ChatMessageType.Achievement || type == ChatMessageType.GuildAchievement)
                packet.ReadInt32("Achievement ID");
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
