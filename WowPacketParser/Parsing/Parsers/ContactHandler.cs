using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class ContactHandler
    {
        public static void ReadSingleContactBlock(Packet packet, bool onlineCheck)
        {
            var status = (ContactStatus)packet.ReadByte();
            Console.WriteLine("Status: " + status);

            if (onlineCheck && status != ContactStatus.Online)
                return;

            packet.ReadInt32("Area ID");
            packet.ReadInt32("Level");

            var clss = (Class)packet.ReadInt32();
            Console.WriteLine("Class: " + clss);
        }

        [Parser(Opcode.SMSG_CONTACT_LIST)]
        public static void HandleContactList(Packet packet)
        {
            var flags = (ContactListFlag)packet.ReadInt32();
            Console.WriteLine("List Flags: " + flags);

            var count = packet.ReadInt32();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid();
                Console.WriteLine("GUID: " + guid);

                var cflags = (ContactEntryFlag)packet.ReadInt32();
                Console.WriteLine("Flags: " + cflags);

                var note = packet.ReadCString();
                Console.WriteLine("Note: " + note);

                if (!cflags.HasFlag(ContactEntryFlag.Friend))
                    continue;

                ReadSingleContactBlock(packet, true);
            }

            if (!packet.IsRead())
                WardenHandler.ReadCheatCheckDecryptionBlock(packet);
        }

        [Parser(Opcode.SMSG_FRIEND_STATUS)]
        public static void HandleFriendStatus(Packet packet)
        {
            var result = (ContactResult)packet.ReadByte();
            Console.WriteLine("Result: " + result);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            switch (result)
            {
                case ContactResult.FriendAddedOnline:
                case ContactResult.FriendAddedOffline:
                case ContactResult.Online:
                {
                    if (result != ContactResult.Online)
                    {
                        var note = packet.ReadCString();
                        Console.WriteLine("Note: " + note);
                    }

                    ReadSingleContactBlock(packet, false);
                    break;
                }
            }
        }
    }
}
