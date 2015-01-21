using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class SceneHandler
    {
        [Parser(Opcode.SMSG_CANCEL_SCENE)]
        public static void HandleCancelScene(Packet packet)
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
    }
}
