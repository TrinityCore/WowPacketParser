using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V1_15_8_63829
{
    public static class Opcodes_1_15_8
    {
        public static BiDictionary<Opcode, int> Opcodes(Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientOpcodes;
                case Direction.ServerToClient:
                    return ServerOpcodes;
                default:
                    return MiscOpcodes;
            }
        }

        private static readonly BiDictionary<Opcode, int> ClientOpcodes = new()
        {
            { Opcode.CMSG_CHAT_MESSAGE_SAY, 0x2F0023 },
            { Opcode.CMSG_SEND_TEXT_EMOTE, 0x2F0024 },
            { Opcode.CMSG_USE_ITEM, 0x30016B },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_TEXT_EMOTE, 0x3B0116 },
            { Opcode.SMSG_EMOTE, 0x3A026B },
            { Opcode.SMSG_CHAT, 0x3F0001 },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x4C0002 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x4A0000 },
            { Opcode.SMSG_AURA_UPDATE, 0x510011 },
            { Opcode.SMSG_SPELL_GO, 0x510028 },
            { Opcode.SMSG_SPELL_START, 0x510029 },
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x510014 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}
