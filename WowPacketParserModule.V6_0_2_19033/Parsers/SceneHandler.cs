using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SceneHandler
    {
        [Parser(Opcode.SMSG_CANCEL_SCENE)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_COMPLETE)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_CANCELED)]
        public static void HandleMiscScene(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
        }

        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandlePlayScene(Packet packet)
        {
            packet.ReadInt32("SceneID");
            packet.ReadInt32("PlaybackFlags");
            packet.ReadInt32("SceneInstanceID");
            packet.ReadInt32("SceneScriptPackageID");
            packet.ReadPackedGuid128("TransportGUID");
            packet.ReadVector3("Pos");
            packet.ReadSingle("Facing");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_INITIAL_UPDATE)]
        public static void HandleSceneObjectPetBattleInitialUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleFullUpdate(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FIRST_ROUND)]
        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_ROUND_RESULT)]
        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_REPLACEMENTS_MADE)]
        public static void HandleSceneObjectPetBattleRound(Packet packet)
        {
            packet.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleRoundResult(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINAL_ROUND)]
        public static void HandleSceneObjectPetBattleFinalRound(Packet packet)
        {
            packet.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleFinalRound(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINISHED)]
        public static void HandleSceneObjectPetBattleFinished(Packet packet)
        {
            packet.ReadPackedGuid128("SceneObjectGUID");
        }

        [Parser(Opcode.CMSG_QUERY_SCENARIO_POI)]
        public static void HandleQueryScenarioPOI(Packet packet)
        {
            var missingScenarioPOITreeCount = packet.ReadInt32("MissingScenarioPOITreeCount");
            for (var i = 0; i < missingScenarioPOITreeCount; i++)
                packet.ReadInt32("MissingScenarioPOITreeIDs", i);
        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.ReadInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadInt32("ScenarioBlobDataCount");
                for (int j = 0; j < scenarioBlobDataCount; j++)
                {
                    packet.ReadInt32("BlobID", i, j);
                    packet.ReadInt32("MapID", i, j);
                    packet.ReadInt32("WorldMapAreaID", i, j);
                    packet.ReadInt32("Floor", i, j);
                    packet.ReadInt32("Priority", i, j);
                    packet.ReadInt32("Flags", i, j);
                    packet.ReadInt32("WorldEffectID", i, j);
                    packet.ReadInt32("PlayerConditionID", i, j);

                    var scenarioPOIPointDataCount = packet.ReadInt32("ScenarioPOIPointDataCount", i, j);
                    for (int k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        packet.ReadInt32("X", i, j, k);
                        packet.ReadInt32("Y", i, j, k);
                    }
                }
            }
        }

        [Parser(Opcode.SMSG_SCENARIO_BOOT)]
        public static void HandleScenarioZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_SCENARIO_PROGRESS_UPDATE)]
        public static void HandleScenarioProgressUpdate(Packet packet)
        {
            AchievementHandler.ReadCriteriaProgress(packet, "Progress");
        }

        [Parser(Opcode.SMSG_SCENARIO_STATE)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadInt32("DifficultyID");
            packet.ReadInt32("WaveCurrent");
            packet.ReadInt32("WaveMax");
            packet.ReadInt32("TimerDuration");

            var int36 = packet.ReadInt32("CriteriaProgressCount");
            var int20 = packet.ReadInt32("BonusObjectiveDataCount");

            for (int i = 0; i < int36; i++)
                AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (int i = 0; i < int20; i++)
            {
                packet.ReadInt32("BonusObjectiveID", i);

                packet.ResetBitReader();
                packet.ReadBit("ObjectiveComplete", i);
            }

            packet.ResetBitReader();

            packet.ReadBit("ScenarioComplete");
        }

        [Parser(Opcode.SMSG_SCENARIO_COMPLETED)]
        public static void HandleScenarioCompleted(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
        }

        [Parser(Opcode.CMSG_SCENE_TRIGGER_EVENT)]
        public static void HandleSceneTriggerEvent(Packet packet)
        {
            var len = packet.ReadBits(6);
            packet.ReadUInt32("SceneInstanceID");
            packet.ReadWoWString("Event", len);
        }
    }
}
