using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.SMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            packet.ReadInt32("Emote ID");

            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var type = packet.ReadEnum<ChatMessageType>("Type", TypeCode.Byte);

            packet.ReadEnum<Language>("Language", TypeCode.Int32);

            packet.ReadGuid("GUID");

            packet.ReadInt32("Unk Int32");

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

                    if (target.GetHighType() == HighGuidType.Unit)
                    {
                        packet.ReadInt32("Receiver Name Length");

                        packet.ReadCString("Receiver Name");
                    }
                    break;
                }
            }

            packet.ReadInt32("Text Length");

            packet.ReadCString("Text");

            packet.ReadEnum<ChatTag>("Chat Tag", TypeCode.Byte);

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
    }
}
