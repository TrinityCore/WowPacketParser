using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct TicketCreate
    {
        public Vector3 Pos;
        public int MapID;
        public byte Flags;
        public bool NeedMoreHelp;
        public bool NeedResponse;
        public string Description;
        public Data ChatHistoryData;

        [Parser(Opcode.CMSG_GM_TICKET_CREATE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.ReadInt32<MapId>("Map ID");
            packet.ReadVector3("Position");
            packet.ReadCString("Text");
            packet.ReadUInt32("Need Response");
            packet.ReadBool("Need GM interaction");
            var count = packet.ReadInt32("Count");

            for (int i = 0; i < count; i++)
                packet.AddValue("Sent", (packet.Time - packet.ReadTime()).ToFormattedString(), i);

            if (count == 0)
                packet.ReadInt32("Unk Int32");
            else
            {
                var decompCount = packet.ReadInt32();
                var pkt = packet.Inflate(decompCount);
                pkt.ReadCString("String");
                pkt.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_CREATE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMTicketCreate602(Packet packet)
        {
            packet.ReadInt32<MapId>("Map");
            packet.ReadVector3("Pos");
            packet.ReadByte("Flags");

            var descriptionLength = packet.ReadBits("DescriptionLength", 11);
            packet.ResetBitReader();
            packet.ReadWoWString("Description", descriptionLength);

            packet.ReadBit("NeedResponse");
            packet.ReadBit("NeedMoreHelp");
            packet.ResetBitReader();

            var dataLength = packet.ReadInt32("DataLength");

            if (dataLength > 0)
            {
                var textCount = packet.ReadByte("TextCount");

                for (int i = 0; i < textCount /* 60 */; ++i)
                    packet.AddValue("Sent", (packet.Time - packet.ReadTime()).ToFormattedString(), i);

                var decompCount = packet.ReadInt32();
                var pkt = packet.Inflate(decompCount);
                pkt.ReadCString("Text");
                pkt.ClosePacket(false);
            }
        }
    }
}
