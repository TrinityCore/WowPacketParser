using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class SceneHandler
    {
        [Parser(Opcode.SMSG_CANCEL_SCENE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleMiscScene(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
        }

        [Parser(Opcode.SMSG_PLAY_SCENE, ClientVersionBuild.V3_4_4_59817)]
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
            packet.ReadInt32("MovieID");

            scene.Encrypted = packet.ReadBit("Encrypted");

            if (sceneId != 0) // SPELL_EFFECT_195 plays scenes by SceneScriptPackageID and sets SceneID = 0 (there are no Scenes which have SceneID = 0)
                Storage.Scenes.Add(scene, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_SCENE_PLAYBACK_CANCELED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_SCENE_PLAYBACK_COMPLETE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlScenePlaybackCanceled(Packet packet)
        {
            packet.ReadUInt32("SceneInstanceID");
            packet.ReadUInt32("TimePassed");
        }

        [Parser(Opcode.CMSG_SCENE_TRIGGER_EVENT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSceneTriggerEvent(Packet packet)
        {
            var len = packet.ReadBits(6);
            packet.ReadUInt32("SceneInstanceID");
            packet.ReadWoWString("Event", len);
        }
    }
}
