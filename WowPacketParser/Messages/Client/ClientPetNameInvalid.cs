using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetNameInvalid
    {
        public PetRenameData RenameData;
        public byte Result;
    }
}
