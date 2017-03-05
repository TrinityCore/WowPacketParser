using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SceneHandler
    {
        [Parser(Opcode.SMSG_CANCEL_SCENE)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_COMPLETE)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_CANCELED)]
        public static void HandleMiscScene(Packet packet)
        {
            packet.Translator.ReadUInt32("SceneInstanceID");
        }

        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandlePlayScene(Packet packet)
        {
            var sceneId = packet.Translator.ReadInt32("SceneID");
            SceneTemplate scene = new SceneTemplate
            {
                SceneID = (uint)sceneId
            };

            scene.Flags = (uint)packet.Translator.ReadInt32("PlaybackFlags");
            packet.Translator.ReadInt32("SceneInstanceID");
            scene.ScriptPackageID = (uint)packet.Translator.ReadInt32("SceneScriptPackageID");
            packet.Translator.ReadPackedGuid128("TransportGUID");
            packet.Translator.ReadVector3("Pos");
            packet.Translator.ReadSingle("Facing");

            Storage.Scenes.Add(scene, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_INITIAL_UPDATE)]
        public static void HandleSceneObjectPetBattleInitialUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleFullUpdate(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FIRST_ROUND)]
        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_ROUND_RESULT)]
        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_REPLACEMENTS_MADE)]
        public static void HandleSceneObjectPetBattleRound(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleRoundResult(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINAL_ROUND)]
        public static void HandleSceneObjectPetBattleFinalRound(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SceneObjectGUID");
            BattlePetHandler.ReadPetBattleFinalRound(packet, "MsgData");
        }

        [Parser(Opcode.SMSG_SCENE_OBJECT_PET_BATTLE_FINISHED)]
        public static void HandleSceneObjectPetBattleFinished(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("SceneObjectGUID");
        }

        [Parser(Opcode.CMSG_QUERY_SCENARIO_POI)]
        public static void HandleQueryScenarioPOI(Packet packet)
        {
            var missingScenarioPOITreeCount = packet.Translator.ReadInt32("MissingScenarioPOITreeCount");
            for (var i = 0; i < missingScenarioPOITreeCount; i++)
                packet.Translator.ReadInt32("MissingScenarioPOITreeIDs", i);
        }

        [Parser(Opcode.SMSG_SCENARIO_POIS)]
        public static void HandleScenarioPOIs(Packet packet)
        {
            var scenarioPOIDataCount = packet.Translator.ReadInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                packet.Translator.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.Translator.ReadInt32("ScenarioBlobDataCount");
                for (int j = 0; j < scenarioBlobDataCount; j++)
                {
                    packet.Translator.ReadInt32("BlobID", i, j);
                    packet.Translator.ReadInt32("MapID", i, j);
                    packet.Translator.ReadInt32("WorldMapAreaID", i, j);
                    packet.Translator.ReadInt32("Floor", i, j);
                    packet.Translator.ReadInt32("Priority", i, j);
                    packet.Translator.ReadInt32("Flags", i, j);
                    packet.Translator.ReadInt32("WorldEffectID", i, j);
                    packet.Translator.ReadInt32("PlayerConditionID", i, j);

                    var scenarioPOIPointDataCount = packet.Translator.ReadInt32("ScenarioPOIPointDataCount", i, j);
                    for (int k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        packet.Translator.ReadInt32("X", i, j, k);
                        packet.Translator.ReadInt32("Y", i, j, k);
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
            packet.Translator.ReadInt32("ScenarioID");
            packet.Translator.ReadInt32("CurrentStep");
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadInt32("WaveCurrent");
            packet.Translator.ReadInt32("WaveMax");
            packet.Translator.ReadInt32("TimerDuration");

            var int36 = packet.Translator.ReadInt32("CriteriaProgressCount");
            var int20 = packet.Translator.ReadInt32("BonusObjectiveDataCount");

            for (int i = 0; i < int36; i++)
                AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (int i = 0; i < int20; i++)
            {
                packet.Translator.ReadInt32("BonusObjectiveID", i);

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("ObjectiveComplete", i);
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("ScenarioComplete");
        }

        [Parser(Opcode.SMSG_SCENARIO_COMPLETED)]
        public static void HandleScenarioCompleted(Packet packet)
        {
            packet.Translator.ReadInt32("ScenarioID");
        }

        [Parser(Opcode.CMSG_SCENE_TRIGGER_EVENT)]
        public static void HandleSceneTriggerEvent(Packet packet)
        {
            var len = packet.Translator.ReadBits(6);
            packet.Translator.ReadUInt32("SceneInstanceID");
            packet.Translator.ReadWoWString("Event", len);
        }
    }
}
