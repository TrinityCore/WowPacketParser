using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlenetProtoHandler
    {
        public static MethodCall ReadMethodCall(Packet packet, params object[] idx)
        {
            var method = new MethodCall();

            method.Type = packet.ReadUInt64("Type", idx);
            method.ObjectId = packet.ReadUInt64("ObjectId", idx);
            method.Token = packet.ReadUInt32("Token", idx);

            return method;
        }

        public static void ReadProtoData(Packet packet, int length, MethodCall method, params object[] indexes)
        {
            packet.AddValue("MethodCall", "ServiceHash: 0x" + method.GetServiceHash().ToString("X") + " MethodID: " + method.GetMethodId(), indexes);
            packet.ReadBytesTable("Message", length, indexes);
        }

        [Parser(Opcode.CMSG_BATTLENET_REQUEST)]
        public static void HandleBattlenetRequest(Packet packet)
        {
            var method = ReadMethodCall(packet, "Method");

            int protoSize = packet.ReadInt32();
            ReadProtoData(packet, protoSize, method, "Data");
        }

        [Parser(Opcode.SMSG_BATTLENET_NOTIFICATION)]
        public static void HandleBattlenetNotification(Packet packet)
        {
            var method = ReadMethodCall(packet, "Method");

            int protoSize = packet.ReadInt32();
            ReadProtoData(packet, protoSize, method, "Data");
        }

        [Parser(Opcode.SMSG_BATTLENET_RESPONSE)]
        public static void HandleBattlenetResponse(Packet packet)
        {
            packet.ReadInt32E<BattlenetRpcErrorCode>("BnetStatus");
            var method = ReadMethodCall(packet, "Method");

            int protoSize = packet.ReadInt32();
            ReadProtoData(packet, protoSize, method, "Data");
        }
    }
}
