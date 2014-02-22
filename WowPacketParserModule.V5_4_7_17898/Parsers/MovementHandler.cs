using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class MovementHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            var pos = new Vector4();

            pos.O = packet.ReadSingle();
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            var pos = new Vector4();

            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.O = packet.ReadSingle();
            pos.X = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_BINDPOINTUPDATE)]
        public static void HandleBindPointUpdate(Packet packet)
        {
            var pos = new Vector3();

            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map");

            pos.X = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.Y = packet.ReadSingle();

            packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            packet.WriteLine("Position: {0}", pos);
        }

        [Parser(Opcode.SMSG_LOGIN_SETTIMESPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("Game Time");
            packet.ReadSingle("Game Speed");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

            var customLoadScreenSpell = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            if (hasTransport)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Transport Map ID");
                packet.ReadInt32("Transport Entry");
            }

            if (customLoadScreenSpell)
                packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");
        }
    }
}
