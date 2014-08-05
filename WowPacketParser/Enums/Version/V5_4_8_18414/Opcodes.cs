using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_8_18414
{
    public static class Opcodes_5_4_8a
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
        //                                                                    /checked in code
        //                                                                     /checked with sniff
            {Opcode.CMSG_ACTIVATETAXI,                             0x03C9}, //--
            {Opcode.CMSG_ACTIVATETAXIEXPRESS,                      0x06FB}, //--
            {Opcode.CMSG_ADD_FRIEND,                               0x09A6}, //--
            {Opcode.CMSG_ADD_IGNORE,                               0x0D20}, //-+
            {Opcode.CMSG_ALTER_APPEARANCE,                         0x07F0}, //+-
            {Opcode.CMSG_AREATRIGGER,                              0x1C44}, //++
            {Opcode.CMSG_ATTACKSTOP,                               0x0345}, //++
            {Opcode.CMSG_ATTACKSWING,                              0x02E7}, //--
            {Opcode.CMSG_AUCTION_HELLO,                            0x0379}, //-+
            {Opcode.CMSG_AUTH_SESSION,                             0x00B2}, //+-
            {Opcode.CMSG_AUTOBANK_ITEM,                            0x066D}, //++
            {Opcode.CMSG_AUTOEQUIP_ITEM,                           0x025F}, //--
            {Opcode.CMSG_AUTOSTORE_BAG_ITEM,                       0x067C}, //--
            {Opcode.CMSG_AUTOSTORE_BANK_ITEM,                      0x02CF}, //++
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM,                      0x0354}, //++
            {Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES,               0x06CB}, //--
            {Opcode.CMSG_BANKER_ACTIVATE,                          0x02E9}, //-+
            {Opcode.CMSG_BATTLEFIELD_LEAVE,                        0x0257}, //+-
            {Opcode.CMSG_BATTLEFIELD_LIST,                         0x1C41}, //+-
            {Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE,    0x1806}, //--
            {Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST,             0x08B3}, //--
            {Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE,    0x0A97}, //--
            {Opcode.CMSG_BATTLEFIELD_PORT,                         0x1379}, //--
            {Opcode.CMSG_BATTLEMASTER_JOIN,                        0x0769}, //--
            {Opcode.CMSG_BATTLEMASTER_JOIN_ARENA,                  0x02D2}, //+-
            {Opcode.CMSG_BATTLE_PET_DELETE_PET,                    0x18B6}, //+-
            {Opcode.CMSG_BATTLE_PET_MODIFY_NAME,                   0x1887}, //+-
            {Opcode.CMSG_BATTLE_PET_NAME_QUERY,                    0x1CE0}, //+-
            {Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT,               0x0163}, //+-
            {Opcode.CMSG_BATTLE_PET_SET_FLAGS,                     0x17AC}, //+-
            {Opcode.CMSG_BATTLE_PET_SUMMON_COMPANION,              0x1896}, //+-
            {Opcode.CMSG_BINDER_ACTIVATE,                          0x1248}, //-+
            {Opcode.CMSG_BLACK_MARKET_BID,                         0x12C8}, //--
            {Opcode.CMSG_BLACK_MARKET_HELLO,                       0x075A}, //--
            {Opcode.CMSG_BLACK_MARKET_REQUEST_ITEMS,               0x127A}, //--
            {Opcode.CMSG_BUYBACK_ITEM,                             0x0661}, //+-
            {Opcode.CMSG_BUY_BANK_SLOT,                            0x12F2}, //-+
            {Opcode.CMSG_BUY_ITEM,                                 0x02E2}, //--
            {Opcode.CMSG_CANCEL_AURA,                              0x1861 | 0x10000}, //++
            {Opcode.CMSG_CANCEL_CAST,                              0x18C0}, //++
            {Opcode.CMSG_CANCEL_MOUNT_AURA,                        0x10E3}, //++ ??
            {Opcode.CMSG_CANCEL_TRADE,                             0x1941}, //+-
            {Opcode.CMSG_CAST_SPELL,                               0x0206 | 0x10000}, //++
            {Opcode.CMSG_CHAR_CREATE,                              0x0F1D}, //--
            {Opcode.CMSG_CHAR_CUSTOMIZE,                           0x0A13}, //--
            {Opcode.CMSG_CHAR_DELETE,                              0x04E2}, //-+
            //{Opcode.CMSG_CHAR_ENUM,                                0x00E0}, //--
            {Opcode.CMSG_CHAR_FACTION_CHANGE,                      0x0329}, //--
            {Opcode.CMSG_CHAR_RENAME,                              0x0963}, //--
            {Opcode.CMSG_CHAT_IGNORED,                             0x048A | 0x10000}, //--
            {Opcode.CMSG_CONTACT_LIST,                             0x0BB4}, //--
            {Opcode.CMSG_CREATURE_QUERY,                           0x0842}, //+-
            {Opcode.CMSG_DEL_FRIEND,                               0x1103}, //--
            {Opcode.CMSG_DEL_IGNORE,                               0x0737}, //-+
            {Opcode.CMSG_DESTROY_ITEM,                             0x0026}, //-+
            {Opcode.CMSG_DUEL_PROPOSED,                            0x1A26}, //--
            {Opcode.CMSG_DUEL_RESPONSE,                            0x03E2}, //--
            {Opcode.CMSG_EMOTE,                                    0x1924}, //++
            {Opcode.CMSG_ENABLETAXI,                               0x0741}, //--
            {Opcode.CMSG_GAMEOBJECT_QUERY,                         0x1461}, //-+
            {Opcode.CMSG_GAMEOBJ_REPORT_USE,                       0x06D9}, //-+ ??
            {Opcode.CMSG_GAMEOBJ_USE,                              0x06D8}, //-+ ??
            {Opcode.CMSG_GET_MAIL_LIST,                            0x077A}, //--
            {Opcode.CMSG_GET_TIMEZONE_INFORMATION,                 0x00E0}, //++
            {Opcode.CMSG_GOSSIP_HELLO,                             0x12F3}, //-+
            {Opcode.CMSG_GOSSIP_SELECT_OPTION,                     0x0748}, //++
            {Opcode.CMSG_GROUP_DISBAND,                            0x1798}, //--
            {Opcode.CMSG_GROUP_INVITE,                             0x072D}, //--
            {Opcode.CMSG_GROUP_INVITE_RESPONSE,                    0x0D61}, //--
            {Opcode.CMSG_GROUP_RAID_CONVERT,                       0x032C}, //--
            {Opcode.CMSG_GROUP_SET_LEADER,                         0x15BB}, //--
            {Opcode.CMSG_GROUP_SET_ROLES,                          0x1A92}, //--
            {Opcode.CMSG_GROUP_UNINVITE_GUID,                      0x0CE1}, //--
            {Opcode.CMSG_GUILD_ACCEPT,                             0x18A2}, //+-
            {Opcode.CMSG_GUILD_ADD_RANK,                           0x047A}, //--
            {Opcode.CMSG_GUILD_ASSIGN_MEMBER_RANK,                 0x05D0}, //--
            {Opcode.CMSG_GUILD_BANK_BUY_TAB,                       0x0251 | 0x10000}, //--
            {Opcode.CMSG_GUILD_BANK_DEPOSIT_MONEY,                 0x0770}, //--
            {Opcode.CMSG_GUILD_BANK_LOG_QUERY,                     0x0CD3}, //--
            {Opcode.CMSG_GUILD_BANK_UPDATE_TAB,                    0x07C2}, //--
            {Opcode.CMSG_GUILD_BANKER_ACTIVATE,                    0x0372}, //--
            {Opcode.CMSG_GUILD_DECLINE,                            0x147B}, //+-
            {Opcode.CMSG_GUILD_DEL_RANK,                           0x0D79 | 0x10000}, //--
            {Opcode.CMSG_GUILD_DEMOTE,                             0x1553 | 0x10000}, //--
            {Opcode.CMSG_GUILD_DISBAND,                            0x0D73}, //--
            {Opcode.CMSG_GUILD_EVENT_LOG_QUERY,                    0x15D9}, //--
            {Opcode.CMSG_GUILD_INFO_TEXT,                          0x0C70}, //--
            {Opcode.CMSG_GUILD_INVITE,                             0x0869}, //--
            {Opcode.CMSG_GUILD_LEAVE,                              0x04D8}, //--
            {Opcode.CMSG_GUILD_MOTD,                               0x1473}, //--
            {Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY,                 0x04D1}, //--
            {Opcode.CMSG_GUILD_PROMOTE,                            0x0571}, //--
            {Opcode.CMSG_GUILD_QUERY,                              0x1AB6}, //--
            {Opcode.CMSG_GUILD_QUERY_NEWS,                         0x1C58}, //--
            {Opcode.CMSG_GUILD_QUERY_RANKS,                        0x0D50}, //--
            {Opcode.CMSG_GUILD_REMOVE,                             0x0CD8}, //--
            {Opcode.CMSG_GUILD_REQUEST_CHALLENGE_UPDATE,           0x147A}, //--
            {Opcode.CMSG_GUILD_ROSTER,                             0x1459}, //--
            {Opcode.CMSG_GUILD_SET_GUILD_MASTER,                   0x1A83}, //--
            {Opcode.CMSG_INSPECT,                                  0x1259}, //--
            {Opcode.CMSG_ITEM_REFUND_INFO,                         0x1258}, //+-
            {Opcode.CMSG_JOIN_CHANNEL,                             0x148E | 0x10000}, //++
            {Opcode.CMSG_LEARN_TALENT,                             0x02A7 | 0x10000}, //+-
            {Opcode.CMSG_LEAVE_CHANNEL,                            0x042A | 0x10000}, //--
            {Opcode.CMSG_LFG_TELEPORT,                             0x1AA6}, //--
            {Opcode.CMSG_LIST_INVENTORY,                           0x02D8}, //-+
            {Opcode.CMSG_LOAD_SCREEN,                              0x1DBD}, //++
            {Opcode.CMSG_LOG_DISCONNECT,                           0x10B3}, //-+
            {Opcode.CMSG_LOGOUT_CANCEL,                            0x06C1}, //--
            {Opcode.CMSG_LOGOUT_REQUEST,                           0x1349}, //+-
            {Opcode.CMSG_LOOT,                                     0x1CE2}, //--
            {Opcode.CMSG_LOOT_METHOD,                              0x0DE1}, //--
            {Opcode.CMSG_LOOT_MONEY,                               0x02F6}, //+-
            {Opcode.CMSG_LOOT_RELEASE,                             0x0840}, //++
            {Opcode.CMSG_MAIL_CREATE_TEXT_ITEM,                    0x1270}, //--
            {Opcode.CMSG_MAIL_DELETE,                              0x14E2}, //+-
            {Opcode.CMSG_MAIL_MARK_AS_READ,                        0x0241}, //--
            {Opcode.CMSG_MAIL_RETURN_TO_SENDER,                    0x1FA8}, //--
            {Opcode.CMSG_MAIL_TAKE_ITEM,                           0x1371}, //--
            {Opcode.CMSG_MAIL_TAKE_MONEY,                          0x06FA}, //--
            {Opcode.CMSG_MESSAGECHAT_AFK,                          0x0EAB | 0x10000}, //--
            {Opcode.CMSG_MESSAGECHAT_CHANNEL,                      0x00BB}, //--
            {Opcode.CMSG_MESSAGECHAT_DND,                          0x002E | 0x10000}, //--
            {Opcode.CMSG_MESSAGECHAT_EMOTE,                        0x103E | 0x10000}, //-+
            {Opcode.CMSG_MESSAGECHAT_GUILD,                        0x0CAE | 0x10000}, //-+
            {Opcode.CMSG_MESSAGECHAT_OFFICER,                      0x0ABF}, //--
            //{Opcode.CMSG_MESSAGECHAT_PARTY,                        0x109A | 0x10000}, //-+ bad id
            {Opcode.CMSG_MESSAGECHAT_RAID,                         0x083E}, //--
            {Opcode.CMSG_MESSAGECHAT_RAID_WARNING,                 0x109A | 0x10000}, //-- bad id
            {Opcode.CMSG_MESSAGECHAT_SAY,                          0x0A9A | 0x10000}, //-+
            {Opcode.CMSG_MESSAGECHAT_WHISPER,                      0x123E | 0x10000}, //++
            {Opcode.CMSG_MESSAGECHAT_YELL,                         0x04AA}, //-+
            {Opcode.CMSG_MINIMAP_PING,                             0x0837}, //-+
            {Opcode.CMSG_MOVE_TELEPORT_ACK,                        0x0078}, //-+
            {Opcode.CMSG_MOVE_TIME_SKIPPED,                        0x0150}, //++
            {Opcode.CMSG_NAME_QUERY,                               0x0328}, //-+
            {Opcode.CMSG_NEUTRALPLAYERFACTIONSELECTRESULT,         0x0027 | 0x10000}, //--
            {Opcode.CMSG_NPC_TEXT_QUERY,                           0x0287 | 0x10000}, //++
            {Opcode.CMSG_OBJECT_UPDATE_FAILED,                     0x1061}, //--
            {Opcode.CMSG_OFFER_PETITION,                           0x15BE}, //-+
            {Opcode.CMSG_OPENING_CINEMATIC,                        0x0130}, //--
            {Opcode.CMSG_OPEN_ITEM,                                0x1D10}, //--
            {Opcode.CMSG_PAGE_TEXT_QUERY,                          0x1022}, //--
            {Opcode.CMSG_PET_ACTION,                               0x025B}, //--
            {Opcode.CMSG_PET_CAST_SPELL,                           0x044D}, //--
            {Opcode.CMSG_PET_NAME_QUERY,                           0x1C62}, //++
            {Opcode.CMSG_PETITION_BUY,                             0x12D9}, //--
            {Opcode.CMSG_PETITION_DECLINE,                         0x1279}, //--
            {Opcode.CMSG_PETITION_QUERY,                           0x0255}, //--
            {Opcode.CMSG_PETITION_RENAME,                          0x1F9A}, //--
            {Opcode.CMSG_PETITION_SHOW_SIGNATURES,                 0x136B}, //--
            {Opcode.CMSG_PETITION_SHOWLIST,                        0x037B}, //-+
            {Opcode.CMSG_PETITION_SIGN,                            0x06DA}, //
            {Opcode.CMSG_PING,                                     0x0012}, //++
            {Opcode.CMSG_PLAYED_TIME,                              0x03F6}, //++
            {Opcode.CMSG_PLAYER_LOGIN,                             0x158F}, //++
            {Opcode.CMSG_PVP_LOG_DATA,                             0x14C2 | 0x10000}, //-+
            {Opcode.CMSG_QUERY_GUILD_REWARDS,                      0x06C4}, //+-
            {Opcode.CMSG_QUERY_GUILD_XP,                           0x05F8}, //--
            {Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST,                  0x06D1}, //--
            {Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD,                 0x07CB}, //--
            {Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST,                0x0659}, //--
            {Opcode.CMSG_QUESTGIVER_HELLO,                         0x02DB}, //--
            {Opcode.CMSG_QUESTGIVER_QUERY_QUEST,                   0x12F0}, //--
            {Opcode.CMSG_QUESTGIVER_REQUEST_REWARD,                0x0378}, //--
            {Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY,         0x02F1 | 0x10000}, //--
            {Opcode.CMSG_QUESTGIVER_STATUS_QUERY,                  0x036A | 0x10000}, //++
            {Opcode.CMSG_QUEST_POI_QUERY,                          0x10C2 | 0x10000}, //++
            {Opcode.CMSG_QUEST_QUERY,                              0x02D5}, //--
            {Opcode.CMSG_QUESTLOG_REMOVE_QUEST,                    0x0779}, //--
            {Opcode.CMSG_RAID_READY_CHECK,                         0x0817 | 0x10000}, //--
            {Opcode.CMSG_RAID_READY_CHECK_CONFIRM,                 0x158B}, //--
            {Opcode.CMSG_RANDOM_ROLL,                              0x08A3}, //--
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME,                      0x0B1C}, //--
            {Opcode.CMSG_READ_ITEM,                                0x0D00}, //--
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES,             0x031C}, //++
            {Opcode.CMSG_REALM_NAME_QUERY,                         0x1A16}, //--
            {Opcode.CMSG_REALM_SPLIT,                              0x18B2}, //++
            {Opcode.CMSG_RECLAIM_CORPSE,                           0x03D3}, //--
            {Opcode.CMSG_REDIRECT_AUTH_PROOF,                      0x0F49}, //--
            {Opcode.CMSG_REFORGE_ITEM,                             0x0C4F}, //--
            {Opcode.CMSG_REORDER_CHARACTERS,                       0x08A7}, //--
            {Opcode.CMSG_REPAIR_ITEM,                              0x02C1}, //--
            {Opcode.CMSG_REPOP_REQUEST,                            0x134A}, //--
            {Opcode.CMSG_REQUEST_ACCOUNT_DATA,                     0x1D8A}, //++
            {Opcode.CMSG_REQUEST_HOTFIX,                           0x158D}, //--
            {Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS,               0x0806}, //--
            {Opcode.CMSG_REQUEST_PVP_REWARDS,                      0x0375}, //+-
            {Opcode.CMSG_RESET_FACTION_CHEAT,                      0x10B6}, //--
            {Opcode.CMSG_RESET_INSTANCES,                          0x0C69}, //+-
            {Opcode.CMSG_RETURN_TO_GRAVEYARD,                      0x12EA}, //--
            {Opcode.CMSG_SAVE_CUF_PROFILES,                        0x06E6}, //++
            {Opcode.CMSG_SELL_ITEM,                                0x1358}, //--
            {Opcode.CMSG_SEND_MAIL,                                0x1DBA}, //--
            {Opcode.CMSG_SET_ACTIONBAR_TOGGLES,                    0x0672}, //++
            {Opcode.CMSG_SET_ACTION_BUTTON,                        0x1F8C | 0x10000}, //++
            {Opcode.CMSG_SET_CONTACT_NOTES,                        0x0937}, //--
            {Opcode.CMSG_SET_DUNGEON_DIFFICULTY,                   0x1A36}, //--
            {Opcode.CMSG_SET_PLAYER_DECLINED_NAMES,                0x09E2}, //++
            {Opcode.CMSG_SET_PRIMARY_TALENT_TREE,                  0x06C6}, //+-
            {Opcode.CMSG_SET_PVP,                                  0x02C5}, //--
            {Opcode.CMSG_SET_RAID_DIFFICULTY,                      0x1093}, //--
            {Opcode.CMSG_SET_SELECTION,                            0x0740}, //++
            {Opcode.CMSG_SET_TAXI_BENCHMARK_MODE,                  0x0762}, //--
            {Opcode.CMSG_SET_TITLE,                                0x03C7}, //+-
            {Opcode.CMSG_SETSHEATHED,                              0x0249}, //++ //some load screen
            {Opcode.CMSG_SHOWING_CLOAK,                            0x02F2}, //--
            {Opcode.CMSG_SHOWING_HELM,                             0x126B}, //--
            {Opcode.CMSG_SPELLCLICK,                               0x067A}, //--
            {Opcode.CMSG_SPLIT_ITEM,                               0x02EC}, //--
            {Opcode.CMSG_STANDSTATECHANGE,                         0x03E6}, //+-
            {Opcode.CMSG_SUBMIT_BUG,                               0x0861}, //--
            {Opcode.CMSG_SUBMIT_COMPLAIN,                          0x030D}, //--
            {Opcode.CMSG_SUGGESTION_SUBMIT,                        0x0A12}, //--
            {Opcode.CMSG_SWAP_INV_ITEM,                            0x03DF}, //--
            {Opcode.CMSG_SWAP_ITEM,                                0x035D}, //--
            {Opcode.CMSG_TAXIQUERYAVAILABLENODES,                  0x02E3}, //--
            {Opcode.CMSG_TEXT_EMOTE,                               0x07E9}, //+-
            {Opcode.CMSG_TIME_SYNC_RESP,                           0x01DB | 0x10000}, //+-
            {Opcode.CMSG_TIME_SYNC_RESP_FAILED,                    0x0058}, //--
            {Opcode.CMSG_TRAINER_BUY_SPELL,                        0x0352}, //--
            {Opcode.CMSG_TRAINER_LIST,                             0x034B}, //--
            {Opcode.CMSG_TRANSMOGRIFY_ITEMS,                       0x06D7}, //--
            {Opcode.CMSG_TURN_IN_PETITION,                         0x0673}, //--
            {Opcode.CMSG_UNLEARN_SKILL,                            0x0268}, //++
            {Opcode.CMSG_UPDATE_ACCOUNT_DATA,                      0x0068}, //++
            {Opcode.CMSG_USE_ITEM,                                 0x1CC1}, //--
            {Opcode.CMSG_VIOLENCE_LEVEL,                           0x0040}, //+-
            {Opcode.CMSG_VOID_STORAGE_QUERY,                       0x0140}, //--
            {Opcode.CMSG_VOID_STORAGE_TRANSFER,                    0x1440 | 0x10000}, //+-
            {Opcode.CMSG_VOID_STORAGE_UNLOCK,                      0x0444}, //--
            {Opcode.CMSG_VOID_SWAP_ITEM,                           0x0655}, //--
            {Opcode.CMSG_WARDEN_DATA,                              0x1816}, //+-
            {Opcode.CMSG_WHO,                                      0x18A3}, //--
            {Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE,              0x15AB}, //+-
            {Opcode.CMSG_UNK_0002,                                 0x0002 | 0x10000}, //++
            {Opcode.CMSG_UNK_006B,                                 0x006B}, //++
            {Opcode.CMSG_UNK_0087,                                 0x0087}, //++
            {Opcode.CMSG_UNK_00A7,                                 0x00A7}, //+-
            {Opcode.CMSG_UNK_00F2,                                 0x00F2}, //++
            {Opcode.CMSG_UNK_0247,                                 0x0247}, //+-
            {Opcode.CMSG_UNK_0264,                                 0x0264}, //+-
            {Opcode.CMSG_UNK_0265,                                 0x0265}, //+-
            {Opcode.CMSG_UNK_0292,                                 0x0292}, //++
            {Opcode.CMSG_UNK_02C4,                                 0x02C4}, //+-
            {Opcode.CMSG_UNK_03E4,                                 0x03E4}, //+-
            {Opcode.CMSG_UNK_044E,                                 0x044E}, //+-
            {Opcode.CMSG_UNK_0656,                                 0x0656}, //+-
            {Opcode.CMSG_UNK_08C0,                                 0x08C0}, //+-
            {Opcode.CMSG_UNK_09DB,                                 0x09DB}, //++
            {Opcode.CMSG_UNK_09F0,                                 0x09F0}, //++
            {Opcode.CMSG_UNK_09FB,                                 0x09FB}, //--
            {Opcode.CMSG_UNK_0CF0,                                 0x0CF0}, //++
            {Opcode.CMSG_UNK_10A2,                                 0x10A2 | 0x10000}, //+-
            {Opcode.CMSG_UNK_10A7,                                 0x10A7 | 0x10000}, //+-
            {Opcode.CMSG_UNK_10F3,                                 0x10F3}, //+-
            {Opcode.CMSG_UNK_115B,                                 0x115B}, //++
            {Opcode.CMSG_UNK_11D8,                                 0x11D8}, //++
            {Opcode.CMSG_UNK_11D9,                                 0x11D9}, //+-
            {Opcode.CMSG_UNK_1446,                                 0x1446}, //+-
            {Opcode.CMSG_UNK_144D,                                 0x144D}, //+-
            {Opcode.CMSG_UNK_14E3,                                 0x14E3}, //+-
            {Opcode.CMSG_UNK_15A9,                                 0x15A9}, //++
            {Opcode.CMSG_UNK_1841,                                 0x1841}, //+-
            {Opcode.CMSG_UNK_19C2,                                 0x19C2}, //+-
            {Opcode.CMSG_UNK_1DAE,                                 0x1DAE}, //++
            {Opcode.CMSG_NULL_0023,                                0x0023}, //+-
            {Opcode.CMSG_NULL_0060,                                0x0060}, //+-
            {Opcode.CMSG_NULL_0141,                                0x0141}, //+-
            {Opcode.CMSG_NULL_01C0,                                0x01C0}, //+-
            {Opcode.CMSG_NULL_029F,                                0x029F}, //+-
            {Opcode.CMSG_NULL_02D6,                                0x02D6}, //+-
            {Opcode.CMSG_NULL_02DA,                                0x02DA}, //+-
            {Opcode.CMSG_NULL_032D,                                0x032D}, //+-
            {Opcode.CMSG_NULL_033D,                                0x033D}, //+-
            {Opcode.CMSG_NULL_0365,                                0x0365}, //+-
            {Opcode.CMSG_NULL_0374,                                0x0374}, //+-
            {Opcode.CMSG_NULL_03C4,                                0x03C4}, //+-
            {Opcode.CMSG_NULL_0558,                                0x0558}, //+-
            {Opcode.CMSG_NULL_05E1,                                0x05E1}, //+-
            {Opcode.CMSG_NULL_0640,                                0x0640}, //+-
            {Opcode.CMSG_NULL_0644,                                0x0644}, //+-
            {Opcode.CMSG_NULL_06D4,                                0x06D4}, //+-
            {Opcode.CMSG_NULL_06E4,                                0x06E4}, //+-
            {Opcode.CMSG_NULL_06F5,                                0x06F5}, //+-
            {Opcode.CMSG_NULL_077B,                                0x077B}, //+-
            {Opcode.CMSG_NULL_0813,                                0x0813}, //+-
            {Opcode.CMSG_NULL_0826,                                0x0826}, //+-
            {Opcode.CMSG_NULL_0A22,                                0x0A22}, //+-
            {Opcode.CMSG_NULL_0A23,                                0x0A23}, //+-
            {Opcode.CMSG_NULL_0A82,                                0x0A82}, //+-
            {Opcode.CMSG_NULL_0A87,                                0x0A87}, //+-
            {Opcode.CMSG_NULL_0C62,                                0x0C62}, //+-
            {Opcode.CMSG_NULL_0DE0,                                0x0DE0}, //+-
            {Opcode.CMSG_NULL_1124,                                0x1124}, //+-
            {Opcode.CMSG_NULL_1203,                                0x1203}, //+-
            {Opcode.CMSG_NULL_1207,                                0x1207}, //+-
            {Opcode.CMSG_NULL_1272,                                0x1272}, //+-
            {Opcode.CMSG_NULL_135B,                                0x135B}, //+-
            {Opcode.CMSG_NULL_1452,                                0x1452}, //+-
            {Opcode.CMSG_NULL_14DB,                                0x14DB}, //+-
            {Opcode.CMSG_NULL_14E0,                                0x14E0}, //+-
            {Opcode.CMSG_NULL_15A8,                                0x15A8}, //+-
            {Opcode.CMSG_NULL_15E2,                                0x15E2 | 0x10000}, //+-
            {Opcode.CMSG_NULL_1A23,                                0x1A23}, //+-
            {Opcode.CMSG_NULL_1A87,                                0x1A87}, //+-
            {Opcode.CMSG_NULL_1C45,                                0x1C45}, //+-
            {Opcode.CMSG_NULL_1C5A,                                0x1C5A}, //+-
            {Opcode.CMSG_NULL_1CE3,                                0x1CE3}, //+-
            {Opcode.CMSG_NULL_1D61,                                0x1D61}, //+-
            {Opcode.CMSG_NULL_1DC3,                                0x1DC3}, //+-
            {Opcode.CMSG_NULL_1F34,                                0x1F34}, //+-
            {Opcode.CMSG_NULL_1F89,                                0x1F89}, //+-
            {Opcode.CMSG_NULL_1F8E,                                0x1F8E}, //+-
            {Opcode.CMSG_NULL_1F9E,                                0x1F9E}, //+-
            {Opcode.CMSG_NULL_1FBE,                                0x1FBE}, //+-

            {Opcode.MSG_MOVE_FALL_LAND,                            0x08FA}, //++
            {Opcode.MSG_MOVE_HEARTBEAT,                            0x01F2}, //++
            {Opcode.MSG_MOVE_JUMP,                                 0x1153}, //+-
            {Opcode.MSG_MOVE_ROOT,                                 0x107A}, //++
            {Opcode.MSG_MOVE_SET_FACING,                           0x1050}, //+-
            {Opcode.MSG_MOVE_SET_PITCH,                            0x017A}, //+-
            {Opcode.MSG_MOVE_SET_RUN_MODE,                         0x0979}, //++
            {Opcode.MSG_MOVE_SET_WALK_MODE,                        0x08D1}, //++
            {Opcode.MSG_MOVE_START_ASCEND,                         0x11FA}, //+-
            {Opcode.MSG_MOVE_START_BACKWARD,                       0x09D8 | 0x10000}, //++
            {Opcode.MSG_MOVE_START_DESCEND,                        0x01D1}, //+-
            {Opcode.MSG_MOVE_START_FORWARD,                        0x095A}, //++
            {Opcode.MSG_MOVE_START_PITCH_DOWN,                     0x08D8}, //+-
            {Opcode.MSG_MOVE_START_PITCH_UP,                       0x00D8 | 0x10000}, //+-
            {Opcode.MSG_MOVE_START_STRAFE_LEFT,                    0x01F8}, //++
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT,                   0x1058}, //++
            {Opcode.MSG_MOVE_START_SWIM,                           0x1858}, //+-
            {Opcode.MSG_MOVE_START_TURN_LEFT,                      0x01D0}, //++
            {Opcode.MSG_MOVE_START_TURN_RIGHT,                     0x107B}, //+-
            {Opcode.MSG_MOVE_STOP,                                 0x08F1}, //++
            {Opcode.MSG_MOVE_STOP_ASCEND,                          0x115A}, //+-
            {Opcode.MSG_MOVE_STOP_PITCH,                           0x007A}, //+-
            {Opcode.MSG_MOVE_STOP_STRAFE,                          0x0171}, //++
            {Opcode.MSG_MOVE_STOP_SWIM,                            0x0950}, //+-
            {Opcode.MSG_MOVE_STOP_TURN,                            0x1170}, //++
            {Opcode.MSG_MOVE_UNROOT,                               0x1051}, //++
            {Opcode.MSG_MOVE_WORLDPORT_ACK,                        0x1FAD}, //++
            {Opcode.MSG_SET_RAID_DIFFICULTY,                       0x0591 | 0x10000}, //--
            {Opcode.MSG_VERIFY_CONNECTIVITY,                       0x4F57}, //--

            {Opcode.SMSG_ACCOUNT_DATA_TIMES,                       0x162B}, //++
            {Opcode.SMSG_ACHIEVEMENT_EARNED,                       0x080B}, //--
            {Opcode.SMSG_ACTION_BUTTONS,                           0x081A}, //++
            {Opcode.SMSG_ACTIVATETAXIREPLY,                        0x1043}, //++ ??
            {Opcode.SMSG_ADDON_INFO,                               0x160A}, //--
            {Opcode.SMSG_AI_REACTION,                              0x06AF}, //++
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA_ACCOUNT,             0x0A9E}, //++
            {Opcode.SMSG_ALL_ACHIEVEMENT_DATA_PLAYER,              0x180A}, //++
            {Opcode.SMSG_ARENA_SEASON_WORLD_STATE,                 0x069B}, //++
            {Opcode.SMSG_ATTACKERSTATEUPDATE,                      0x06AA}, //--
            {Opcode.SMSG_ATTACKSTART,                              0x1A9E}, //--
            {Opcode.SMSG_ATTACKSTOP,                               0x12AF}, //--
            {Opcode.SMSG_AUCTION_HELLO,                            0x10A7}, //+-
            {Opcode.SMSG_AURA_UPDATE,                              0x0072}, //++
            {Opcode.SMSG_AUTH_CHALLENGE,                           0x0949}, //--+
            {Opcode.SMSG_AUTH_RESPONSE,                            0x0ABA}, //++
            {Opcode.SMSG_BARBER_SHOP_RESULT,                       0x0C3F}, //--
            {Opcode.SMSG_BATTLEFIELD_LIST,                         0x160E}, //--
            {Opcode.SMSG_BATTLEFIELD_MGR_EJECTED,                  0x18C2}, //--
            {Opcode.SMSG_BATTLEFIELD_MGR_ENTERED,                  0x081B}, //--
            {Opcode.SMSG_BATTLEFIELD_STATUS,                       0x0433}, //--
            {Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED,                0x122E}, //--
            {Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE,                0x1AAF}, //--
            {Opcode.SMSG_BATTLEFIELD_STATUS_NEEDCONFIRMATION,      0x1EAF}, //--
            {Opcode.SMSG_BATTLEFIELD_STATUS_FAILED,                0x1140}, //--
            {Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED,               0x1E2F}, //--
            {Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT,                 0x0206 | 0x20000}, //+-
            {Opcode.SMSG_BATTLE_PET_DELETED,                       0x18AB}, //+-
            {Opcode.SMSG_BATTLE_PET_JOURNAL,                       0x1542}, //--
            {Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED,         0x1A0F}, //--
            {Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED,           0x0203}, //--
            {Opcode.SMSG_BATTLE_PET_QUERY_NAME_RESPONSE,           0x1540}, //+-
            {Opcode.SMSG_BATTLE_PET_SLOT_UPDATE,                   0x16AF}, //--
            {Opcode.SMSG_BATTLE_PET_UPDATES,                       0x041A}, //--
            {Opcode.SMSG_BINDER_CONFIRM,                           0x1287}, //--
            {Opcode.SMSG_BINDPOINTUPDATE,                          0x0E3B}, //++
            {Opcode.SMSG_BLACKMARKET_HELLO,                        0x00AE}, //--
            {Opcode.SMSG_BLACKMARKET_REQUEST_ITEMS_RESULT,         0x128B}, //--
            //{Opcode.SMSG_BLACKMARKET_BID_RESULT,                   0x18BA}, //-- bad id
            {Opcode.SMSG_BUY_FAILED,                               0x1563}, //--
            {Opcode.SMSG_BUY_ITEM,                                 0x101A}, //--
            {Opcode.SMSG_CANCEL_COMBAT,                            0x0534}, //--
            {Opcode.SMSG_CAST_FAILED,                              0x143A}, //++
            {Opcode.SMSG_CHANNEL_NOTIFY,                           0x0F06}, //--
            {Opcode.SMSG_CHANNEL_START,                            0x10F9}, //++
            {Opcode.SMSG_CHANNEL_UPDATE,                           0x11D9 | 0x20000}, //--
            {Opcode.SMSG_CHAR_CREATE,                              0x1CAA}, //--
            {Opcode.SMSG_CHAR_DELETE,                              0x0C9F}, //--
            {Opcode.SMSG_CHAR_ENUM,                                0x11C3}, //++
            {Opcode.SMSG_CHAT_PLAYER_NOT_FOUND,                    0x1082}, //--
            {Opcode.SMSG_CLIENTCACHE_VERSION,                      0x002A}, //++
            {Opcode.SMSG_CONTACT_LIST,                             0x1F22}, //--
            {Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE,       0x1A3A}, //--
            {Opcode.SMSG_CORPSE_NOT_IN_INSTANCE,                   0x089E}, //--
            {Opcode.SMSG_CORPSE_QUERY_RESPONSE,                    0x0E0B}, //--
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY,                     0x022A}, //++
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE,                  0x048B}, //--
            {Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT,                  0x189E}, //++
            {Opcode.SMSG_CRITERIA_UPDATE_PLAYER,                   0x0E9B}, //++
            {Opcode.SMSG_DB_REPLY,                                 0x103B}, //++
            {Opcode.SMSG_DEATH_RELEASE_LOC,                        0x1063 | 0x20000}, //+-
            {Opcode.SMSG_DEFENSE_MESSAGE,                          0x0A1F}, //--
            {Opcode.SMSG_DESTROY_OBJECT,                           0x14C2 | 0x20000}, //++
            {Opcode.SMSG_DUEL_COMPLETE,                            0x1C0A}, //--
            {Opcode.SMSG_DUEL_COUNTDOWN,                           0x129F}, //--
            {Opcode.SMSG_DUEL_INBOUNDS,                            0x163A}, //--
            {Opcode.SMSG_DUEL_OUTOFBOUNDS,                         0x001A}, //--
            {Opcode.SMSG_DUEL_REQUESTED,                           0x0022}, //--
            {Opcode.SMSG_DUEL_WINNER,                              0x10E1}, //--
            {Opcode.SMSG_EMOTE,                                    0x002E}, //++ ??
            {Opcode.SMSG_ENABLE_BARBER_SHOP,                       0x1222}, //--
            {Opcode.SMSG_ENVIRONMENTALDAMAGELOG,                   0x0DF1}, //--
            {Opcode.SMSG_EQUIPMENT_SET_LIST,                       0x18E2}, //++
            {Opcode.SMSG_EXPLORATION_EXPERIENCE,                   0x189A}, //--
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS,                    0x16BB}, //++
            {Opcode.SMSG_FLIGHT_SPLINE_SYNC,                       0x0063}, //++
            {Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS,                0x0969}, //--
            {Opcode.SMSG_FRIEND_STATUS,                            0x0532}, //--
            {Opcode.SMSG_GAME_STORE_INGAME_BUY_FAILED,             0x023A}, //++
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE,                0x06BF}, //--
            {Opcode.SMSG_GMRESPONSE_RECEIVED,                      0x148E | 0x20000}, //++
            {Opcode.SMSG_GOSSIP_COMPLETE,                          0x034E}, //--
            {Opcode.SMSG_GOSSIP_MESSAGE,                           0x0244}, //--
            {Opcode.SMSG_GOSSIP_POI,                               0x0785}, //-- +
            {Opcode.SMSG_GROUP_DECLINE,                            0x17A3}, //--
            {Opcode.SMSG_GROUP_DESTROYED,                          0x1B27}, //--
            {Opcode.SMSG_GROUP_INVITE,                             0x0A8F}, //--
            {Opcode.SMSG_GROUP_LIST,                               0x0CBB}, //--
            {Opcode.SMSG_GROUP_SET_LEADER,                         0x18BF}, //--
            {Opcode.SMSG_GROUP_SET_ROLE,                           0x1E1F}, //--
            {Opcode.SMSG_GUILD_BANK_LIST,                          0x0B79}, //--
            {Opcode.SMSG_GUILD_BANK_LOG_QUERY_RESULT,              0x0FF0}, //--
            {Opcode.SMSG_GUILD_CHALLENGE_UPDATED,                  0x0AE9}, //--
            {Opcode.SMSG_GUILD_COMMAND_RESULT,                     0x0EF1}, //--
            {Opcode.SMSG_GUILD_EVENT_LOG_QUERY_RESULT,             0x1AF1}, //--
            {Opcode.SMSG_GUILD_INVITE,                             0x0F71}, //--
            {Opcode.SMSG_GUILD_INVITE_CANCEL,                      0x0FE1}, //++
            {Opcode.SMSG_GUILD_MEMBER_DAILY_RESET,                 0x1BE8}, //--
            {Opcode.SMSG_GUILD_NEWS_UPDATE,                        0x0AE8}, //--
            {Opcode.SMSG_GUILD_QUERY_RESPONSE,                     0x1B79}, //++
            {Opcode.SMSG_GUILD_RANK,                               0x0A79}, //--
            {Opcode.SMSG_GUILD_RANKS_UPDATE,                       0x0A60}, //--
            {Opcode.SMSG_GUILD_REPUTATION_WEEKLY_CAP,              0x1A71}, //--
            {Opcode.SMSG_GUILD_REWARDS_LIST,                       0x1A69}, //--
            {Opcode.SMSG_GUILD_ROSTER,                             0x0BE0}, //--
            {Opcode.SMSG_GUILD_XP,                                 0x0AF0}, //--
            {Opcode.SMSG_GUILD_XP_GAIN,                            0x0FE0}, //--
            {Opcode.SMSG_HIGHEST_THREAT_UPDATE,                    0x14AE}, //++
            {Opcode.SMSG_HOTFIX_INFO,                              0x1EBA}, //--
            {Opcode.SMSG_INIT_CURRENCY,                            0x1A8B}, //++
            {Opcode.SMSG_INIT_WORLD_STATES,                        0x1560}, //--
            {Opcode.SMSG_INITIAL_SPELLS,                           0x045A}, //++
            {Opcode.SMSG_INITIALIZE_FACTIONS,                      0x0AAA}, //++
            {Opcode.SMSG_INSTANCE_RESET,                           0x160F}, //--
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE,                 0x0C1E}, //--
            {Opcode.SMSG_ITEM_ENCHANT_TIME_UPDATE,                 0x10A2}, //++
            {Opcode.SMSG_ITEM_PUSH_RESULT,                         0x0E0A}, //++
            {Opcode.SMSG_ITEM_TIME_UPDATE,                         0x18C1}, //--
            {Opcode.SMSG_LEARNED_SPELL,                            0x129A}, //++
            {Opcode.SMSG_LIST_INVENTORY,                           0x1AAE}, //--
            {Opcode.SMSG_LOAD_CUF_PROFILES,                        0x0E32}, //++
            {Opcode.SMSG_LOG_XPGAIN,                               0x1E9A}, //--
            {Opcode.SMSG_LOGIN_SETTIMESPEED,                       0x082B}, //++
            {Opcode.SMSG_LOGIN_VERIFY_WORLD,                       0x1C0F}, //--
            {Opcode.SMSG_LOGOUT_CANCEL_ACK,                        0x0AAF}, //++
            {Opcode.SMSG_LOGOUT_COMPLETE,                          0x142F}, //+-
            {Opcode.SMSG_LOGOUT_RESPONSE,                          0x008F}, //+-
            {Opcode.SMSG_LOOT_CLEAR_MONEY,                         0x1632}, //--
            {Opcode.SMSG_LOOT_MONEY_NOTIFY,                        0x14C0}, //--
            {Opcode.SMSG_LOOT_RELEASE_RESPONSE,                    0x123F}, //--
            {Opcode.SMSG_LOOT_REMOVED,                             0x0C3E}, //++
            {Opcode.SMSG_LOOT_RESPONSE,                            0x128A}, //--
            {Opcode.SMSG_MAIL_LIST_RESULT,                         0x1C0B}, //--
            {Opcode.SMSG_MESSAGECHAT,                              0x1A9A}, //++
            {Opcode.SMSG_MINIMAP_PING,                             0x168F}, //--
            {Opcode.SMSG_MONSTER_MOVE,                             0x1A07}, //++
            {Opcode.SMSG_MOTD,                                     0x183B}, //++
            {Opcode.SMSG_MOVE_ROOT,                                0x15AE}, //++
            {Opcode.SMSG_MOVE_SET_ACTIVE_MOVER,                    0x0C6D}, //+-
            {Opcode.SMSG_MOVE_SET_CAN_FLY,                         0x178D}, //++
            {Opcode.SMSG_MOVE_SET_FLIGHT_SPEED,                    0x006E}, //++
            {Opcode.SMSG_MOVE_SET_RUN_SPEED,                       0x184C}, //++
            {Opcode.SMSG_MOVE_SET_RUN_BACK_SPEED,                  0x0A83}, //++
            {Opcode.SMSG_MOVE_SET_SWIM_SPEED,                      0x0817}, //++
            {Opcode.SMSG_MOVE_SET_WALK_SPEED,                      0x0469}, //++
            {Opcode.SMSG_MOVE_TELEPORT,                            0x0B39}, //++
            {Opcode.SMSG_MOVE_UNROOT,                              0x1FAE}, //++
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY,                       0x0162}, //++
            {Opcode.SMSG_NAME_QUERY_RESPONSE,                      0x169B}, //++
            {Opcode.SMSG_NEW_WORLD,                                0x1C3B}, //++
            {Opcode.SMSG_NOTIFICATION,                             0x0C2A}, //--
            {Opcode.SMSG_NPC_TEXT_UPDATE,                          0x140A}, //--
            {Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE,                 0x081E}, //--
            {Opcode.SMSG_PARTY_COMMAND_RESULT,                     0x0F86}, //--
            {Opcode.SMSG_PARTY_MEMBER_STATS,                       0x0A9A}, //-+
            {Opcode.SMSG_PERIODICAURALOG,                          0x0CF2}, //++
            {Opcode.SMSG_PET_NAME_QUERY_RESPONSE,                  0x0ABE}, //--
            {Opcode.SMSG_PET_SPELLS,                               0x095A | 0x20000}, //--
            {Opcode.SMSG_PETITION_ALREADY_SIGNED,                  0x0286}, //--
            {Opcode.SMSG_PETITION_QUERY_RESPONSE,                  0x1083}, //--
            {Opcode.SMSG_PETITION_RENAME_RESULT,                   0x082A}, //--
            {Opcode.SMSG_PETITION_SHOWLIST,                        0x10A3}, //--
            {Opcode.SMSG_PETITION_SHOW_SIGNATURES,                 0x00AA}, //--
            {Opcode.SMSG_PETITION_SIGN_RESULTS,                    0x06AE}, //--
            {Opcode.SMSG_PLAY_SOUND,                               0x102A}, //++
            {Opcode.SMSG_PLAYED_TIME,                              0x11E2}, //+-
            {Opcode.SMSG_PLAYER_MOVE,                              0x1A32}, //++
            {Opcode.SMSG_PLAYERBOUND,                              0x1B60}, //--
            {Opcode.SMSG_PONG,                                     0x1969}, //--
            {Opcode.SMSG_POWER_UPDATE,                             0x109F}, //++
            {Opcode.SMSG_PVP_LOG_DATA,                             0x1E8F}, //--
            {Opcode.SMSG_QUERY_TIME_RESPONSE,                      0x100F}, //++
            {Opcode.SMSG_QUESTGIVER_OFFER_REWARD,                  0x074F}, //--
            {Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE,                0x0346}, //--
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS,                 0x134C}, //--
            {Opcode.SMSG_QUESTGIVER_QUEST_LIST,                    0x02D4}, //--
            {Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS,                 0x0277}, //--
            {Opcode.SMSG_QUESTGIVER_STATUS,                        0x1275}, //++
            {Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE,               0x06CE}, //++
            {Opcode.SMSG_QUESTLOG_FULL,                            0x07FD}, //--
            {Opcode.SMSG_QUESTUPDATE_ADD_KILL,                     0x1645}, //--
            {Opcode.SMSG_QUESTUPDATE_COMPLETE,                     0x0776}, //--
            {Opcode.SMSG_QUEST_CONFIRM_ACCEPT,                     0x13C7}, //--
            {Opcode.SMSG_QUEST_POI_QUERY_RESPONSE,                 0x067F}, //++
            {Opcode.SMSG_QUEST_QUERY_RESPONSE,                     0x0276}, //--
            {Opcode.SMSG_RAID_READY_CHECK,                         0x1C8E}, //--
            {Opcode.SMSG_RAID_READY_CHECK_COMPLETED,               0x15C2}, //--
            {Opcode.SMSG_RAID_READY_CHECK_CONFIRM,                 0x02AF}, //--
            {Opcode.SMSG_RANDOM_ROLL,                              0x141A}, //--
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME,                      0x169F}, //--
            {Opcode.SMSG_REALM_QUERY_RESPONSE,                     0x063E}, //--
            {Opcode.SMSG_RECEIVED_MAIL,                            0x182B}, //--
            {Opcode.SMSG_REDIRECT_CLIENT,                          0x1149}, //--
            {Opcode.SMSG_REFORGE_RESULT,                           0x141E}, //--
            {Opcode.SMSG_REMOVED_SPELL,                            0x14C3}, //+-
            {Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE,             0x08AA}, //--
            {Opcode.SMSG_SELL_ITEM,                                0x048E}, //--
            {Opcode.SMSG_SEND_MAIL_RESULT,                         0x1A9B}, //--
            {Opcode.SMSG_SEND_UNLEARN_SPELLS,                      0x10F1}, //++
            {Opcode.SMSG_SET_DUNGEON_DIFFICULTY,                   0x1283}, //--
            {Opcode.SMSG_SET_FACTION_ATWAR,                        0x0C9B}, //--
            {Opcode.SMSG_SET_FACTION_STANDING,                     0x10AA}, //--
            {Opcode.SMSG_SET_FLAT_SPELL_MODIFIER,                  0x10F2 | 0x20000}, //++
            {Opcode.SMSG_SET_FORCED_REACTIONS,                     0x068F}, //++
            {Opcode.SMSG_SET_PCT_SPELL_MODIFIER,                   0x09D3}, //++
            {Opcode.SMSG_SET_PHASE_SHIFT,                          0x02A2}, //++
            {Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT,         0x180E}, //++
            {Opcode.SMSG_SET_PROFICIENCY,                          0x1440}, //+-
            {Opcode.SMSG_SET_RAID_DIFFICULTY,                      0x0591}, //--
            {Opcode.SMSG_SET_TIMEZONE_INFORMATION,                 0x19C1}, //++
            {Opcode.SMSG_SET_VIGNETTE,                             0x0CBE}, //++
            {Opcode.SMSG_SHOW_BANK,                                0x0007}, //++
            {Opcode.SMSG_SHOWTAXINODES,                            0x1E1A}, //++
            {Opcode.SMSG_SPELL_CATEGORY_COOLDOWN,                  0x01DB}, //++
            {Opcode.SMSG_SPELL_COOLDOWN,                           0x0452}, //++
            {Opcode.SMSG_SPELL_DELAYED,                            0x087A}, //++
            {Opcode.SMSG_SPELL_FAILED_OTHER,                       0x040B}, //++
            {Opcode.SMSG_SPELL_FAILURE,                            0x04AF}, //++
            {Opcode.SMSG_SPELL_GO,                                 0x09D8}, //++
            {Opcode.SMSG_SPELL_START,                              0x107A | 0x20000}, //++
            {Opcode.SMSG_SPELLDISPELLOG,                           0x0DF9}, //--
            {Opcode.SMSG_SPELLENERGIZELOG,                         0x0D79 | 0x20000}, //--
            {Opcode.SMSG_SPELLHEALLOG,                             0x09FB | 0x20000}, //--
            {Opcode.SMSG_SPELLLOGEXECUTE,                          0x00D8}, //--
            {Opcode.SMSG_SPELLNONMELEEDAMAGELOG,                   0x1450 | 0x20000}, //++
            {Opcode.SMSG_SPLINE_MOVE_SET_FLIGHT_SPEED,             0x1DAB}, //++
            {Opcode.SMSG_SPLINE_MOVE_SET_RUN_BACK_SPEED,           0x1F9F}, //++
            {Opcode.SMSG_SPLINE_MOVE_SET_RUN_SPEED,                0x02F1}, //++
            {Opcode.SMSG_SPLINE_MOVE_SET_SWIM_SPEED,               0x1D8E}, //++
            {Opcode.SMSG_SPLINE_MOVE_SET_WALK_SPEED,               0x08B2}, //++
            {Opcode.SMSG_STANDSTATE_UPDATE,                        0x1C12}, //++
            {Opcode.SMSG_START_TIMER,                              0x0E3F}, //+-
            {Opcode.SMSG_START_MIRROR_TIMER,                       0x0E12}, //++
            {Opcode.SMSG_STOP_MIRROR_TIMER,                        0x1026}, //++
            {Opcode.SMSG_SUSPEND_COMMS,                            0x1D48}, //--
            {Opcode.SMSG_TALENTS_INFO,                             0x0A9B}, //++
            {Opcode.SMSG_TEXT_EMOTE,                               0x002E | 0x20000}, //--
            {Opcode.SMSG_THREAT_CLEAR,                             0x180B}, //++
            {Opcode.SMSG_THREAT_REMOVE,                            0x1E0F}, //++ ??
            {Opcode.SMSG_THREAT_UPDATE,                            0x0632}, //++
            {Opcode.SMSG_TIME_SYNC_REQ,                            0x1A8F}, //++
            {Opcode.SMSG_TRAINER_LIST,                             0x189F}, //--
            {Opcode.SMSG_TRANSFER_PENDING,                         0x061B}, //++
            {Opcode.SMSG_TRIGGER_CINEMATIC,                        0x0B01}, //--
            {Opcode.SMSG_TRIGGER_MOVIE,                            0x1C2E}, //--
            {Opcode.SMSG_TURN_IN_PETITION_RESULTS,                 0x0E13}, //--
            {Opcode.SMSG_TUTORIAL_FLAGS,                           0x1B90}, //++
            {Opcode.SMSG_UPDATE_ACCOUNT_DATA,                      0x0AAE}, //++
            {Opcode.SMSG_UPDATE_COMBO_POINTS,                      0x082F}, //++
            {Opcode.SMSG_UPDATE_LAST_INSTANCE,                     0x189B}, //--
            {Opcode.SMSG_UPDATE_OBJECT,                            0x1792}, //--
            {Opcode.SMSG_UPDATE_WORLD_STATE,                       0x121B}, //++
            {Opcode.SMSG_VOID_ITEM_SWAP_RESPONSE,                  0x1EBF}, //--
            {Opcode.SMSG_VOID_STORAGE_CONTENTS,                    0x008B}, //--
            {Opcode.SMSG_VOID_STORAGE_FAILED,                      0x1569}, //--
            {Opcode.SMSG_VOID_STORAGE_TRANSFER_CHANGES,            0x14BA}, //--
            {Opcode.SMSG_VOID_TRANSFER_RESULT,                     0x1C9E}, //+-
            {Opcode.SMSG_WARDEN_DATA,                              0x0C0A}, //-- ??
            {Opcode.SMSG_WEATHER,                                  0x06AB}, //++
            {Opcode.SMSG_WHO,                                      0x161B}, //--
            {Opcode.SMSG_WORLD_SERVER_INFO,                        0x0082}, //++
            {Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE,              0x0027}, //--
            {Opcode.SMSG_ZONE_UNDER_ATTACK,                        0x10C2}, //--
            {Opcode.SMSG_UNK_0002,                                 0x0002}, //+-
            {Opcode.SMSG_UNK_001F,                                 0x001F}, //++
            {Opcode.SMSG_UNK_0050,                                 0x0050}, //++
            {Opcode.SMSG_UNK_00A3,                                 0x00A3}, //++
            {Opcode.SMSG_UNK_00AB,                                 0x00AB}, //++
            {Opcode.SMSG_UNK_00F9,                                 0x00F9}, //++
            {Opcode.SMSG_UNK_01D2,                                 0x01D2}, //++
            {Opcode.SMSG_UNK_01E1,                                 0x01E1}, //++
            {Opcode.SMSG_UNK_021A,                                 0x021A}, //++
            {Opcode.SMSG_UNK_0250,                                 0x0250}, //++
            {Opcode.SMSG_UNK_02A7,                                 0x02A7}, //+-
            {Opcode.SMSG_UNK_036D,                                 0x036D}, //++
            {Opcode.SMSG_UNK_042A,                                 0x042A}, //++
            {Opcode.SMSG_UNK_043F,                                 0x043F}, //++
            {Opcode.SMSG_UNK_048A,                                 0x048A}, //++
            {Opcode.SMSG_UNK_0562,                                 0x0562}, //++
            {Opcode.SMSG_UNK_05F3,                                 0x05F3}, //++
            {Opcode.SMSG_UNK_068E,                                 0x068E}, //++
            {Opcode.SMSG_UNK_069F,                                 0x069F}, //++
            {Opcode.SMSG_UNK_0728,                                 0x0728}, //++
            {Opcode.SMSG_UNK_0845,                                 0x0845}, //++
            {Opcode.SMSG_UNK_0865,                                 0x0865}, //++
            {Opcode.SMSG_UNK_0868,                                 0x0868}, //++
            {Opcode.SMSG_UNK_089B,                                 0x089B}, //++
            {Opcode.SMSG_UNK_0987,                                 0x0987}, //--
            {Opcode.SMSG_UNK_09F8,                                 0x09F8}, //++
            {Opcode.SMSG_UNK_0A03,                                 0x0A03}, //++
            {Opcode.SMSG_UNK_0A27,                                 0x0A27}, //++
            {Opcode.SMSG_UNK_0A0B,                                 0x0A0B}, //++
            {Opcode.SMSG_UNK_0A3F,                                 0x0A3F}, //++
            {Opcode.SMSG_UNK_0A8B,                                 0x0A8B}, //++
            {Opcode.SMSG_UNK_0C32,                                 0x0C32}, //++
            {Opcode.SMSG_UNK_0C44,                                 0x0C44}, //+-
            {Opcode.SMSG_UNK_0CAE,                                 0x0CAE}, //++
            {Opcode.SMSG_UNK_0D51,                                 0x0D51}, //++
            {Opcode.SMSG_UNK_0D79,                                 0x0D79}, //++
            {Opcode.SMSG_UNK_0E2A,                                 0x0E2A}, //++
            {Opcode.SMSG_UNK_0EAB,                                 0x0EAB}, //++
            {Opcode.SMSG_UNK_0EBA,                                 0x0EBA}, //++
            {Opcode.SMSG_UNK_102E,                                 0x102E}, //++
            {Opcode.SMSG_UNK_103E,                                 0x103E}, //++
            {Opcode.SMSG_UNK_108B,                                 0x108B}, //++
            {Opcode.SMSG_UNK_109A,                                 0x109A}, //++
            {Opcode.SMSG_UNK_1163,                                 0x1163}, //++
            {Opcode.SMSG_UNK_11E1,                                 0x11E1}, //++
            {Opcode.SMSG_UNK_11E3,                                 0x11E3}, //+-
            {Opcode.SMSG_UNK_121E,                                 0x121E}, //++
            {Opcode.SMSG_UNK_1227,                                 0x1227}, //++
            {Opcode.SMSG_UNK_129B,                                 0x129B}, //++
            {Opcode.SMSG_UNK_1443,                                 0x1443}, //++
            {Opcode.SMSG_UNK_148F,                                 0x148F}, //++
            {Opcode.SMSG_UNK_1570,                                 0x1570}, //+-
            {Opcode.SMSG_UNK_159F,                                 0x159F}, //++
            {Opcode.SMSG_UNK_15E2,                                 0x15E2}, //++
            {Opcode.SMSG_UNK_1613,                                 0x1613}, //++
            {Opcode.SMSG_UNK_16BF,                                 0x16BF}, //++
            {Opcode.SMSG_UNK_1861,                                 0x1861}, //++
            {Opcode.SMSG_UNK_188F,                                 0x188F}, //++
            {Opcode.SMSG_UNK_18BA,                                 0x18BA}, //++
            {Opcode.SMSG_UNK_18C3,                                 0x18C3}, //++
            {Opcode.SMSG_UNK_1961,                                 0x1961}, //++
            {Opcode.SMSG_UNK_1E12,                                 0x1E12}, //++
            {Opcode.SMSG_UNK_1E9B,                                 0x1E9B}, //++
            {Opcode.SMSG_NULL_04BB,                                0x04BB}, //++
            {Opcode.SMSG_NULL_0C59,                                0x0C59}, //++
            {Opcode.SMSG_NULL_0C9A,                                0x0C9A}, //++
            {Opcode.SMSG_NULL_0E2B,                                0x0E2B}, //++
            {Opcode.SMSG_NULL_0E8B,                                0x0E8B}, //++
            {Opcode.SMSG_NULL_141B,                                0x141B}, //++
            {Opcode.SMSG_NULL_1A2A,                                0x1A2A}, //++
        };
    }
}
