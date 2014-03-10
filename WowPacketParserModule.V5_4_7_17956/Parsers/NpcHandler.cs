using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_GOSSIP_HELLO, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcHello(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 4, 5, 1, 7, 2, 0);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");

            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var boxTextLength = packet.ReadBits(8);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);

            packet.ReadWoWString("Box Text", boxTextLength);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("NPC Guid", guid);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_LIST_INVENTORY, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcListInventory(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.ParseBitStream(guid, 0, 5, 6, 7, 1, 3, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleTrainerBuySpell(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
            packet.ReadUInt32("Trainer Id");

            var guid = packet.StartBitStream(6, 2, 0, 7, 5, 3, 1, 4);
            packet.ParseBitStream(guid, 6, 0, 5, 1, 7, 4, 2, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guid = new byte[8];
            uint[] QuestTitleLength;
            uint[] OptionsMessageLength;
            uint[] BoxMessageLength;

            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var QuestsCount = packet.ReadBits("Quests Count", 19);

            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var OptionsCount = packet.ReadBits("Gossip Options Count", 20);

            OptionsMessageLength = new uint[OptionsCount];
            BoxMessageLength = new uint[OptionsCount];
            for (var i = 0; i < OptionsCount; ++i)
            {
                BoxMessageLength[i] = packet.ReadBits(12);
                OptionsMessageLength[i] = packet.ReadBits(12);
            }

            QuestTitleLength = new uint[QuestsCount];
            for (var i = 0; i < QuestsCount; ++i)
            {
                packet.ReadBit("Quest Icon Change", i);
                QuestTitleLength[i] = packet.ReadBits(9);
            }

            for (var i = 0; i < QuestsCount; i++)
            {
                packet.ReadEnum<QuestFlags2>("Quest Special Flags", TypeCode.UInt32, i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest Id", i);
                packet.ReadUInt32("Quest Level", i);
                packet.ReadUInt32("Quest Icon", i);
                packet.ReadEnum<QuestFlags>("Quest Flags", TypeCode.UInt32, i);
                packet.ReadWoWString("Quest Titles", QuestTitleLength[i], i);
            }

            packet.ReadXORByte(guid, 6);

            var ObjectGossip = new Gossip();
            ObjectGossip.GossipOptions = new List<GossipOption>((int)OptionsCount);
            for (var i = 0; i < OptionsCount; ++i)
            {
                var gossipMenuOption = new GossipOption
                {
                    RequiredMoney = packet.ReadUInt32("Message Box Required Money", i),
                    OptionText = packet.ReadWoWString("Gossip Option Text", OptionsMessageLength[i], i),
                    Index = packet.ReadUInt32("Gossip Option Index", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Gossip Option Icon", TypeCode.Byte, i),
                    BoxText = packet.ReadWoWString("Message Box Text", BoxMessageLength[i], i),
                    Box = packet.ReadBoolean("Message Box Coded", i), // True if it has a password.
                };

                ObjectGossip.GossipOptions.Add(gossipMenuOption);
            }

            packet.ReadXORByte(guid, 2);

            var GossipMenuTextId = packet.ReadUInt32("Gossip Menu Text Id");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);

            var GossipMenuId = packet.ReadUInt32("Gossip Menu Id");
            packet.ReadUInt32("Friendly Faction Id");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Object Guid", guid);

            var ObjectGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            ObjectGossip.ObjectEntry = ObjectGuid.GetEntry();
            ObjectGossip.ObjectType = ObjectGuid.GetObjectType();

            if (ObjectGuid.GetObjectType() == ObjectType.Unit)
                if (Storage.Objects.ContainsKey(ObjectGuid))
                    ((Unit)Storage.Objects[ObjectGuid].Item1).GossipId = GossipMenuId;

            Storage.Gossips.Add(Tuple.Create(GossipMenuId, GossipMenuTextId), ObjectGossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)GossipMenuId, ObjectGuid.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_SHOW_BANK, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_SHOW_BANK, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 6, 4, 3, 5, 0, 2);
            packet.ParseBitStream(guid, 6, 0, 7, 3, 5, 1, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }
    }
}
