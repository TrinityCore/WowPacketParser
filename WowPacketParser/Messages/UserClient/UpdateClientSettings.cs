using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UpdateClientSettings
    {
        public CliClientSettings Settings;

        [Parser(Opcode.CMSG_UPDATE_CLIENT_SETTINGS)]
        public static void HandleUpdateClientSettings(Packet packet)
        {
            readClientSettings(packet, "ClientSettings");
        }

        public static void readClientSettings(Packet packet, params object[] idx)
        {
            packet.ReadSingle("FarClip", idx);
        }
    }
}
