using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ActionBarHandler
    {
        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleActionButton(Packet packet)
        {
            var data = packet.ReadInt32();

            packet.AddValue("Type", (ActionButtonType)((data & 0xFF000000) >> 24));
            packet.AddValue("ID", data & 0x00FFFFFF);
            packet.ReadByte("Button");
        }

        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.CMSG_SET_ACTION_BAR_TOGGLES, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("ActionBar");
        }
    }
}
