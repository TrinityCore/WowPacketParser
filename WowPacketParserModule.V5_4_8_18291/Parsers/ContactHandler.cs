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
            packet.Translator.ReadInt32E<ContactListFlag>("List Flags");
            var count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadGuid("GUID");
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("Realm Id");
                var flag = packet.Translator.ReadInt32E<ContactEntryFlag>("Flags");
                packet.Translator.ReadCString("Note");

                if (!flag.HasAnyFlag(ContactEntryFlag.Friend))
                    continue;

                var status = packet.Translator.ReadByteE<ContactStatus>("Status");
                if (status == 0) // required any flag
                    continue;

                packet.Translator.ReadInt32<AreaId>("Area");
                packet.Translator.ReadInt32("Level");
                packet.Translator.ReadInt32E<Class>("Class");
            }

            // still needed?
            if (packet.CanRead())
                CoreParsers.WardenHandler.ReadCheatCheckDecryptionBlock(packet);
        }
    }
}
