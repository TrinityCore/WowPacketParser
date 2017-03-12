using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientActivateTaxiReply
    {
        public Taxireply Error;

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleActivateTaxiReply(Packet packet)
        {
            packet.ReadUInt32E<TaxiError>("Result");
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleActivateTaxiReply548(Packet packet)
        {
            packet.ReadBitsE<TaxiError>("Result", 4);
        }

        [Parser(Opcode.SMSG_ACTIVATE_TAXI_REPLY, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleActivateTaxiReply6(Packet packet)
        {
            packet.ReadBitsE<TaxiError>("Result", 4);
        }
    }
}
