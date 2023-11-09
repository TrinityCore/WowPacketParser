using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CalendarHandler
    {
        public static void ReadCalendarSendCalendarInviteInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventID", indexes);
            packet.ReadInt64("InviteID", indexes);

            packet.ReadByte("Status", indexes);
            packet.ReadByte("Moderator", indexes);
            packet.ReadByte("InviteType", indexes);

            packet.ReadPackedGuid128("InviterGUID", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
            {
                packet.ResetBitReader();
                packet.ReadBit("IgnoreFriendAndGuildRestriction");
            }
        }

        public static void ReadCalendarSendCalendarRaidLockoutInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("InstanceID", indexes);

            packet.ReadInt32("MapID", indexes);
            packet.ReadUInt32("DifficultyID", indexes);
            packet.ReadInt32("ExpireTime", indexes);
        }

        public static void ReadCalendarSendCalendarEventInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventID", indexes);

            packet.ReadByte("EventType", indexes);

            packet.ReadPackedTime("Date", indexes);
            packet.ReadInt32("Flags", indexes);
            packet.ReadInt32("TextureID", indexes);

            packet.ReadUInt64("CommunityID", indexes);
            packet.ReadPackedGuid128("OwnerGUID", indexes);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("EventName", len, indexes);
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");

            var invitesCount = packet.ReadUInt32("InvitesCount");
            var eventsCount = packet.ReadUInt32("EventsCount");
            var raidLockoutsCount = packet.ReadUInt32("RaidLockoutsCount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
            {
                for (int i = 0; i < raidLockoutsCount; i++)
                    ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

                for (int i = 0; i < invitesCount; i++)
                    ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

                for (int i = 0; i < eventsCount; i++)
                    ReadCalendarSendCalendarEventInfo(packet, "Events", i);
            }
            else
            {
                for (int i = 0; i < invitesCount; i++)
                    ReadCalendarSendCalendarInviteInfo(packet, "Invites", i);

                for (int i = 0; i < raidLockoutsCount; i++)
                    ReadCalendarSendCalendarRaidLockoutInfo(packet, "RaidLockouts", i);

                for (int i = 0; i < eventsCount; i++)
                    ReadCalendarSendCalendarEventInfo(packet, "Events", i);
            }
        }
    }
}
