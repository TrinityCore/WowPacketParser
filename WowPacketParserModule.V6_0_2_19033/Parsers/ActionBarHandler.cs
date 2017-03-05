using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class ActionBarHandler
    {
        [Parser(Opcode.SMSG_UPDATE_ACTION_BUTTONS)]
        public static void HandleActionButtons(Packet packet)
        {
            const int buttonCount = 132;

            for (int i = 0; i < buttonCount; ++i)
            {
                PlayerCreateInfoAction action = new PlayerCreateInfoAction
                {
                    Button = (uint)i
                };

                action.Action = packet.ReadUInt32();
                uint type = packet.ReadUInt32();

                packet.AddValue("Action " + i, action.Action);
                packet.AddValue("Type " + i, type);

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
                            action.Race = player.Race;
                            action.Class = player.Class;
                            Storage.StartActions.Add(action, packet.TimeSpan);
                        }
                    }
                }
            }

            packet.ReadByte("Packet Type");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleSetActionButton(Packet packet)
        {
            uint action = packet.ReadUInt32();
            uint type = packet.ReadUInt32();

            packet.AddValue("Action ", action);
            packet.AddValue("Type ", type);
            packet.ReadByte("Slot Id");
        }
    }
}
