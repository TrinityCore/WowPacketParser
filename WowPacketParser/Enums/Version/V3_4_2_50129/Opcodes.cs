using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V3_4_2_50129
{
    public static class Opcodes_3_4_2
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
            // GROUP: CliGlobal

            { Opcode.CMSG_ENTER_ENCRYPTED_MODE_ACK, 0x3767 },
            { Opcode.CMSG_AREA_TRIGGER, 0x31D7 },
            { Opcode.CMSG_CAST_SPELL, 0x3295 },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_AURA_UPDATE, 0x2C1F },
            { Opcode.SMSG_AUTH_CHALLENGE, 0x3048},
            { Opcode.SMSG_UPDATE_BNET_SESSION_KEY, 0x282B},
            { Opcode.SMSG_ENTER_ENCRYPTED_MODE, 0x3049},
            { Opcode.SMSG_SPELL_GO, 0x2C36 },
            { Opcode.SMSG_SPELL_START, 0x2C37 },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x2DD4 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x27D1 },
            { Opcode.SMSG_EMOTE, 0x27CF },
            { Opcode.SMSG_CHAT, 0x2BAD },
            { Opcode.SMSG_GOSSIP_COMPLETE, 0x2A97 },
            { Opcode.SMSG_GOSSIP_MESSAGE, 0x2A98 },
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x2C22 },
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x2914 },
            { Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x2915 },
            { Opcode.SMSG_TEXT_EMOTE, 0x2677 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}