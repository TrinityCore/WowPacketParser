using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_OFF)]
        public static void HandleChannelSetVoice(Packet packet)
        {
            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        [Parser(Opcode.CMSG_CHANNEL_SILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_VOICE)]
        [Parser(Opcode.CMSG_CHANNEL_SILENCE_ALL)]
        [Parser(Opcode.CMSG_CHANNEL_UNSILENCE_ALL)]
        public static void HandleChannelSilencing(Packet packet)
        {
            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            var playerName = packet.ReadCString();
            Console.WriteLine("Player Name: " + playerName);
        }

        [Parser(Opcode.CMSG_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        [Parser(Opcode.SMSG_CHANNEL_LIST)]
        public static void HandleChannelSendList(Packet packet)
        {
            var type = packet.ReadByte();
            Console.WriteLine("Type: " + type);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            var flags = (ChannelFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);

            var count = packet.ReadUInt32();
            Console.WriteLine("Counter: " + count);

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid();
                Console.WriteLine("Player GUID " + i + ": " + guid);

                var playerflags = (ChannelMemberFlag)packet.ReadByte();
                Console.WriteLine("Player Flags " + i + ": " + playerflags);

            }
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY)]
        public static void HandleChannelNotify(Packet packet)
        {
            var type = (ChatNotificationType)packet.ReadByte();
            Console.WriteLine("Notification Type: " + type);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            switch(type)
            {
                case ChatNotificationType.PlayerAlreadyMember:
                case ChatNotificationType.Invite:
                case ChatNotificationType.ModerationOn:
                case ChatNotificationType.ModerationOff:
                case ChatNotificationType.AnnouncementsOn:
                case ChatNotificationType.AnnouncementsOff:
                case ChatNotificationType.PasswordChanged:
                case ChatNotificationType.OwnerChanged:
                case ChatNotificationType.Joined:
                case ChatNotificationType.Left:
                case ChatNotificationType.VoiceOn:
                case ChatNotificationType.VoiceOff:
                    {
                        var guid = packet.ReadGuid();
                        Console.WriteLine("GUID: " + guid);
                    }
                    break;
                case ChatNotificationType.YouJoined:
                    {
                        var flags = (ChannelFlag)packet.ReadByte();
                        Console.WriteLine("Flags: " + flags);

                        var id = packet.ReadUInt32();
                        Console.WriteLine("Id: " + id);

                        var unk1 = packet.ReadUInt32();
                        Console.WriteLine("Unk: " + unk1);
                    }
                    break;
                case ChatNotificationType.YouLeft:
                    {
                        var id = packet.ReadUInt32();
                        Console.WriteLine("Id: " + id);

                        var unk1 = packet.ReadByte();
                        Console.WriteLine("Unk: " + unk1);
                    }
                    break;
                case ChatNotificationType.WrongPassword:
                case ChatNotificationType.NotMember:
                case ChatNotificationType.NotModerator:
                case ChatNotificationType.NotOwner:
                case ChatNotificationType.Muted:
                case ChatNotificationType.Banned:
                case ChatNotificationType.InviteWrongFaction:
                case ChatNotificationType.WrongFaction:
                case ChatNotificationType.InvalidName:
                case ChatNotificationType.NotModerated:
                case ChatNotificationType.Throttled:
                case ChatNotificationType.NotInArea:
                case ChatNotificationType.NotInLfg:
                    break;
                case ChatNotificationType.PlayerNotFound:
                case ChatNotificationType.ChannelOwner:
                case ChatNotificationType.PlayerNotBanned:
                case ChatNotificationType.PlayerInvited:
                case ChatNotificationType.PlayerInviteBanned:
                    {
                        var name = packet.ReadCString();
                        Console.WriteLine("Player Name: " + name);
                    }
                    break;
                case ChatNotificationType.ModeChange:
                    {
                        var guid = packet.ReadGuid();
                        Console.WriteLine("GUID: " + guid);

                        var oldFlags = (ChannelMemberFlag)packet.ReadByte();
                        Console.WriteLine("Old Flags: " + oldFlags);

                        var newFlags = (ChannelMemberFlag)packet.ReadByte();
                        Console.WriteLine("New Flags: " + newFlags);
                    }
                    break;
                case ChatNotificationType.PlayerKicked:
                case ChatNotificationType.PlayerBanned:
                case ChatNotificationType.PlayerUnbanned:
                    {
                        var guid = packet.ReadGuid();
                        Console.WriteLine("Bad: " + guid);

                        var good = packet.ReadGuid();
                        Console.WriteLine("Good: " + good);
                    }
                    break;
                case ChatNotificationType.Unknown1:
                    {
                        var guid = packet.ReadGuid();
                        Console.WriteLine("GUID: " + guid);
                    }
                    break;
            }
        }

        [Parser(Opcode.CMSG_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            var id = packet.ReadUInt32();
            Console.WriteLine("ChannelId: " + id);

            var unk1 = packet.ReadByte();
            Console.WriteLine("Unk1: " + unk1);

            var unk2 = packet.ReadByte();
            Console.WriteLine("Unk2: " + unk2);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            var chanPass = packet.ReadCString();
            Console.WriteLine("Channel Pass: " + chanPass);
        }

        [Parser(Opcode.CMSG_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {
            var id = packet.ReadUInt32();
            Console.WriteLine("ChannelId: " + id);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        [Parser(Opcode.SMSG_USERLIST_REMOVE)]
        public static void HandleChannelUserListRemove(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var flags = (ChannelFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);

            var count = packet.ReadUInt32();
            Console.WriteLine("Counter: " + count);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        [Parser(Opcode.SMSG_USERLIST_ADD)]
        [Parser(Opcode.SMSG_USERLIST_UPDATE)]
        public static void HandleChannelUserListAdd(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var memberFlags = (ChannelMemberFlag)packet.ReadByte();
            Console.WriteLine("Member Flags: " + memberFlags);

            var flags = (ChannelFlag)packet.ReadByte();
            Console.WriteLine("Flags: " + flags);

            var count = packet.ReadUInt32();
            Console.WriteLine("Counter: " + count);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);
        }

        /*
        [Parser(Opcode.CMSG_LEAVE_CHANNEL)]
        public static void HandleChannelLeave(Packet packet)
        {

        }
         */
    }
}
