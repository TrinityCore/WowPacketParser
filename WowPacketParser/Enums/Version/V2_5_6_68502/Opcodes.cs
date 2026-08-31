using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V2_5_6_68502
{
    public static class Opcodes_2_5_6
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
            { Opcode.CMSG_CAST_SPELL, 0x3E0160 },
            { Opcode.CMSG_CHAT_MESSAGE_SAY, 0x2F0023 },
            { Opcode.CMSG_CHAT_MESSAGE_EMOTE, 0x2F0024 },
            { Opcode.CMSG_AREA_TRIGGER, 0x3E0088 },
            { Opcode.CMSG_SEND_TEXT_EMOTE, 0x3F0014 },
            { Opcode.CMSG_USE_ITEM, 0x3E015C },
        };

        private static readonly BiDictionary<Opcode, int> ServerOpcodes = new()
        {
            { Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE, 0x640016 },
            { Opcode.SMSG_QUERY_CREATURE_RESPONSE, 0x4A0006 },
            { Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE, 0x4A0007 },

            { Opcode.SMSG_GOSSIP_COMPLETE, 0x640017 },
            { Opcode.SMSG_GOSSIP_MESSAGE, 0x640018 },
            { Opcode.SMSG_GOSSIP_QUEST_UPDATE, 0x640019 },
            { Opcode.SMSG_GOSSIP_REFRESH_OPTIONS, 0x640027 },
            { Opcode.SMSG_GOSSIP_OPTION_NPC_INTERACTION, 0x640028 },

            { Opcode.SMSG_SET_TIME_ZONE_INFORMATION, 0x460124 },
            { Opcode.SMSG_HIGHEST_THREAT_UPDATE, 0x460187 },
            { Opcode.SMSG_THREAT_CLEAR, 0x46018A },
            { Opcode.SMSG_THREAT_REMOVE, 0x460189 },
            { Opcode.SMSG_THREAT_UPDATE, 0x460188 },
            { Opcode.SMSG_ATTACKER_STATE_UPDATE, 0x4C0030 },
            { Opcode.SMSG_ATTACK_START, 0x4C001B },
            { Opcode.SMSG_ATTACK_STOP, 0x4C001C },
            { Opcode.SMSG_AI_REACTION, 0x460163 },
            { Opcode.SMSG_TEXT_EMOTE, 0x460127 },
            { Opcode.SMSG_EMOTE, 0x46027D },
            { Opcode.SMSG_CHAT, 0x4B0001 },
            { Opcode.SMSG_ON_MONSTER_MOVE, 0x5E0002 },
            { Opcode.SMSG_UPDATE_OBJECT, 0x5C0000 },
            { Opcode.SMSG_AURA_UPDATE, 0x660011 },
            { Opcode.SMSG_CAST_FAILED, 0x660048 },
            { Opcode.SMSG_SPELL_GO, 0x66002A },
            { Opcode.SMSG_SPELL_START, 0x66002B },
            { Opcode.SMSG_SPELL_COOLDOWN, 0x660005 },
            { Opcode.SMSG_SPELL_CATEGORY_COOLDOWN, 0x660006 },
            { Opcode.SMSG_PET_SPELLS_MESSAGE, 0x660014 },
            { Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG, 0x660021 },
            { Opcode.SMSG_SPELL_INSTAKILL_LOG, 0x660022 },
        };

        private static readonly BiDictionary<Opcode, int> MiscOpcodes = new();
    }
}
