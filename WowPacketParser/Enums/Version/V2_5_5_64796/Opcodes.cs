using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V2_5_5_64796
{
    public static class Opcodes_2_5_5
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
            { Opcode.CMSG_SEND_TEXT_EMOTE, 0x3E0013 },
            { Opcode.CMSG_USE_ITEM, 0x30016B },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_TEXT_EMOTE, 0x440121 },
            { Opcode.SMSG_EMOTE, 0x440270 },
            { Opcode.SMSG_CHAT, 0x490001 },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x5C0002 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x5A0000 },
            { Opcode.SMSG_AURA_UPDATE, 0x630011 },
            { Opcode.SMSG_SPELL_GO, 0x630028 },
            { Opcode.SMSG_SPELL_START, 0x630029 },
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x630014 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}
