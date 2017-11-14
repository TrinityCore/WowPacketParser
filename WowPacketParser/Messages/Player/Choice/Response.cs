using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player.Choice
{
    public unsafe struct Response
    {
        public int ResponseID;
        public string Answer;
        public string Description;
        public int ChoiceArtFileID;
        public ResponseReward? Reward; // Optional
        
        [Parser(Opcode.CMSG_CHOICE_RESPONSE)]
        public static void HandleChoiceResponse(Packet packet)
        {
            packet.ReadInt32("ChoiceID");
            packet.ReadInt32("ResponseID");
        }
    }
}
