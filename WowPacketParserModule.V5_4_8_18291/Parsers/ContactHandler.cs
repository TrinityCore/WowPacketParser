using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class ContactHandler
    {
        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            packet.ReadInt32E<ContactListFlag>("List Flags");
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("GUID");
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Realm Id");
                var flag = packet.ReadInt32E<ContactEntryFlag>("Flags");
                packet.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                var status = packet.ReadByteE<ContactStatus>("Status");
                if (status == 0) // required any flag
                    continue;

                packet.ReadInt32<AreaId>("Area");
                packet.ReadInt32("Level");
                packet.ReadInt32E<Class>("Class");
            }

            // still needed?
            if (packet.CanRead())
                CoreParsers.WardenHandler.ReadCheatCheckDecryptionBlock(packet);
        }
    }
}
