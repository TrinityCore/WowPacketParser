using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version.V5_4_7_18019
{
    public static class Opcodes_5_4_7
    {
        public static BiDictionary<Opcode, int> Opcodes()
        {
            return Opcs;
        }

        private static readonly BiDictionary<Opcode, int> Opcs = new BiDictionary<Opcode, int>
        {
            {Opcode.CMSG_ADD_FRIEND,                               0x064F},
            {Opcode.CMSG_ADD_IGNORE,                               0x126D},
            {Opcode.CMSG_AREATRIGGER,                              0x155A},
            {Opcode.CMSG_ATTACKSTOP,                               0x1E13},
            {Opcode.CMSG_ATTACKSWING,                              0x1513},
            {Opcode.CMSG_AUCTION_HELLO,                            0x047F},
            {Opcode.CMSG_AUCTION_LIST_ITEMS,                       0x105F},
            {Opcode.CMSG_AUCTION_LIST_OWNER_ITEMS,                 0x105E},
            {Opcode.CMSG_AUCTION_LIST_PENDING_SALES,               0x1055},
            {Opcode.CMSG_AUCTION_SELL_ITEM,                        0x07F5},
            {Opcode.CMSG_AUTH_SESSION,                             0x1A51},
            {Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES,               0x0174}, //
            {Opcode.CMSG_AUTOBANK_ITEM,                            0x1C42}, //
            {Opcode.CMSG_AUTOEQUIP_ITEM,                           0x166B},
            {Opcode.CMSG_AUTOSTORE_BAG_ITEM,                       0x162B}, //
            {Opcode.CMSG_AUTOSTORE_BANK_ITEM,                      0x176A}, //
            {Opcode.CMSG_AUTOSTORE_LOOT_ITEM,                      0x1F7A},
            {Opcode.CMSG_BANKER_ACTIVATE,                          0x02FD},
            {Opcode.CMSG_BEGIN_TRADE,                              0x0C9A}, //
            {Opcode.CMSG_BINDER_ACTIVATE,                          0x0477},
            {Opcode.CMSG_BUYBACK_ITEM,                             0x07D7},
            {Opcode.CMSG_BUY_BANK_SLOT,                            0x00FE},
            {Opcode.CMSG_BUY_ITEM,                                 0x1077},
            {Opcode.CMSG_CALENDAR_ADD_EVENT,                       0x16D0}, //
            {Opcode.CMSG_CALENDAR_EVENT_INVITE,                    0x1551}, //
            {Opcode.CMSG_CALENDAR_REMOVE_EVENT,                    0x1DA1},
            {Opcode.CMSG_CANCEL_AURA,                              0x16E1},
            {Opcode.CMSG_CANCEL_CAST,                              0x12EB},
            {Opcode.CMSG_CANCEL_MOUNT_AURA,                        0x1552},
            {Opcode.CMSG_CANCEL_TRADE,                             0x1D32},
            {Opcode.CMSG_CAST_SPELL,                               0x1E5B},
            {Opcode.CMSG_CHAR_CREATE,                              0x09B9},
            {Opcode.CMSG_CHAR_DELETE,                              0x113B}, //OK
            {Opcode.CMSG_CHAR_ENUM,                                0x12C2},
            {Opcode.CMSG_CONNECT_TO_FAILED,                        0x16C8}, //?
            {Opcode.CMSG_CORPSE_MAP_POSITION_QUERY,                0x0152}, //-+
            {Opcode.CMSG_CORPSE_QUERY,                             0x129B}, //
            {Opcode.CMSG_CREATURE_QUERY,                           0x1E72},
            {Opcode.CMSG_DESTROY_ITEM,                             0x1F12},
            {Opcode.CMSG_DUEL_PROPOSED,                            0x19B3}, //
            {Opcode.CMSG_EMOTE,                                    0x12C5}, //
            {Opcode.CMSG_GAMEOBJECT_QUERY,                         0x14EA},
            {Opcode.CMSG_GAMEOBJ_REPORT_USE,                       0x06DF}, // 5.4.7 18019
            {Opcode.CMSG_GAMEOBJ_USE,                              0x055F},
            {Opcode.CMSG_GAME_STORE_BUY,                           0x1A83}, //?
            {Opcode.CMSG_GAME_STORE_LIST,                          0x1993}, //?
            {Opcode.CMSG_GET_MAIL_LIST,                            0x07DD},
            {Opcode.CMSG_GMTICKET_CREATE,                          0x103B}, //
            {Opcode.CMSG_GMTICKET_SYSTEMSTATUS,                    0x128A}, //
            {Opcode.CMSG_GOSSIP_HELLO,                             0x05F6},
            {Opcode.CMSG_GOSSIP_SELECT_OPTION,                     0x02D7},
            {Opcode.CMSG_GROUP_DISBAND,                            0x0DB2},
            {Opcode.CMSG_GROUP_INVITE,                             0x1990},
            {Opcode.CMSG_GROUP_INVITE_RESPONSE,                    0x1C51},
            {Opcode.CMSG_GROUP_RAID_CONVERT,                       0x19A0},
            {Opcode.CMSG_GROUP_SET_LEADER,                         0x1383},
            {Opcode.CMSG_GROUP_SET_ROLES,                          0x1C93},
            {Opcode.CMSG_GROUP_UNINVITE_GUID,                      0x0989},
            {Opcode.CMSG_GUILD_ACCEPT,                             0x18A3}, //
            {Opcode.CMSG_GUILD_ADD_RANK,                           0x1935}, //
            {Opcode.CMSG_GUILD_BANKER_ACTIVATE,                    0x02F6},
            {Opcode.CMSG_GUILD_BANK_BUY_TAB,                       0x02D6}, //
            {Opcode.CMSG_GUILD_BANK_LOG_QUERY,                     0x1D97},
            {Opcode.CMSG_GUILD_BANK_MONEY_WITHDRAWN_QUERY,         0x03FC},
            {Opcode.CMSG_GUILD_BANK_QUERY_TAB,                     0x07DC},
            {Opcode.CMSG_GUILD_BANK_QUERY_TEXT,                    0x19A6},
            {Opcode.CMSG_GUILD_BANK_SWAP_ITEMS,                    0x02DF},
            {Opcode.CMSG_GUILD_BANK_UPDATE_TAB,                    0x1054},
            //{Opcode.CMSG_GUILD_DECLINE,                            0x1B05}, // dublicate
            {Opcode.CMSG_GUILD_DEL_RANK,                           0x1D3C}, //
            {Opcode.CMSG_GUILD_DEMOTE,                             0x1B1C}, //
            {Opcode.CMSG_GUILD_DISBAND,                            0x190E}, //
            {Opcode.CMSG_GUILD_EVENT_LOG_QUERY,                    0x1D17},
            {Opcode.CMSG_GUILD_INFO_TEXT,                          0x1DAD},
            {Opcode.CMSG_GUILD_INVITE,                             0x188B}, //
            {Opcode.CMSG_GUILD_LEAVE,                              0x193D}, //
            {Opcode.CMSG_GUILD_MOTD,                               0x1DB4}, //
            {Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY,                 0x1984}, //
            {Opcode.CMSG_GUILD_PERMISSIONS,                        0x1D0F}, //
            {Opcode.CMSG_GUILD_PROMOTE,                            0x19B5}, //
            {Opcode.CMSG_GUILD_QUERY,                              0x1280}, //
            {Opcode.CMSG_GUILD_QUERY_NEWS,                         0x1D35}, //
            {Opcode.CMSG_GUILD_QUERY_RANKS,                        0x1BBC}, //
            {Opcode.CMSG_GUILD_REMOVE,                             0x1D9F}, //
            {Opcode.CMSG_GUILD_REQUEST_CHALLENGE_UPDATE,           0x19A3},
            {Opcode.CMSG_GUILD_ROSTER,                             0x19BC}, //
            {Opcode.CMSG_GUILD_SET_GUILD_MASTER,                   0x189B}, //
            {Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS,               0x19BD}, //
            {Opcode.CMSG_INITIATE_TRADE,                           0x12BB}, //
            //{Opcode.CMSG_INSPECT,                                  0x01D4}, //dubl
            {Opcode.CMSG_JOIN_CHANNEL,                             0x1D20},
            {Opcode.CMSG_LEARN_TALENT,                             0x1F5A},
            {Opcode.CMSG_LF_GUILD_GET_RECRUITS,                    0x198C},
            {Opcode.CMSG_LF_GUILD_POST_REQUEST,                    0x1B3E},
            {Opcode.CMSG_LF_GUILD_BROWSE,                          0x0992},
            {Opcode.CMSG_LFG_GET_STATUS,                           0x19AA}, //
            {Opcode.CMSG_LFG_JOIN,                                 0x18B8}, //
            {Opcode.CMSG_LIST_INVENTORY,                           0x10DD},
            {Opcode.CMSG_LOAD_SCREEN,                              0x1691},
            {Opcode.CMSG_LOGOUT_CANCEL,                            0x11D4},
            {Opcode.CMSG_LOGOUT_REQUEST,                           0x0476},
            {Opcode.CMSG_LOG_DISCONNECT,                           0x1A13},
            {Opcode.CMSG_LOOT,                                     0x1E52},
            {Opcode.CMSG_LOOT_METHOD,                              0x1C10},
            //{Opcode.CMSG_LOOT_MONEY,                               0x15A0}, // dublicate
            {Opcode.CMSG_LOOT_RELEASE,                             0x12F0},
            {Opcode.CMSG_MAIL_CREATE_TEXT_ITEM,                    0x0254}, //
            {Opcode.CMSG_MAIL_DELETE,                              0x13A3}, //
            {Opcode.CMSG_MAIL_MARK_AS_READ,                        0x027F}, //
            {Opcode.CMSG_MAIL_RETURN_TO_SENDER,                    0x1C8A}, //
            {Opcode.CMSG_MAIL_TAKE_ITEM,                           0x06F6}, //
            {Opcode.CMSG_MAIL_TAKE_MONEY,                          0x0676}, //
            {Opcode.CMSG_MESSAGECHAT_AFK,                          0x0422},
            {Opcode.CMSG_MESSAGECHAT_CHANNEL,                      0x0904},
            {Opcode.CMSG_MESSAGECHAT_DND,                          0x1E21},
            {Opcode.CMSG_MESSAGECHAT_EMOTE,                        0x0520},
            {Opcode.CMSG_MESSAGECHAT_GUILD,                        0x070B},
            {Opcode.CMSG_MESSAGECHAT_OFFICER,                      0x1F69},
            {Opcode.CMSG_MESSAGECHAT_PARTY,                        0x0642}, //-+
            {Opcode.CMSG_MESSAGECHAT_RAID,                         0x054B},
            {Opcode.CMSG_MESSAGECHAT_RAID_WARNING,                 0x0423},
            {Opcode.CMSG_MESSAGECHAT_SAY,                          0x0C41},
            {Opcode.CMSG_MESSAGECHAT_WHISPER,                      0x0D60},
            {Opcode.CMSG_MESSAGECHAT_YELL,                         0x0C43},
            {Opcode.CMSG_MINIMAP_PING,                             0x1A93},
            //{Opcode.CMSG_MOUNTSPECIAL_ANIM,                        0x1F32}, // dublicate
            {Opcode.CMSG_NAME_QUERY,                               0x0DB3},
            {Opcode.CMSG_NPC_TEXT_QUERY,                           0x12FA},
            {Opcode.CMSG_OBJECT_UPDATE_FAILED,                     0x0882},
            //{Opcode.CMSG_OBJECT_UPDATE_RESCUED,                    0x042E},
            {Opcode.CMSG_PET_NAME_QUERY,                           0x16A3},
            {Opcode.CMSG_PING,                                     0x1070},
            {Opcode.CMSG_PLAYED_TIME,                              0x173A},
            {Opcode.CMSG_PLAYER_LOGIN,                             0x17D3},
            {Opcode.CMSG_QUERY_GUILD_RECIPES,                      0x1DBC},
            {Opcode.CMSG_QUERY_GUILD_REWARDS,                      0x171B}, //
            {Opcode.CMSG_QUERY_GUILD_XP,                           0x1D37}, //
            {Opcode.CMSG_QUERY_INSPECT_ACHIEVEMENTS,               0x047E},
            {Opcode.CMSG_QUERY_TIME,                               0x03FD},
            {Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST,                  0x0356}, //
            {Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD,                 0x075F}, //
            {Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST,                0x115E}, //
            {Opcode.CMSG_QUESTGIVER_HELLO,                         0x1056}, //
            {Opcode.CMSG_QUESTGIVER_QUERY_QUEST,                   0x0474}, //
            {Opcode.CMSG_QUESTGIVER_REQUEST_REWARD,                0x107E}, //
            {Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY,         0x0275}, //
            {Opcode.CMSG_QUESTGIVER_STATUS_QUERY,                  0x05FD}, //
            {Opcode.CMSG_QUESTLOG_REMOVE_QUEST,                    0x0655}, //
            {Opcode.CMSG_QUEST_POI_QUERY,                          0x1DA8},
            {Opcode.CMSG_QUEST_QUERY,                              0x1F52}, //
            {Opcode.CMSG_RAID_READY_CHECK,                         0x0D88},
            {Opcode.CMSG_RAID_READY_CHECK_CONFIRM,                 0x13D9},
            {Opcode.CMSG_RANDOM_ROLL,                              0x1891},
            {Opcode.CMSG_RANDOMIZE_CHAR_NAME,                      0x1DB9},
            {Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES,             0x13CB},
            {Opcode.CMSG_REALM_NAME_QUERY,                         0x1899}, //
            //{Opcode.CMSG_REALM_QUERY,                              0x1899},
            {Opcode.CMSG_REALM_SPLIT,                              0x1282},
            {Opcode.CMSG_RECLAIM_CORPSE,                           0x065C},
            //{Opcode.CMSG_REDIRECTION_AUTH_PROOF,                   0x1A5B},
            {Opcode.CMSG_REFORGE_ITEM,                             0x1632}, //
            {Opcode.CMSG_REORDER_CHARACTERS,                       0x1892},
            {Opcode.CMSG_REPAIR_ITEM,                              0x0577},
            {Opcode.CMSG_REPOP_REQUEST,                            0x04FC},
            {Opcode.CMSG_REQUEST_HOTFIX,                           0x16C2},
            //{Opcode.CMSG_REQUEST_RAID_INFO,                        0x1980},
            {Opcode.CMSG_RESET_INSTANCES,                          0x169B},
            {Opcode.CMSG_RETURN_TO_GRAVEYARD,                      0x0257},
            {Opcode.CMSG_SELL_ITEM,                                0x115F},
            {Opcode.CMSG_SEND_MAIL,                                0x01A9}, //
            {Opcode.CMSG_SET_ACTIONBAR_TOGGLES,                    0x03F5},
            {Opcode.CMSG_SET_ACTION_BUTTON,                        0x1393},
            //{Opcode.CMSG_SET_DUNGEON_DIFFICULTY,                   0x1898},
            {Opcode.CMSG_SET_PRIMARY_TALENT_TREE,                  0x04AA},
            {Opcode.CMSG_SET_SELECTION,                            0x10D5},
            {Opcode.CMSG_SET_TITLE,                                0x13E2}, //
            {Opcode.CMSG_SET_TRADE_GOLD,                           0x0C93}, //
            {Opcode.CMSG_SHOWING_CLOAK,                            0x1276},
            {Opcode.CMSG_SHOWING_HELM,                             0x117F},
            {Opcode.CMSG_SPLIT_ITEM,                               0x140A}, //
            {Opcode.CMSG_SWAP_ITEM,                                0x150A},
            {Opcode.CMSG_TAXINODE_STATUS_QUERY,                    0x01FF}, //
            {Opcode.CMSG_TAXIQUERYAVAILABLENODES,                  0x0656}, //
            {Opcode.CMSG_TEXT_EMOTE,                               0x037D}, //
            {Opcode.CMSG_TIME_SYNC_RESP,                           0x0413},
            {Opcode.CMSG_TRAINER_BUY_SPELL,                        0x0274},
            {Opcode.CMSG_TRAINER_LIST,                             0x075E},
            {Opcode.CMSG_TRANSMOGRIFY_ITEMS,                       0x13F8}, //
            {Opcode.CMSG_TUTORIAL_FLAG,                            0x07A4},
            {Opcode.CMSG_UNLEARN_SKILL,                            0x025D},
            {Opcode.CMSG_USE_ITEM,                                 0x15E3},
            {Opcode.CMSG_VIOLENCE_LEVEL,                           0x05A0},
            {Opcode.CMSG_WARDEN_DATA,                              0x1681},
            {Opcode.CMSG_WHO,                                      0x13C1},
            {Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE,              0x1CA3},
            {Opcode.MSG_MOVE_FALL_LAND,                            0x055B},
            {Opcode.MSG_MOVE_HEARTBEAT,                            0x017B},
            {Opcode.MSG_MOVE_JUMP,                                 0x0438},
            {Opcode.MSG_MOVE_SET_FACING,                           0x005A},
            {Opcode.MSG_MOVE_SET_PITCH,                            0x047A},
            {Opcode.MSG_MOVE_SET_RUN_MODE,                         0x0878},
            {Opcode.MSG_MOVE_SET_WALK_MODE,                        0x0138},
            {Opcode.MSG_MOVE_START_ASCEND,                         0x0430},
            {Opcode.MSG_MOVE_START_BACKWARD,                       0x0459},
            {Opcode.MSG_MOVE_START_DESCEND,                        0x0132},
            {Opcode.MSG_MOVE_START_FORWARD,                        0x041B},
            {Opcode.MSG_MOVE_START_PITCH_DOWN,                     0x093B},
            {Opcode.MSG_MOVE_START_PITCH_UP,                       0x0079},
            {Opcode.MSG_MOVE_START_STRAFE_LEFT,                    0x0873},
            {Opcode.MSG_MOVE_START_STRAFE_RIGHT,                   0x0C12},
            {Opcode.MSG_MOVE_START_SWIM,                           0x0871},
            {Opcode.MSG_MOVE_START_TURN_LEFT,                      0x011B},
            {Opcode.MSG_MOVE_START_TURN_RIGHT,                     0x0411},
            {Opcode.MSG_MOVE_STOP,                                 0x0570},
            {Opcode.MSG_MOVE_STOP_ASCEND,                          0x0012},
            {Opcode.MSG_MOVE_STOP_PITCH,                           0x0071},
            {Opcode.MSG_MOVE_STOP_STRAFE,                          0x0171},
            {Opcode.MSG_MOVE_STOP_SWIM,                            0x0578},
            {Opcode.MSG_MOVE_STOP_TURN,                            0x0530},
            //{Opcode.MSG_MOVE_TELEPORT,                             0x00D5},
            {Opcode.MSG_MOVE_TELEPORT_ACK,                         0x0978},
            {Opcode.MSG_MOVE_WORLDPORT_ACK,                        0x18BB},
            {Opcode.MSG_VERIFY_CONNECTIVITY,                       0x4F57},
            {Opcode.SMSG_ACCOUNT_DATA_TIMES,                       0x0F40},
            {Opcode.SMSG_ACTION_BUTTONS,                           0x1768},
            {Opcode.SMSG_ADDON_INFO,                               0x10E2},
            //{Opcode.SMSG_ALL_ACHIEVEMENT_DATA,                     0x072B},
            {Opcode.SMSG_ATTACKERSTATEUPDATE,                      0x0540},
            {Opcode.SMSG_ATTACKSTART,                              0x0403},
            {Opcode.SMSG_ATTACKSTOP,                               0x1448},
            {Opcode.SMSG_AUCTION_HELLO,                            0x04E9},
            {Opcode.SMSG_AUCTION_COMMAND_RESULT,                   0x12FB},
            {Opcode.SMSG_AUCTION_LIST_PENDING_SALES,               0x11EB},
            {Opcode.SMSG_AUCTION_LIST_RESULT,                      0x0504},
            {Opcode.SMSG_AUCTION_OWNER_LIST_RESULT,                0x048F},
            //{Opcode.SMSG_AURACASTLOG,                              0x0919},
            {Opcode.SMSG_AURA_UPDATE,                              0x1B8D},
            {Opcode.SMSG_AUTH_CHALLENGE,                           0x14B8},
            {Opcode.SMSG_AUTH_RESPONSE,                            0x15A0},
            {Opcode.SMSG_BINDER_CONFIRM,                           0x0F22},
            {Opcode.SMSG_BINDPOINTUPDATE,                          0x11E2},
            {Opcode.SMSG_BUY_ITEM,                                 0x0763},
            {Opcode.SMSG_CALENDAR_SEND_CALENDAR,                   0x15B1},
            {Opcode.SMSG_CALENDAR_SEND_EVENT,                      0x05E0},
            //{Opcode.SMSG_CANCEL_COMBAT,                            0x1E48},
            //{Opcode.SMSG_CAST_FAILED,                              0x0560}, //???
            {Opcode.SMSG_CHANNEL_LIST,                             0x087B},
            {Opcode.SMSG_CHANNEL_NOTIFY,                           0x11C5},
            {Opcode.SMSG_CHAR_CREATE,                              0x1469},
            {Opcode.SMSG_CHAR_DELETE,                              0x1529},
            {Opcode.SMSG_CHAR_ENUM,                                0x040A},
            {Opcode.SMSG_CLIENTCACHE_VERSION,                      0x1E41},
            {Opcode.SMSG_CLIENT_CONTROL_UPDATE,                    0x01EA},
            //{Opcode.SMSG_CONTACT_LIST,                             0x15CF},
            {Opcode.SMSG_COOLDOWN_EVENT,                           0x1C5B},
            {Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE,       0x1C73},
            {Opcode.SMSG_CORPSE_NOT_IN_INSTANCE,                   0x1C3B},
            {Opcode.SMSG_CORPSE_QUERY,                             0x1F32},
            {Opcode.SMSG_CORPSE_RECLAIM_DELAY,                     0x1E73},
            {Opcode.SMSG_CREATURE_QUERY_RESPONSE,                  0x00E0},
            {Opcode.SMSG_DB_REPLY,                                 0x1F01},
            {Opcode.SMSG_DEATH_RELEASE_LOC,                        0x1672},
            {Opcode.SMSG_DESTROY_OBJECT,                           0x1D69},
            {Opcode.SMSG_FEATURE_SYSTEM_STATUS,                    0x1560},
            {Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE,                0x066A},
            {Opcode.SMSG_GAME_STORE_AUTH_BUY_FAILED,               0x0C40},
            {Opcode.SMSG_GAME_STORE_BUY_RESULT,                    0x12AB},
            {Opcode.SMSG_GAME_STORE_INGAME_BUY_FAILED,             0x145A},
            {Opcode.SMSG_GAME_STORE_LIST,                          0x1C29},
            {Opcode.SMSG_GAMESPEED_SET,                            0x1D73},
            {Opcode.SMSG_GAMETIME_SET,                             0x037E},
            {Opcode.SMSG_GAMETIME_UPDATE,                          0x00A3},
            {Opcode.SMSG_GOSSIP_COMPLETE,                          0x0F79},
            {Opcode.SMSG_GOSSIP_MESSAGE,                           0x0E52},
            {Opcode.SMSG_GOSSIP_POI,                               0x058F},
            {Opcode.SMSG_GROUP_DECLINE,                            0x108F},
            {Opcode.SMSG_GROUP_DESTROYED,                          0x1564},
            {Opcode.SMSG_GROUP_INVITE,                             0x1472},
            {Opcode.SMSG_GROUP_LIST,                               0x1E61},
            {Opcode.SMSG_GROUP_SET_LEADER,                         0x15A2},
            {Opcode.SMSG_GROUP_SET_ROLE,                           0x0890},
            {Opcode.SMSG_GUILD_BANK_LIST,                          0x1B7B},
            //{Opcode.SMSG_GUILD_BANK_MONEY_WITHDRAWN,               0x11EB}, //dubl
            //{Opcode.SMSG_GUILD_COMMAND_RESULT,                     0x1A13}, //dubl
            //{Opcode.SMSG_GUILD_INVITE,                             0x1A13}, //dubl
            {Opcode.SMSG_GUILD_NEWS_UPDATE,                        0x187B},
            {Opcode.SMSG_GUILD_PERMISSIONS_QUERY_RESULTS,          0x1A32},
            {Opcode.SMSG_GUILD_QUERY_RESPONSE,                     0x1953},
            {Opcode.SMSG_GUILD_RANK,                               0x1271},
            {Opcode.SMSG_GUILD_REWARDS_LIST,                       0x1010},
            {Opcode.SMSG_GUILD_ROSTER,                             0x1231},
            //{Opcode.SMSG_GUILD_XP,                                 0x1A51}, //dubl
            {Opcode.SMSG_GUILD_XP_GAIN,                            0x1A11},
            {Opcode.SMSG_INITIALIZE_FACTIONS,                      0x11E1},
            {Opcode.SMSG_INITIAL_SPELLS,                           0x1B05},
            {Opcode.SMSG_INIT_CURRENCY,                            0x1E3A},
            {Opcode.SMSG_INIT_WORLD_STATES,                        0x0F03},
            {Opcode.SMSG_INVENTORY_CHANGE_FAILURE,                 0x0F49},
            {Opcode.SMSG_ITEM_PUSH_RESULT,                         0x04A1},
            {Opcode.SMSG_LEARNED_SPELL,                            0x0C99},
            {Opcode.SMSG_LEVELUP_INFO,                             0x0E6A},
            {Opcode.SMSG_LIST_INVENTORY,                           0x0D2A},
            {Opcode.SMSG_LOGIN_SETTIMESPEED,                       0x0F4A},
            {Opcode.SMSG_LOGIN_VERIFY_WORLD,                       0x0603},
            {Opcode.SMSG_LOGOUT_CANCEL_ACK,                        0x0E0A},
            {Opcode.SMSG_LOGOUT_COMPLETE,                          0x0429},
            {Opcode.SMSG_LOGOUT_RESPONSE,                          0x0D2B},
            {Opcode.SMSG_LOG_XPGAIN,                               0x1613},
            {Opcode.SMSG_LOOT_CLEAR_MONEY,                         0x0C89},
            {Opcode.SMSG_LOOT_MONEY_NOTIFY,                        0x1F49},
            {Opcode.SMSG_LOOT_RELEASE_RESPONSE,                    0x14A2},
            {Opcode.SMSG_LOOT_REMOVED,                             0x0D00},
            //{Opcode.SMSG_LOOT_RESPONSE,                            0x1F41}, //???
            //{Opcode.SMSG_MESSAGECHAT,                              0x0E60} ,//???
            {Opcode.SMSG_MINIMAP_PING,                             0x1501},
            {Opcode.SMSG_MONEY_NOTIFY,                             0x05E2},
            //{Opcode.SMSG_MONSTER_MOVE,                             0x12D8}, //???
            {Opcode.SMSG_MOTD,                                     0x0E20},
            {Opcode.SMSG_MOVE_SET_ACTIVE_MOVER,                    0x129A},
            {Opcode.SMSG_MOVE_SET_CAN_FLY,                         0x01F4},
            {Opcode.SMSG_MOVE_SET_FLIGHT_SPEED,                    0x02DC},
            {Opcode.SMSG_MOVE_SET_RUN_SPEED,                       0x1B9B},
            {Opcode.SMSG_MOVE_SET_SWIM_SPEED,                      0x01D4},
            {Opcode.SMSG_MOVE_SET_WALK_SPEED,                      0x00A0},
            {Opcode.SMSG_MOVE_UNSET_CAN_FLY,                       0x1D81},
            {Opcode.SMSG_MULTIPLE_PACKETS_2,                       0x05B1}, //???
            //{Opcode.SMSG_NAME_QUERY_RESPONSE,                      0x1E5B}, //???
            {Opcode.SMSG_NEW_WORLD,                                0x05AB},
            {Opcode.SMSG_NPC_TEXT_UPDATE,                          0x10E0},
            {Opcode.SMSG_PARTY_COMMAND_RESULT,                     0x1787},
            {Opcode.SMSG_PERIODICAURALOG,                          0x051B},
            {Opcode.SMSG_PET_NAME_QUERY_RESPONSE,                  0x1F08},
            {Opcode.SMSG_PLAYED_TIME,                              0x1C69},
            {Opcode.SMSG_PLAYER_MOVE,                              0x1CB2},
            {Opcode.SMSG_PLAYERBOUND,                              0x00E8},
            //{Opcode.SMSG_PONG,                                     0x15B1}, //dubl
            {Opcode.SMSG_POWER_UPDATE,                             0x1441},
            //{Opcode.SMSG_PLAYERBOUND,                              0x00E8}, //dubl
            {Opcode.SMSG_QUERY_TIME_RESPONSE,                      0x0C30},
            {Opcode.SMSG_QUEST_QUERY_RESPONSE,                     0x0F13},
            {Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE,                0x0F77},
            {Opcode.SMSG_QUESTGIVER_QUEST_DETAILS,                 0x0966},
            //{Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE,               0x0F79}, //dubl
            //{Opcode.SMSG_RAID_INSTANCE_INFO,                       0x0C21},
            {Opcode.SMSG_RAID_READY_CHECK,                         0x072A},
            {Opcode.SMSG_RAID_READY_CHECK_CONFIRM,                 0x1641},
            {Opcode.SMSG_RANDOM_ROLL,                              0x0529},
            {Opcode.SMSG_RANDOMIZE_CHAR_NAME,                      0x074B},
            {Opcode.SMSG_REALM_NAME_QUERY_RESPONSE,                0x1652},
            //{Opcode.SMSG_REALM_SPLIT,                              0x145A}, //dubl
            {Opcode.SMSG_REMOVED_SPELL,                            0x05E3},
            {Opcode.SMSG_SERVER_MESSAGE,                           0x026E},
            {Opcode.SMSG_SET_PROFICIENCY,                          0x1E3B},
            {Opcode.SMSG_SERVER_TIMEZONE,                          0x0C2B},
            //{Opcode.SMSG_SERVERTIME,                               0x047E},
            {Opcode.SMSG_SHOW_BANK,                                0x060B},
            {Opcode.SMSG_SPELLHEALLOG,                             0x1BBF},
            {Opcode.SMSG_SPELLLOGEXECUTE,                          0x19B4},
            {Opcode.SMSG_SPELLNONMELEEDAMAGELOG,                   0x0172},
            {Opcode.SMSG_SPELL_CATEGORY_COOLDOWN,                  0x053B},
            //{Opcode.SMSG_SPELL_COOLDOWN,                           0x1B14}, //??? not work
            {Opcode.SMSG_SPELL_FAILED_OTHER,                       0x1E7A},
            {Opcode.SMSG_SPELL_FAILURE,                            0x0E03},
            //{Opcode.SMSG_SPELL_GO,                                 0x0851}, //???
            //{Opcode.SMSG_SPELL_START,                              0x0130}, //???
            {Opcode.SMSG_SPLINE_MOVE_SET_PITCH_RATE,               0x0D93}, //*
            {Opcode.SMSG_TALENTS_INFO,                             0x0C68},
            {Opcode.SMSG_TEXT_EMOTE,                               0x0522},
            {Opcode.SMSG_TIME_SYNC_REQ,                            0x12F1},
            {Opcode.SMSG_TRADE_STATUS,                             0x0609},
            {Opcode.SMSG_TRAINER_BUY_SUCCEEDED,                    0x13B2},
            {Opcode.SMSG_TRAINER_LIST,                             0x1509},
            {Opcode.SMSG_TRIGGER_CINEMATIC,                        0x04CC},
            {Opcode.SMSG_TRANSFER_PENDING,                         0x0440},
            {Opcode.SMSG_TUTORIAL_FLAGS,                           0x10A7},
            //{Opcode.SMSG_UPDATE_OBJECT,                            0x1725}, //last
            {Opcode.SMSG_WARDEN_DATA,                              0x14EB},
            {Opcode.SMSG_WHO,                                      0x0460},
            //{Opcode.SMSG_WORLD_SERVER_INFO,                        0x1D01}, //bad opcode
            {Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE,              0x0C22},
            {Opcode.SMSG_UNK_0130,                                 0x0130},
            {Opcode.SMSG_UNK_022F,                                 0x022F},
            {Opcode.SMSG_UNK_04AB,                                 0x04AB},
            {Opcode.SMSG_UNK_0851,                                 0x0851},
            {Opcode.SMSG_UNK_10E3,                                 0x10E3},
            {Opcode.SMSG_UNK_12D8,                                 0x12D8},
            {Opcode.SMSG_UNK_12F9,                                 0x12F9},
            {Opcode.SMSG_UNK_1609,                                 0x1609},
            {Opcode.SMSG_UNK_1725,                                 0x1725},
            {Opcode.SMSG_UNK_1D13,                                 0x1D13},
        };
    }
}
