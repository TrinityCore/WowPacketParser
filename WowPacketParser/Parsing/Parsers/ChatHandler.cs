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
            var id = packet.ReadInt32();
            Console.WriteLine("Emote ID: " + id);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_MESSAGECHAT)]
        public static void HandleServerChatMessage(Packet packet)
        {
            var type = (ChatMessageType)packet.ReadByte();
            Console.WriteLine("Type: " + type);

            var lang = (Language)packet.ReadInt32();
            Console.WriteLine("Language: " + lang);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var unkInt = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkInt);

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
                    {
                        var chanName = packet.ReadCString();
                        Console.WriteLine("Channel Name: " + chanName);
                    }

                    var senderGuid = packet.ReadGuid();
                    Console.WriteLine("Sender GUID: " + senderGuid);
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
                    var nameLen = packet.ReadInt32();
                    Console.WriteLine("Name Length: " + nameLen);

                    var name = packet.ReadCString();
                    Console.WriteLine("Name: " + name);

                    var target = packet.ReadGuid();
                    Console.WriteLine("Receiver GUID: " + guid);

                    if (target.Full != 0)
                    {
                        var tNameLen = packet.ReadInt32();
                        Console.WriteLine("Receiver Name Length: " + tNameLen);

                        var tName = packet.ReadCString();
                        Console.WriteLine("Receiver Name: " + tName);
                    }
                    break;
                }
            }

            var textLen = packet.ReadInt32();
            Console.WriteLine("Text Length: " + textLen);

            var text = packet.ReadCString();
            Console.WriteLine("Text: " + text);

            var chatTag = (ChatTag)packet.ReadByte();
            Console.WriteLine("Chat Tag: " + chatTag);

            if (type != ChatMessageType.Achievement && type != ChatMessageType.GuildAchievement)
                return;

            var achId = packet.ReadInt32();
            Console.WriteLine("Achievement ID: " + achId);
        }

        [Parser(Opcode.CMSG_MESSAGECHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            var type = (ChatMessageType)packet.ReadInt32();
            Console.WriteLine("Type: " + type);

            var lang = (Language)packet.ReadInt32();
            Console.WriteLine("Language: " + lang);

            switch (type)
            {
                case ChatMessageType.Whisper:
                {
                    var to = packet.ReadCString();
                    Console.WriteLine("Recipient: " + to);
                    goto default;
                }
                case ChatMessageType.Channel:
                {
                    var chan = packet.ReadCString();
                    Console.WriteLine("Channel: " + chan);
                    goto default;
                }
                default:
                {
                    var msg = packet.ReadCString();
                    Console.WriteLine("Message: " + msg);
                    break;
                }
            }
        }
    }
}
