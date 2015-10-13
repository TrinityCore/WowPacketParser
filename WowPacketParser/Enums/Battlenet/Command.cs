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
        FriendInvite = 1,
        FriendInviteResponse = 2,
        FriendRemove = 4,
        FriendNote = 5,
        ToonsOfFriendsRequest = 6,
        BlockAdd = 8,
        BlockRemove = 10,
        GetFriendsOfFriend = 11,
        SendInvitationRequest = 26
    }

    public enum FriendsServerCommand : ushort
    {
        FriendInviteNotify = 1,
        FriendInviteResult = 3,
        ToonsOfFriendsNotify = 6,
        BlockAddFailure = 9,
        MaxFriendsNotify = 21,
        SendInvitationResult = 27,
        FriendInvitationAddedNotify = 28,
        FriendInvitationRemovedNotify = 29,
        FriendsListNotify5 = 30,
        AccountBlockAddedNotify = 31,
        AccountBlockRemovedNotify = 32,
        ToonBlockNotify = 33,
        FriendsOfFriendResult = 34
    }

    public enum PresenceClientCommand : ushort
    {
        UpdateRequest = 0,
        StatisticsSubscribe = 2
    }

    public enum PresenceServerCommand : ushort
    {
        UpdateNotify = 0,
        FieldSpecAnnounce = 1,
        StatisticsUpdate = 3
    }

    public enum ChatClientCommand : ushort
    {
        JoinRequest2 = 0,
        LeaveRequest = 2,
        InviteRequest = 3,
        CreateAndInviteRequest = 10,
        MessageSend = 11,
        DatagramConnectionUpdate = 13,
        ReportSpamRequest = 14,
        WhisperSend = 19,
        EnumCategoryDescriptions = 21,
        EnumConferenceDescriptions = 23,
        EnumConferenceMemberCounts = 25,
        GetMemberCountRequest = 31,
        ModifyChannelListRequest2 = 32,
        GameDataSendRequest = 34
    }

    public enum ChatServerCommand : ushort
    {
        MembershipChangeNotify = 1,
        InviteNotify = 4,
        InviteCanceled = 7,
        MessageRecv = 11,
        MessageUndeliverable = 12,
        DatagramConnectionUpdate = 13,
        InviteFailed = 15,
        SystemMessage = 16,
        MessageBlocked = 18,
        WhisperRecv = 19,
        WhisperUndeliverable = 20,
        CategoryDescriptions = 22,
        ConferenceDescriptions = 24,
        ConferenceMemberCounts = 26,
        JoinNotify2 = 27,
        ConfigChanged = 29,
        WhisperEchoRecv = 30,
        GetMemberCountResponse = 31,
        ModifyChannelListResponse2 = 33,
        GameDataSendResponse = 35,
        GameDataRecv = 36
    }

    public enum SupportClientCommand : ushort
    {
        ComplaintRequest2 = 1
    }

    public enum SupportServerCommand : ushort
    {
        // none
    }

    public enum AchievementClientCommand : ushort
    {
        ListenRequest = 0,
        CriteriaFlushRequest = 3,
        SetTrophyCase = 5
    }

    public enum AchievementServerCommand : ushort
    {
        Data = 2,
        CriteriaFlushResponse = 3,
        AchievementHandleUpdate = 4,
        ChangeTrophyCaseResult = 6
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
        ReadRequest = 0,
        AddressQueryRequest = 1,
        ResolveToonHandleToNameRequest = 2,
        ResolveToonNameToHandleRequest = 3,
        ChangeSettings = 5
    }

    public enum ProfileServerCommand : ushort
    {
        ReadResponse = 0,
        AddressQueryResponse = 1,
        ResolveToonHandleToNameResponse = 2,
        ResolveToonNameToHandleResponse = 3,
        SettingsAvailable = 4
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
