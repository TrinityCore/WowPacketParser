using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Pet
{
    public unsafe struct Rename
    {
        public PetRenameData RenameData;

        [Parser(Opcode.CMSG_PET_RENAME)]
        public static void HandlePetRename(Packet packet)
        {
            packet.ReadGuid("Pet Guid");
            packet.ReadCString("Name");
            var declined = packet.ReadBool("Is Declined");
            if (declined)
                for (var i = 0; i < 5; ++i)
                    packet.ReadCString("Declined Name", i);
        }
    }
}
