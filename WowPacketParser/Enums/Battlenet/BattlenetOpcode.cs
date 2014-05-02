
using System.Collections.Generic;

namespace WowPacketParser.Enums.Battlenet
{
    public enum BattlenetOpcode : ushort
    {
#region None
        ClientInformationRequestOld = 0x00,
        ClientProofResponse = 0x02,
        ClientInformationRequest = 0x09,

        ServerComplete = 0x00,
        ServerProofRequest = 0x02,
#endregion

#region Creep
        ClientPing = 0x00,

        ServerPong = 0x00,
#endregion

#region WoW
        ClientRealmUpdate = 0x00,
        ClientJoinRequest = 0x08,

        ServerRealmUpdate = 0x02,
        ServerJoinResponse = 0x08,
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
            { new OpcodeNameKey(2, 0, Direction.BNClientToServer), "ClientProofResponse" },
            { new OpcodeNameKey(9, 0, Direction.BNClientToServer), "ClientInformationRequest" },
            { new OpcodeNameKey(0, 0, Direction.BNServerToClient), "ServerComplete" },
            { new OpcodeNameKey(2, 0, Direction.BNServerToClient), "ServerProofRequest" },
            { new OpcodeNameKey(0, 1, Direction.BNClientToServer), "ClientPing" },
            { new OpcodeNameKey(0, 1, Direction.BNServerToClient), "ServerPong" },
            { new OpcodeNameKey(8, 2, Direction.BNClientToServer), "ClientRealmUpdate" },
            { new OpcodeNameKey(0, 2, Direction.BNClientToServer), "ClientJoinRequest" },
            { new OpcodeNameKey(2, 2, Direction.BNServerToClient), "ServerRealmUpdate" },
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
