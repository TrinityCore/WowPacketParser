
using System.Collections.Generic;

namespace WowPacketParser.Enums.Battlenet
{
    public enum BattlenetOpcode : ushort
    {
#region Authentication
        LogonRequest = 0x00,
        ResumeRequest = 0x01,
        ProofResponse = 0x02,
        GenerateSingleSignOnTokenRequest2 = 0x08,
        LogonRequest3 = 0x09,
        SingleSignOnRequest3 = 0x0A,

        LogonResponse = 0x00,
        ProofRequest = 0x02,
#endregion

#region Connection
        Ping = 0x00,
        EnableEncyption = 0x05,
        LogoutRequest = 0x06,
        DisconnectRequest = 0x07,
        ConnectionClosing = 0x09,

        Pong = 0x00,
        STUNServers = 0x04,
#endregion

#region WoWRealm
        ClientRealmUpdateSubscribe = 0x00,
        ClientRealmUpdateUnsubscribe = 0x01,
        ClientJoinRequest = 0x08,

        ListSubscribeResponse = 0x00,
        ListUpdate = 0x02,
        ListComplete = 0x03,
        ToonReady = 0x06,
        ToonLoggedOut = 0x07,
        JoinResponseV2 = 0x08,
#endregion

#region Cache
        GetStreamItemsRequest = 0x09,

        GetStreamItemsResponse = 0x09,
#endregion

#region Profile
        SettingsAvailable = 0x04,
#endregion
    }

    struct OpcodeNameKey
    {
        public OpcodeNameKey(ushort opcode, byte channel, Direction direction) : this()
        {
            Opcode = opcode;
            Channel = channel;
            Direction = direction;
        }

        public ushort Opcode { get; set; }
        public byte Channel { get; set; }
        public Direction Direction { get; set; }
    }

    public static class BattlenetOpcodeName
    {
        private static readonly Dictionary<OpcodeNameKey, string> Names = new Dictionary<OpcodeNameKey, string>
        {
            { new OpcodeNameKey(0, 0, Direction.BNClientToServer), "LogonRequest" },
            { new OpcodeNameKey(1, 0, Direction.BNClientToServer), "ResumeRequest" },
            { new OpcodeNameKey(2, 0, Direction.BNClientToServer), "ProofResponse" },
            { new OpcodeNameKey(8, 0, Direction.BNClientToServer), "GenerateSingleSignOnTokenRequest2" },
            { new OpcodeNameKey(9, 0, Direction.BNClientToServer), "LogonRequest3" },
            { new OpcodeNameKey(10, 0, Direction.BNClientToServer), "SingleSignOnRequest3" },

            { new OpcodeNameKey(0, 0, Direction.BNServerToClient), "LogonResponse" },
            { new OpcodeNameKey(1, 0, Direction.BNServerToClient), "ResumeResponse" },
            { new OpcodeNameKey(2, 0, Direction.BNServerToClient), "ProofRequest" },

            { new OpcodeNameKey(0, 1, Direction.BNClientToServer), "Ping" },
            { new OpcodeNameKey(5, 1, Direction.BNClientToServer), "ClientEnableEncryption" },
            { new OpcodeNameKey(6, 1, Direction.BNClientToServer), "LogoutRequest" },
            { new OpcodeNameKey(7, 1, Direction.BNClientToServer), "DisconnectRequest" },
            { new OpcodeNameKey(9, 1, Direction.BNClientToServer), "ConnectionClosing" },

            { new OpcodeNameKey(0, 1, Direction.BNServerToClient), "Pong" },
            { new OpcodeNameKey(4, 1, Direction.BNServerToClient), "STUNServers" },

            { new OpcodeNameKey(0, 2, Direction.BNClientToServer), "ListSubscribeRequest" },
            { new OpcodeNameKey(1, 2, Direction.BNClientToServer), "ListUnsubscribe" },
            { new OpcodeNameKey(8, 2, Direction.BNClientToServer), "JoinRequestV2" },

            { new OpcodeNameKey(0, 2, Direction.BNServerToClient), "ListSubscribeResponse" },
            { new OpcodeNameKey(2, 2, Direction.BNServerToClient), "ListUpdate" },
            { new OpcodeNameKey(3, 2, Direction.BNServerToClient), "ListComplete" },
            { new OpcodeNameKey(6, 2, Direction.BNServerToClient), "ToonReady" },
            { new OpcodeNameKey(7, 2, Direction.BNServerToClient), "ToonLoggedOut" },
            { new OpcodeNameKey(8, 2, Direction.BNServerToClient), "JoinResponseV2" },

            { new OpcodeNameKey(9, 11, Direction.BNClientToServer), "GetStreamItemsRequest" },
            { new OpcodeNameKey(9, 11, Direction.BNServerToClient), "GetStreamItemsResponse" },

            { new OpcodeNameKey(4, 14, Direction.BNServerToClient), "SettingsAvailable" },
        };

        public static string GetName(ushort opcode, byte channel, Direction direction)
        {
            string name;
            if (Names.TryGetValue(new OpcodeNameKey(opcode, channel, direction), out name))
                return name;

            return "Unknown";
        }
    }
}
