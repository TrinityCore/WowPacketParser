using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class SceneHandler
    {
        [Parser(Opcode.SMSG_PLAY_SCENE)]
        public static void HandlePlayScene(Packet packet)
        {
            var transportGuid = new byte[8];

            packet.ReadSingle("PosX");
            packet.ReadSingle("PosZ");
            packet.ReadSingle("PosY");

            bool hasSceneInstanceId = !packet.ReadBit();
            packet.ReadBit(); // fake

            foreach (byte idx in new byte[] { 5, 2, 6, 7, 0, 1, 3, 4 })
                transportGuid[idx] = packet.ReadBit();

            bool hasOrientation = !packet.ReadBit();
            bool hasSceneId = !packet.ReadBit();
            bool hasPlayBackFlags = !packet.ReadBit();
            bool hasSceneScriptPackageId = !packet.ReadBit();

            if (hasOrientation)
                packet.ReadSingle("PosO");

            foreach (byte idx in new byte[] { 6, 7, 0, 2, 3, 4, 1, 5 })
                packet.ReadXORByte(transportGuid, idx);

            uint sceneId = 0;

            if (hasSceneId)
                sceneId = packet.ReadUInt32("SceneId");

            SceneTemplate scene = new SceneTemplate
            {
                SceneID = sceneId
            };

            if (hasSceneInstanceId)
                packet.ReadUInt32("SceneInstanceID");

            if (hasPlayBackFlags)
                scene.Flags = packet.ReadUInt32("PlaybackFlags");

            if (hasSceneScriptPackageId)
                scene.ScriptPackageID = packet.ReadUInt32("SceneScriptPackageID");

            if (sceneId != 0) // SPELL_EFFECT_195 plays scenes by SceneScriptPackageID and sets SceneID = 0 (there are no Scenes which have SceneID = 0)
                Storage.Scenes.Add(scene, packet.TimeSpan);
        }
    }
}
