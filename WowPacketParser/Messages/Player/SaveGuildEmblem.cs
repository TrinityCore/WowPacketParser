using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct SaveGuildEmblem
    {
        public ulong Vendor;
        public int Bg;
        public int EStyle;
        public int EColor;
        public int BColor;
        public int BStyle;

        [Parser(Opcode.MSG_SAVE_GUILD_EMBLEM)]
        public static void HandleGuildEmblem(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadGuid("GUID");
                ReadEmblemInfo(packet);
            }
            else
                packet.ReadUInt32E<GuildEmblemError>("Result");
        }

        [Parser(Opcode.CMSG_SAVE_GUILD_EMBLEM, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSaveGuildEmblem(Packet packet)
        {
            packet.ReadPackedGuid128("Vendor");
            packet.ReadUInt32("EColor");
            packet.ReadUInt32("EStyle");
            packet.ReadUInt32("BColor");
            packet.ReadUInt32("BStyle");
            packet.ReadUInt32("Bg");
        }

        [Parser(Opcode.SMSG_PLAYER_SAVE_GUILD_EMBLEM, ClientVersionBuild.V6_0_2_19033)]
        public static void HandlePlayerSaveGuildEmblem(Packet packet)
        {
            packet.ReadInt32E<GuildEmblemError>("Error");
        }
        private static void ReadEmblemInfo(Packet packet)
        {
            packet.ReadInt32("Emblem Style");
            packet.ReadInt32("Emblem Color");
            packet.ReadInt32("Emblem Border Style");
            packet.ReadInt32("Emblem Border Color");
            packet.ReadInt32("Emblem Background Color");
        }
    }
}
