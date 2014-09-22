﻿
using System.Collections.Generic;

namespace WowPacketParser.Enums.Battlenet
{
    public enum BattlenetOpcode : ushort
    {
#region None
        ClientInformationRequestOld = 0x00,
        ClientResume = 0x01,
        ClientProofResponse = 0x02,
        ClientInformationRequest = 0x09,
        ClientSingleSignOn = 0x0A,

        ServerComplete = 0x00,
        ServerProofRequest = 0x02,
#endregion

#region Connection
        ClientPing = 0x00,
        ClientEnableEncyption = 0x05,
        ClientDisconnect = 0x06,
        ClientReceivedInvalidPacket = 0x09,

        ServerPong = 0x00,
#endregion

#region WoW
        ClientRealmUpdateSubscribe = 0x00,
        ClientRealmUpdateUnsubscribe = 0x01,
        ClientJoinRequest = 0x08,

        ServerRealmUpdateBegin = 0x00,
        ServerRealmUpdate = 0x02,
        ServerRealmUpdateEnd = 0x03,
        ServerJoinResponse = 0x08,
#endregion

#region Cache
        ClientCacheOpcode9 = 0x09,
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
        private static readonly Dictionary<OpcodeNameKey, string> Names = new Dictionary<OpcodeNameKey, string>()
        {
            { new OpcodeNameKey(0, 0, Direction.BNClientToServer), "ClientInformationRequestOld" },
            { new OpcodeNameKey(1, 0, Direction.BNClientToServer), "ClientResume" },
            { new OpcodeNameKey(2, 0, Direction.BNClientToServer), "ClientProofResponse" },
            { new OpcodeNameKey(9, 0, Direction.BNClientToServer), "ClientInformationRequest" },
            { new OpcodeNameKey(10, 0, Direction.BNClientToServer), "ClientSingleSignOn" },

            { new OpcodeNameKey(0, 0, Direction.BNServerToClient), "ServerComplete" },
            { new OpcodeNameKey(2, 0, Direction.BNServerToClient), "ServerProofRequest" },

            { new OpcodeNameKey(0, 1, Direction.BNClientToServer), "ClientPing" },
            { new OpcodeNameKey(5, 1, Direction.BNClientToServer), "ClientEnableEncryption" },
            { new OpcodeNameKey(6, 1, Direction.BNClientToServer), "ClientDisconnect" },
            { new OpcodeNameKey(9, 1, Direction.BNClientToServer), "ClientReceivedInvalidPacket" },

            { new OpcodeNameKey(0, 1, Direction.BNServerToClient), "ServerPong" },

            { new OpcodeNameKey(0, 2, Direction.BNClientToServer), "ClientRealmUpdateSubscribe" },
            { new OpcodeNameKey(1, 2, Direction.BNClientToServer), "ClientRealmUpdateUnsubscribe" },
            { new OpcodeNameKey(8, 2, Direction.BNClientToServer), "ClientJoinRequest" },

            { new OpcodeNameKey(0, 2, Direction.BNServerToClient), "ServerRealmUpdateBegin" },
            { new OpcodeNameKey(2, 2, Direction.BNServerToClient), "ServerRealmUpdate" },
            { new OpcodeNameKey(3, 2, Direction.BNServerToClient), "ServerRealmUpdateEnd" },
            { new OpcodeNameKey(8, 2, Direction.BNServerToClient), "ServerJoinResponse" },
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
