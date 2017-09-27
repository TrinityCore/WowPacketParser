using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct MountSetFavorite
    {
        public int MountSpellID;
        public bool IsFavorite;

        [Parser(Opcode.CMSG_MOUNT_SET_FAVORITE)]
        public static void HandleMountSetFavorite(Packet packet)
        {
            packet.ReadInt32("MountSpellID");
            packet.ReadBit("IsFavorite");
        }
    }
}
