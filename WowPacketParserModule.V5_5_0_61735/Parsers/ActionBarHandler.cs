using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            for (int i = 0; i < 180; ++i)
            {
                var packedData = packet.ReadUInt64();

                if (packedData == 0)
                    continue;

                var actionVal = packedData & 0xFFFFFFFFFFFFFFu;
                var type = (byte)((packedData >> 56) & 0xFF);

                packet.AddValue("Action", actionVal, i);
                packet.AddValue("Type", type, i);

                if (type != 0)
                    continue;

                if (CoreParsers.SessionHandler.LoginGuid != null)
                {
                    WoWObject character;
                    if (Storage.Objects.TryGetValue(CoreParsers.SessionHandler.LoginGuid, out character))
                    {
                        Player player = character as Player;
                        if (player != null && player.FirstLogin)
                        {
                            var action = new PlayerCreateInfoAction
                            {
                                Button = (uint)i,
                                Action = (uint)actionVal,
                                Race = player.Race,
                                Class = player.Class,
                                Type = (ActionButtonType)type
                            };

                            Storage.StartActions.Add(action, packet.TimeSpan);
                        }
                    }
                }
            }

            packet.ReadByte("Reason");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("ActionBar");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleActionButton(Packet packet)
        {
            var data = packet.ReadUInt64();

            packet.AddValue("Type", (ActionButtonType)((data & 0xFF00000000000000) >> 56));
            packet.AddValue("ID", data & 0x00FFFFFFFFFFFFFF);
            packet.ReadByte("Button");
        }
    }
}
