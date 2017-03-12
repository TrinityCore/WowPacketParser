using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPlayScene
    {
        public CliPlaySceneData PlayData;
    }
}
