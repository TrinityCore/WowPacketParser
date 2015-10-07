using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class BattlenetParserAttribute : Attribute
    {
        private BattlenetParserAttribute(ushort opcode, BattlenetChannel channel, Direction direction)
        {
            Header = new BattlenetPacketHeader
            {
                Opcode = opcode,
                Channel = channel,
                Direction = direction
            };
        }

        public BattlenetParserAttribute(AuthenticationClientCommand command)
            : this((ushort)command, BattlenetChannel.Authentication, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(AuthenticationServerCommand command)
            : this((ushort)command, BattlenetChannel.Authentication, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(ConnectionClientCommand command)
            : this((ushort)command, BattlenetChannel.Connection, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(ConnectionServerCommand command)
            : this((ushort)command, BattlenetChannel.Connection, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(WoWRealmClientCommand command)
            : this((ushort)command, BattlenetChannel.WoWRealm, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(WoWRealmServerCommand command)
            : this((ushort)command, BattlenetChannel.WoWRealm, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(FriendsClientCommand command)
            : this((ushort)command, BattlenetChannel.Friends, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(FriendsServerCommand command)
            : this((ushort)command, BattlenetChannel.Friends, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(PresenceClientCommand command)
            : this((ushort)command, BattlenetChannel.Presence, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(PresenceServerCommand command)
            : this((ushort)command, BattlenetChannel.Presence, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(ChatClientCommand command)
            : this((ushort)command, BattlenetChannel.Chat, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(ChatServerCommand command)
            : this((ushort)command, BattlenetChannel.Chat, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(SupportClientCommand command)
            : this((ushort)command, BattlenetChannel.Support, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(SupportServerCommand command)
            : this((ushort)command, BattlenetChannel.Support, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(AchievementClientCommand command)
            : this((ushort)command, BattlenetChannel.Achievement, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(AchievementServerCommand command)
            : this((ushort)command, BattlenetChannel.Achievement, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(CacheClientCommand command)
            : this((ushort)command, BattlenetChannel.Cache, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(CacheServerCommand command)
            : this((ushort)command, BattlenetChannel.Cache, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetParserAttribute(ProfileClientCommand command)
            : this((ushort)command, BattlenetChannel.Profile, Direction.BNClientToServer) { CommandName = command.ToString(); }
        public BattlenetParserAttribute(ProfileServerCommand command)
            : this((ushort)command, BattlenetChannel.Profile, Direction.BNServerToClient) { CommandName = command.ToString(); }

        public BattlenetPacketHeader Header { get; private set; }
        public string CommandName { get; private set; }
    }
}
