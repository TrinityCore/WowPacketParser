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
        public static void HandleMiscScene(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
        }

        [Parser(Opcode.CMSG_SCENE_PLAYBACK_COMPLETE)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_CANCELED)]
        public static void HandlScenePlayback(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_6_53840))
                packet.ReadInt32("TimePassed");
        }

        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandlePlayScene(Packet packet)
        {
            var sceneId = packet.ReadInt32("SceneID");
            SceneTemplate scene = new SceneTemplate
            {
                SceneID = (uint)sceneId
            };

            scene.Flags = (uint)packet.ReadInt32("PlaybackFlags");
            packet.ReadInt32("SceneInstanceID");
            scene.ScriptPackageID = (uint)packet.ReadInt32("SceneScriptPackageID");
            packet.ReadPackedGuid128("TransportGUID");
            packet.ReadVector3("Pos");
            packet.ReadSingle("Facing");

            if (sceneId != 0) // SPELL_EFFECT_195 plays scenes by SceneScriptPackageID and sets SceneID = 0 (there are no Scenes which have SceneID = 0)
                Storage.Scenes.Add(scene, packet.TimeSpan);
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
            var scenarioPOIDataCount = packet.ReadUInt32("ScenarioPOIDataCount");
            for (var i = 0; i < scenarioPOIDataCount; i++)
            {
                int citeriaTreeId = packet.ReadInt32("CriteriaTreeID");

                var scenarioBlobDataCount = packet.ReadUInt32("ScenarioBlobDataCount");
                for (uint j = 0; j < scenarioBlobDataCount; j++)
                {
                    ScenarioPOI scenarioPOI = new ScenarioPOI
                    {
                        CriteriaTreeID = citeriaTreeId
                    };

                    scenarioPOI.BlobIndex = packet.ReadInt32("BlobID", i, j);
                    scenarioPOI.Idx1 = j;
                    scenarioPOI.MapID = packet.ReadInt32<MapId>("MapID", i, j);
                    scenarioPOI.WorldMapAreaId = packet.ReadInt32("WorldMapAreaID", i, j);
                    scenarioPOI.Floor = packet.ReadInt32("Floor", i, j);
                    scenarioPOI.Priority = packet.ReadInt32("Priority", i, j);
                    scenarioPOI.Flags = packet.ReadInt32("Flags", i, j);
                    scenarioPOI.WorldEffectID = packet.ReadInt32("WorldEffectID", i, j);
                    scenarioPOI.PlayerConditionID = packet.ReadInt32("PlayerConditionID", i, j);

                    Storage.ScenarioPOIs.Add(scenarioPOI, packet.TimeSpan);

                    var scenarioPOIPointDataCount = packet.ReadUInt32("ScenarioPOIPointDataCount", i, j);
                    for (uint k = 0; k < scenarioPOIPointDataCount; k++)
                    {
                        ScenarioPOIPoint scenarioPOIPoint = new ScenarioPOIPoint
                        {
                            CriteriaTreeID = citeriaTreeId
                        };

                        scenarioPOIPoint.Idx1 = j;
                        scenarioPOIPoint.Idx2 = k;
                        scenarioPOIPoint.X = packet.ReadInt32("X", i, j, k);
                        scenarioPOIPoint.Y = packet.ReadInt32("Y", i, j, k);

                        Storage.ScenarioPOIPoints.Add(scenarioPOIPoint, packet.TimeSpan);
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
            packet.ReadInt32<DifficultyId>("DifficultyID");
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
