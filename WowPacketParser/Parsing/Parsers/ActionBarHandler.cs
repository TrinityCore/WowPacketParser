using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Parsing.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_ACTION_BUTTONS)]
        public static void HandleInitialButtons(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                // State = 0: Looks to be sent when initial action buttons get sent, however on Trinity we use 1 since 0 had some difficulties
                // State = 1: Used in any SMSG_ACTION_BUTTONS packet with button data on Trinity. Only used after spec swaps on retail.
                // State = 2: Clears the action bars client sided. This is sent during spec swap before unlearning and before sending the new buttons
                if (packet.ReadByte("Packet Type") == 2)
                    return;
            }

            var buttonCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 144 : 132;

            packet.StoreBeginList("Buttons");
            for (var i = 0; i < buttonCount; i++)
            {
                var packed = packet.ReadInt32();

                if (packed == 0)
                    continue;

                var actionId = (uint)(packed & 0x00FFFFFF);
                packet.Store("Action", actionId, i);

                var actionType = (ActionButtonType)((packed & 0xFF000000) >> 24);
                packet.Store("Type", actionType, i);
            }
            packet.StoreEndList();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadByte("Packet Type");
        }


        [Parser(Opcode.CMSG_SET_ACTIONBAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("Action Bar");
        }
    }
}
