using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class ContactHandler
    {
        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadEnum<ContactListFlag>("List Flags", TypeCode.Int32);
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");
                packet.ReadInt32("Unk int1");
                packet.ReadInt32("Unk int2");
                var flag = packet.ReadEnum<ContactEntryFlag>("Flags", TypeCode.Int32);
                packet.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                var status = packet.ReadEnum<ContactStatus>("Status", TypeCode.Byte);
                if (status == 0) // required any flag
                    continue;

                packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area");
                packet.ReadInt32("Level");
                packet.ReadEnum<Class>("Class", TypeCode.Int32);
            }

            // still needed?
            if (packet.CanRead())
                CoreParsers.WardenHandler.ReadCheatCheckDecryptionBlock(ref packet);
        }
    }
}
