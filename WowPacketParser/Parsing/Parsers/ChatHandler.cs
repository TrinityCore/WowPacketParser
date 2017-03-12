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
            packet.ReadUInt32<ZoneId>("Zone Id");
            packet.ReadInt32("Message Length");
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_REPORT_IGNORED)]
        public static void HandleChatIgnored(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmoteClient(Packet packet)
        {
            packet.ReadInt32E<EmoteType>("Emote ID");
        }

        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            var emote = packet.ReadInt32E<EmoteType>("Emote ID");
            var guid = packet.ReadGuid("GUID");

            if (guid.GetObjectType() == ObjectType.Unit)
                Storage.Emotes.Add(guid, emote, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SEND_TEXT_EMOTE)]
        public static void HandleTextEmote(Packet packet)
        {
            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadInt32E<EmoteType>("Emote ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_TEXT_EMOTE)]
        public static void HandleTextEmoteServer(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32E<EmoteTextType>("Text Emote ID");
            packet.ReadInt32E<EmoteType>("Emote ID");
            packet.ReadInt32("Name length");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAT_PLAYER_NOTFOUND)]
        public static void HandleChatPlayerNotFound(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var text = new CreatureText
            {
                Type = packet.ReadByteE<ChatMessageType>("Type"),
                Language = packet.ReadInt32E<Language>("Language"),
                SenderGUID = packet.ReadGuid("GUID")
            };

            packet.ReadInt32("Constant time");

            switch (text.Type)
            {
                case ChatMessageType.Channel:
                {
                    packet.ReadCString("Channel Name");
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
                        case HighGuidType.Creature:
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
                    text.SenderName = packet.ReadCString("Name");
                    text.ReceiverGUID = packet.ReadGuid("Receiver GUID");
                    switch (text.ReceiverGUID.GetHighType())
                    {
                        case HighGuidType.Creature:
                        case HighGuidType.Vehicle:
                        case HighGuidType.GameObject:
                        case HighGuidType.Transport:
                            packet.ReadInt32("Receiver Name Length");
                            text.ReceiverName = packet.ReadCString("Receiver Name");
                            break;
                    }
                    break;
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914) && text.Language == Language.Addon)
                packet.ReadCString("Addon Message Prefix");

            packet.ReadInt32("Text Length");
            text.Text = packet.ReadCString("Text");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadInt16E<ChatTag>("Chat Tag");
            else
                packet.ReadByteE<ChatTag>("Chat Tag");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
            {
                if (text.Type == ChatMessageType.RaidBossEmote || text.Type == ChatMessageType.RaidBossWhisper)
                {
                    packet.ReadSingle("Unk single");
                    packet.ReadByte("Unk byte");
                }
            }

            if (text.Type == ChatMessageType.Achievement || text.Type == ChatMessageType.GuildAchievement)
                packet.ReadInt32<AchievementId>("Achievement Id");

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
            var type = packet.ReadInt32E<ChatMessageType>("Type");

            packet.ReadInt32E<Language>("Language");

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

        [Parser(Opcode.SMSG_GM_MESSAGECHAT)] // Similar to SMSG_MESSAGECHAT
        public static void HandleGMMessageChat(Packet packet)
        {
            var type = packet.ReadByteE<ChatMessageType>("Type");
            packet.ReadInt32E<Language>("Language");
            packet.ReadGuid("GUID 1");
            packet.ReadInt32("Constant time");
            packet.ReadInt32("GM Name Length");
            packet.ReadCString("GM Name");
            packet.ReadGuid("GUID 2");
            packet.ReadInt32("Message Length");
            packet.ReadCString("Message");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadInt16E<ChatTag>("Chat Tag");
            else
                packet.ReadByteE<ChatTag>("Chat Tag");

            if (type == ChatMessageType.Achievement || type == ChatMessageType.GuildAchievement)
                packet.ReadInt32<AchievementId>("Achievement Id");
        }

        [Parser(Opcode.SMSG_CHAT_RESTRICTED)]
        public static void HandleChatRestricted(Packet packet)
        {
            packet.ReadByteE<ChatRestrictionType>("Restriction");
        }
    }
}
