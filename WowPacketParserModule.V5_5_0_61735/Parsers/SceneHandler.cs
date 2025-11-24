using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class SceneHandler
    {
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
            packet.ReadVector4("Location");
            packet.ReadInt32("MovieID");
            scene.Encrypted = packet.ReadBit("Encrypted");

            if (sceneId != 0) // SPELL_EFFECT_195 plays scenes by SceneScriptPackageID and sets SceneID = 0 (there are no Scenes which have SceneID = 0)
                Storage.Scenes.Add(scene, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_CANCEL_SCENE)]
        public static void HandleMiscScene(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
        }
    }
}
