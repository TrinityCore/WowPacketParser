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
    }
}
