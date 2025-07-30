using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AuthenticationHandler
    {
        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            for (uint i = 0; i < 8; ++i)
                packet.ReadUInt32("DosChallenge", i);
            packet.ReadBytes("Challenge", 32);
            packet.ReadByte("DosZeroBits");
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            packet.ReadInt64("DosResponse");
            packet.ReadInt64("Key");
            packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            packet.ReadUInt64("DosResponse");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 32);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }

        [Parser(Opcode.SMSG_ENTER_ENCRYPTED_MODE)]
        public static void HandleEnterEncryptedMode(Packet packet)
        {
            packet.ReadInt32("RegionGroup");
            packet.ReadBytes("Signature (ED25519)", 64);
            packet.ReadBit("Enabled");
        }

        [Parser(Opcode.SMSG_SUSPEND_COMMS)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadBytes("Where (RSA encrypted)", 256);

            AddressType type = packet.ReadByteE<AddressType>("Type");
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress("Address");
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address("Address");
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString("Address", 128);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port");
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64("Key");
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            packet.ReadUInt32E<BattlenetRpcErrorCode>("Result");

            var ok = packet.ReadBit("Success");
            var queued = packet.ReadBit("Queued");
            if (ok)
            {
                packet.ReadUInt32("VirtualRealmAddress");
                var realms = packet.ReadUInt32();
                packet.ReadUInt32("TimeRested");
                packet.ReadByte("ActiveExpansionLevel");
                packet.ReadByte("AccountExpansionLevel");
                packet.ReadUInt32("TimeSecondsUntilPCKick");
                var classes = packet.ReadUInt32("AvailableClasses");
                var templates = packet.ReadUInt32("Templates");
                packet.ReadUInt32("AccountCurrency");
                packet.ReadTime64("Time");

                for (var i = 0; i < classes; ++i)
                {
                    packet.ReadByteE<Race>("RaceID", "AvailableClasses", i);
                    var classesForRace = packet.ReadUInt32();
                    for (var j = 0u; j < classesForRace; ++j)
                    {
                        packet.ReadByteE<Class>("ClassID", "AvailableClasses", i, "Classes", j);
                        packet.ReadByteE<ClientType>("ActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                        packet.ReadByteE<ClientType>("AccountExpansionLevel", "AvailableClasses", i, "Classes", j);
                        packet.ReadByte("MinActiveExpansionLevel", "AvailableClasses", i, "Classes", j);
                    }
                }

                packet.ResetBitReader();
                packet.ReadBit("IsExpansionTrial");
                packet.ReadBit("ForceCharacterTemplate");
                var horde = packet.ReadBit(); // NumPlayersHorde
                var alliance = packet.ReadBit(); // NumPlayersAlliance
                var trialExpiration = packet.ReadBit(); // ExpansionTrialExpiration
                var hasNewBuildKeys = packet.ReadBit();

                packet.ResetBitReader();
                packet.ReadUInt32("BillingPlan");
                packet.ReadUInt32("TimeRemain");
                packet.ReadUInt32("Unk_V7_3_5");

                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");
                packet.ReadBit("InGameRoom");

                if (horde)
                    packet.ReadUInt16("NumPlayersHorde");

                if (alliance)
                    packet.ReadUInt16("NumPlayersAlliance");

                if (trialExpiration)
                    packet.ReadInt64("ExpansionTrialExpiration");

                if (hasNewBuildKeys)
                {
                    var newBuildKey = new byte[16];
                    var someKey = new byte[16];
                    for (var i = 0; i < 16; i++)
                    {
                        newBuildKey[i] = packet.ReadByte();
                        someKey[i] = packet.ReadByte();
                    }
                    packet.AddValue("NewBuildKey", Encoding.UTF8.GetString(newBuildKey));
                    packet.AddValue("SomeKey", Encoding.UTF8.GetString(someKey));
                }

                for (var i = 0; i < realms; ++i)
                    SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealms", i);

                for (var i = 0; i < templates; ++i)
                {
                    packet.ReadUInt32("TemplateSetId", i);
                    var templateClasses = packet.ReadUInt32();
                    for (var j = 0; j < templateClasses; ++j)
                    {
                        packet.ReadByteE<Class>("Class", i, j);
                        packet.ReadByte("FactionGroup", i, j);
                    }

                    packet.ResetBitReader();
                    var nameLen = packet.ReadBits(7);
                    var descLen = packet.ReadBits(10);
                    packet.ReadWoWString("Name", nameLen, i);
                    packet.ReadWoWString("Description", descLen, i);
                }
            }

            if (queued)
            {
                packet.ReadUInt32("WaitCount");
                packet.ReadUInt32("WaitTime");
                packet.ReadByte("AllowedFactionGroupForCharacterCreate");
                packet.ResetBitReader();
                packet.ReadBit("HasFCM");
                packet.ReadBit("CanCreateOnlyIfExisting");
            }
        }

        [Parser(Opcode.SMSG_RESUME_COMMS)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
