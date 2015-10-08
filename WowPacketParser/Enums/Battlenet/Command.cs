using System;
using System.Collections.Generic;

namespace WowPacketParser.Enums.Battlenet
{
    public enum AuthenticationClientCommand : ushort
    {
        LogonRequest = 0,
        ResumeRequest = 1,
        ProofResponse = 2,
        GenerateSingleSignOnTokenRequest2 = 8,
        LogonRequest3 = 9,
        SingleSignOnRequest3 = 10
    }

    public enum AuthenticationServerCommand : ushort
    {
        LogonResponse3 = 0,
        ResumeResponse = 1,
        ProofRequest = 2,
        Patch = 3,
        AuthorizedLicenses = 4,
        GenerateSingleSignOnTokenResponse2 = 8
    }

    public enum ConnectionClientCommand : ushort
    {
        Ping = 0,
        EnableEncyption = 5,
        LogoutRequest = 6,
        DisconnectRequest = 7,
        ConnectionClosing = 9
    }

    public enum ConnectionServerCommand : ushort
    {
        Pong = 0,
        Boom = 1,
        RegulatorUpdate = 2,
        ServerVersion = 3,
        STUNServers = 4
    }

    public enum WoWRealmClientCommand : ushort
    {
        ListSubscribeRequest = 0,
        ListUnsubscribe = 1,
        JoinRequestV2 = 8,
        MultiLogonRequestV2 = 9
    }

    public enum WoWRealmServerCommand : ushort
    {
        ListSubscribeResponse = 0,
        ListUpdate = 2,
        ListComplete = 3,
        ToonReady = 6,
        ToonLoggedOut = 7,
        JoinResponseV2 = 8
    }

    public enum FriendsClientCommand : ushort
    {
    }

    public enum FriendsServerCommand : ushort
    {
    }

    public enum PresenceClientCommand : ushort
    {
    }

    public enum PresenceServerCommand : ushort
    {
    }

    public enum ChatClientCommand : ushort
    {
    }

    public enum ChatServerCommand : ushort
    {
    }

    public enum SupportClientCommand : ushort
    {
    }

    public enum SupportServerCommand : ushort
    {
    }

    public enum AchievementClientCommand : ushort
    {
    }

    public enum AchievementServerCommand : ushort
    {
    }

    public enum CacheClientCommand : ushort
    {
        GatewayLookupRequest = 2,
        ConnectRequest = 4,
        DataChunk = 7,
        GetStreamItemsRequest = 9
    }

    public enum CacheServerCommand : ushort
    {
        GatewayLookupResponse = 3,
        ConnectResponse = 4,
        PublishListResponse = 7,
        Result = 8,
        GetStreamItemsResponse = 9,
        GetConfigHandleResponse = 10
    }

    public enum ProfileClientCommand : ushort
    {
    }

    public enum ProfileServerCommand : ushort
    {
        ReadResponse = 0,
        AddressQueryResponse = 1,
        ResolveToonHandleToNameResponse = 2,
        ResolveToonNameToHandleResponse = 3,
        SettingsAvailable = 4,
    }

    public static class CommandNames
    {
        private static readonly Dictionary<Tuple<BattlenetChannel, Direction>, Type> _commandTypes = new Dictionary<Tuple<BattlenetChannel, Direction>, Type>()
        {
            { Tuple.Create(BattlenetChannel.Authentication, Direction.BNClientToServer), typeof(AuthenticationClientCommand) },
            { Tuple.Create(BattlenetChannel.Authentication, Direction.BNServerToClient), typeof(AuthenticationServerCommand) },
            { Tuple.Create(BattlenetChannel.Connection, Direction.BNClientToServer), typeof(ConnectionClientCommand) },
            { Tuple.Create(BattlenetChannel.Connection, Direction.BNServerToClient), typeof(ConnectionServerCommand) },
            { Tuple.Create(BattlenetChannel.WoWRealm, Direction.BNClientToServer), typeof(WoWRealmClientCommand) },
            { Tuple.Create(BattlenetChannel.WoWRealm, Direction.BNServerToClient), typeof(WoWRealmServerCommand) },
            { Tuple.Create(BattlenetChannel.Friends, Direction.BNClientToServer), typeof(FriendsClientCommand) },
            { Tuple.Create(BattlenetChannel.Friends, Direction.BNServerToClient), typeof(FriendsServerCommand) },
            { Tuple.Create(BattlenetChannel.Presence, Direction.BNClientToServer), typeof(PresenceClientCommand) },
            { Tuple.Create(BattlenetChannel.Presence, Direction.BNServerToClient), typeof(PresenceServerCommand) },
            { Tuple.Create(BattlenetChannel.Chat, Direction.BNClientToServer), typeof(ChatClientCommand) },
            { Tuple.Create(BattlenetChannel.Chat, Direction.BNServerToClient), typeof(ChatServerCommand) },
            { Tuple.Create(BattlenetChannel.Support, Direction.BNClientToServer), typeof(SupportClientCommand) },
            { Tuple.Create(BattlenetChannel.Support, Direction.BNServerToClient), typeof(SupportServerCommand) },
            { Tuple.Create(BattlenetChannel.Achievement, Direction.BNClientToServer), typeof(AchievementClientCommand) },
            { Tuple.Create(BattlenetChannel.Achievement, Direction.BNServerToClient), typeof(AchievementServerCommand) },
            { Tuple.Create(BattlenetChannel.Cache, Direction.BNClientToServer), typeof(CacheClientCommand) },
            { Tuple.Create(BattlenetChannel.Cache, Direction.BNServerToClient), typeof(CacheServerCommand) },
            { Tuple.Create(BattlenetChannel.Profile, Direction.BNClientToServer), typeof(ProfileClientCommand) },
            { Tuple.Create(BattlenetChannel.Profile, Direction.BNServerToClient), typeof(ProfileServerCommand) },
        };

        public static string Get(ushort command, BattlenetChannel channel, Direction direction)
        {
            var key = Tuple.Create(channel, direction);
            if (_commandTypes.ContainsKey(key))
                return _commandTypes[key].GetEnumName(command);

            return "Unknown";
        }
    }
}
