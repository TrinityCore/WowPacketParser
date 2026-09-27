using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V3_4_1_47014
{
    public static class Opcodes_3_4_1
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
            { Opcode.CMSG_AREA_TRIGGER, 0x31D8 },
            { Opcode.CMSG_CAST_SPELL, 0x329B },
            { Opcode.CMSG_USE_ITEM, 0x3297 },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_AURA_UPDATE, 0x2C22 },
            { Opcode.SMSG_AUTH_CHALLENGE, 0x3048},
            { Opcode.SMSG_UPDATE_BNET_SESSION_KEY, 0x282B},
            { Opcode.SMSG_SPELL_GO, 0x2C39 },
            { Opcode.SMSG_SPELL_START, 0x2C3A },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x2DD4 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x27D1 },
            { Opcode.SMSG_EMOTE, 0x27CF },
            { Opcode.SMSG_CHAT, 0x2BAD },
            { Opcode.SMSG_GOSSIP_COMPLETE, 0x2A97 },
            { Opcode.SMSG_GOSSIP_MESSAGE, 0x2A98 },
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x2C25 },
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x2914 },
            { Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x2915 },
            { Opcode.SMSG_TEXT_EMOTE, 0x2677 },
            { Opcode.SMSG_ATTACK_START, 0x293C },
            { Opcode.SMSG_ATTACK_STOP, 0x293D },
            { Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x2951 },
            { Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x2C32 },
            { Opcode.SMSG_AI_REACTION, 0x26B7 },
            { Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x2C33 },
            { Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x26DB },
            { Opcode.SMSG_THREAT_CLEAR, 0x26DE },
            { Opcode.SMSG_THREAT_REMOVE, 0x26DD },
            { Opcode.SMSG_THREAT_UPDATE, 0x26DC },
            { Opcode.SMSG_CAST_FAILED, 0x2C57 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new BiDictionary<Opcode, int>();
    }
}