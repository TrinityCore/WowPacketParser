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
            : this((ushort)command, BattlenetChannel.Authentication, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(AuthenticationServerCommand command)
            : this((ushort)command, BattlenetChannel.Authentication, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(ConnectionClientCommand command)
            : this((ushort)command, BattlenetChannel.Connection, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(ConnectionServerCommand command)
            : this((ushort)command, BattlenetChannel.Connection, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(WoWRealmClientCommand command)
            : this((ushort)command, BattlenetChannel.WoWRealm, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(WoWRealmServerCommand command)
            : this((ushort)command, BattlenetChannel.WoWRealm, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(FriendsClientCommand command)
            : this((ushort)command, BattlenetChannel.Friends, Direction.BNClientToServer) {  }
        public BattlenetParserAttribute(FriendsServerCommand command)
            : this((ushort)command, BattlenetChannel.Friends, Direction.BNServerToClient) {  }

        public BattlenetParserAttribute(PresenceClientCommand command)
            : this((ushort)command, BattlenetChannel.Presence, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(PresenceServerCommand command)
            : this((ushort)command, BattlenetChannel.Presence, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(ChatClientCommand command)
            : this((ushort)command, BattlenetChannel.Chat, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(ChatServerCommand command)
            : this((ushort)command, BattlenetChannel.Chat, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(SupportClientCommand command)
            : this((ushort)command, BattlenetChannel.Support, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(SupportServerCommand command)
            : this((ushort)command, BattlenetChannel.Support, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(AchievementClientCommand command)
            : this((ushort)command, BattlenetChannel.Achievement, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(AchievementServerCommand command)
            : this((ushort)command, BattlenetChannel.Achievement, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(CacheClientCommand command)
            : this((ushort)command, BattlenetChannel.Cache, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(CacheServerCommand command)
            : this((ushort)command, BattlenetChannel.Cache, Direction.BNServerToClient) { }

        public BattlenetParserAttribute(ProfileClientCommand command)
            : this((ushort)command, BattlenetChannel.Profile, Direction.BNClientToServer) { }
        public BattlenetParserAttribute(ProfileServerCommand command)
            : this((ushort)command, BattlenetChannel.Profile, Direction.BNServerToClient) { }

        public BattlenetPacketHeader Header { get; private set; }
    }
}
