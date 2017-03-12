using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetCastSpell
    {
        public ulong PetGUID;
        public SpellCastRequest Cast;
    }
}
