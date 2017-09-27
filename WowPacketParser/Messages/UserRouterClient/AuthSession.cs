using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct AuthSession
    {
        public uint SiteID;
        public sbyte LoginServerType;
        public sbyte BuildType;
        public uint RealmID;
        public ushort Build;
        public uint LocalChallenge;
        public int LoginServerID;
        public uint RegionID;
        public ulong DosResponse;
        public fixed byte Digest[20];
        public string Account;
        public bool UseIPv6;
        public Data AddonInfo;


        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleAuthSession(Packet packet)
        {
            // Do not overwrite version after Handler was initialized
            packet.ReadInt32E<ClientVersionBuild>("Client Build");

            packet.ReadInt32("Unk Int32 1");
            packet.ReadCString("Account");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadInt32("Unk Int32 2");

            packet.ReadUInt32("Client Seed");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5a_12340))
            {
                // Some numbers about selected realm
                packet.ReadInt32("Unk Int32 3");
                packet.ReadInt32("Unk Int32 4");
                packet.ReadInt32("Unk Int32 5");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadInt64("Unk Int64");

            packet.ReadBytes("Proof SHA-1 Hash", 20);

            AddonHandler.ReadClientAddonsList(packet);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleAuthSession422(Packet packet)
        {
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadInt16E<ClientVersionBuild>("Client Build");

            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadByte("Byte");

            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");

            packet.ReadCString("Account name");
            packet.ReadInt32("Int32");

            AddonHandler.ReadClientAddonsList(packet);
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleAuthSession430(Packet packet)
        {
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (1)");
            packet.ReadInt64("Int64");
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (2)");
            packet.ReadInt32("Int32");
            packet.ReadByte("Digest (3)");

            packet.ReadInt32("Int32");
            for (var i = 0; i < 7; i++)
                packet.ReadByte("Digest (4)", i);

            packet.ReadInt16E<ClientVersionBuild>("Client Build");

            for (var i = 0; i < 8; i++)
                packet.ReadByte("Digest (5)", i);

            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");

            packet.ReadUInt32("Client Seed");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("Digest (6)", i);

            var pkt = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(pkt);
            pkt.ClosePacket(false);

            packet.ReadByte("Mask"); // TODO: Seems to affect how the size is read
            var size = (packet.ReadByte() >> 4);
            packet.AddValue("Size", size);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_2_15211, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleAuthSession432(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt32("Int32");
            sha[12] = packet.ReadByte();
            packet.ReadInt32("Int32");
            packet.ReadInt32("Int32");
            sha[0] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[11] = packet.ReadByte();

            packet.ReadInt16E<ClientVersionBuild>("Client Build");

            sha[15] = packet.ReadByte();

            packet.ReadInt64("Int64");
            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");
            sha[3] = packet.ReadByte();
            sha[10] = packet.ReadByte();

            packet.ReadUInt32("Client Seed");

            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadInt32("Int32");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            var pkt = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(pkt);
            pkt.ClosePacket(false);

            var highBits = packet.ReadByte() << 5;
            var lowBits = packet.ReadByte() >> 3;
            var size = lowBits | highBits;
            packet.AddValue("Size", size);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleAuthSession434(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 1");
            packet.ReadUInt32("UInt32 2");
            packet.ReadByte("Unk Byte");
            sha[10] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            packet.ReadInt64("Int64");
            sha[15] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Client Build");
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");
            packet.ReadByte("Unk Byte");
            sha[17] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[2] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            sha[14] = packet.ReadByte();
            sha[13] = packet.ReadByte();

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(12);
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_0_5_16048, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleAuthSession505(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");//18
            sha[2] = packet.ReadByte();//24
            sha[15] = packet.ReadByte();//37
            sha[12] = packet.ReadByte();//34
            sha[11] = packet.ReadByte();//33
            sha[10] = packet.ReadByte();//32
            sha[9] = packet.ReadByte();//31
            packet.ReadByte("Unk Byte");//20
            packet.ReadUInt32("Client seed");//14
            sha[16] = packet.ReadByte();//38
            sha[5] = packet.ReadByte();//27
            packet.ReadInt16E<ClientVersionBuild>("Client Build");//34
            packet.ReadUInt32("UInt32 4");//16
            sha[18] = packet.ReadByte();//40
            sha[0] = packet.ReadByte();//22
            sha[13] = packet.ReadByte();//35
            sha[3] = packet.ReadByte();//25
            sha[14] = packet.ReadByte();//36
            packet.ReadByte("Unk Byte");//21
            sha[8] = packet.ReadByte();//30
            sha[7] = packet.ReadByte();//29
            packet.ReadUInt32("UInt32 3");//15
            sha[4] = packet.ReadByte();//26
            packet.ReadInt64("Int64");//12,13
            sha[17] = packet.ReadByte();//39
            sha[19] = packet.ReadByte();//41
            packet.ReadUInt32("UInt32 1");//4
            sha[6] = packet.ReadByte();//28
            sha[1] = packet.ReadByte();//23

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(12);
            packet.AddValue("Account name", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleAuthSession530(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");//16
            sha[8] = packet.ReadByte();//40
            sha[13] = packet.ReadByte();//45
            sha[3] = packet.ReadByte();//35
            packet.ReadUInt32("UInt32 3");//28
            sha[6] = packet.ReadByte();//38
            packet.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[2] = packet.ReadByte();//34
            sha[0] = packet.ReadByte();//32
            sha[7] = packet.ReadByte();//39
            sha[11] = packet.ReadByte();//43
            packet.ReadUInt32("UInt32 4");//56
            sha[5] = packet.ReadByte();//37
            sha[15] = packet.ReadByte();//47
            sha[14] = packet.ReadByte();//46
            sha[12] = packet.ReadByte();//44
            packet.ReadInt64("Int64");//64,68
            packet.ReadByte("Unk Byte");//61
            packet.ReadUInt32("Client seed");//52
            packet.ReadUInt32("UInt32 1");//24
            sha[1] = packet.ReadByte();//33
            sha[9] = packet.ReadByte();//41
            sha[4] = packet.ReadByte();//36
            sha[17] = packet.ReadByte();//49
            sha[16] = packet.ReadByte();//48
            sha[19] = packet.ReadByte();//51
            sha[18] = packet.ReadByte();//50
            sha[10] = packet.ReadByte();//42
            packet.ReadByte("Unk Byte");//60

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits(11);
            packet.ResetBitReader();
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleAuthSession540(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");
            sha[4] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            packet.ReadByte("Unk Byte");
            sha[19] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[10] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[3] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            packet.ReadInt64("Int64");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("UInt32 1");
            sha[5] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[16] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.ResetBitReader();
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleAuthSession541(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            sha[10] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[9] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[5] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[12] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadInt64("Int64");
            sha[7] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[6] = packet.ReadByte();
            packet.ReadUInt32("UInt32 1");
            sha[15] = packet.ReadByte();
            //packet.ReadUInt32("UInt32 5");

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.ResetBitReader();
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAuthSession547(Packet packet)
        {
            var sha = new byte[20];

            packet.ReadUInt32("UInt32 1");
            packet.ReadUInt32("UInt32 2");

            sha[4] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.ReadUInt32("UInt32 3");

            sha[11] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[5] = packet.ReadByte();

            packet.ReadInt64("Int64");

            sha[10] = packet.ReadByte();

            packet.ReadUInt32("UInt32 4");

            sha[6] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[8] = packet.ReadByte();

            packet.ReadInt16E<ClientVersionBuild>("Client Build");

            sha[1] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[2] = packet.ReadByte();

            packet.ReadByte("Unk Byte");

            packet.ReadUInt32("UInt32 5");
            //packet.ReadUInt32("UInt32 6");

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.ResetBitReader();
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleAuthSession602(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("Grunt ServerId");
            packet.ReadInt16E<ClientVersionBuild>("Client Build");
            packet.ReadUInt32("Region");
            packet.ReadUInt32("Battlegroup");
            packet.ReadUInt32("RealmIndex");
            packet.ReadByte("Login Server Type");
            packet.ReadByte("Unk");
            packet.ReadUInt32("Client Seed");
            packet.ReadUInt64("DosResponse");

            for (uint i = 0; i < 20; ++i)
                sha[i] = packet.ReadByte();

            var accountNameLength = packet.ReadBits("Account Name Length", 11);
            packet.ResetBitReader();
            packet.ReadWoWString("Account Name", accountNameLength);
            packet.ReadBit("UseIPv6");

            var addonSize = packet.ReadInt32("Addons Size");

            if (addonSize > 0)
            {
                var addons = new Packet(packet.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
                AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V6_2_4_21315, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAuthSession624(Packet packet)
        {
            packet.ReadInt16E<ClientVersionBuild>("Build");
            packet.ReadByte("BuildType");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 16);
            packet.ReadBytes("Digest", 24);
            packet.ReadUInt64("DosResponse");

            var addonSize = packet.ReadInt32();
            if (addonSize > 0)
            {
                var addons = new Packet(packet.ReadBytes(addonSize), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
                AddonHandler.ReadClientAddonsList(addons);
                addons.ClosePacket(false);
            }

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
            packet.ReadBit("UseIPv6");

        }

        [Parser(Opcode.CMSG_AUTH_SESSION, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleAuthSession703(Packet packet)
        {
            packet.ReadUInt64("DosResponse");
            packet.ReadInt16E<ClientVersionBuild>("Build");
            packet.ReadByte("BuildType");
            packet.ReadUInt32("RegionID");
            packet.ReadUInt32("BattlegroupID");
            packet.ReadUInt32("RealmID");
            packet.ReadBytes("LocalChallenge", 16);
            packet.ReadBytes("Digest", 24);
            packet.ReadBit("UseIPv6");

            var realmJoinTicketSize = packet.ReadInt32();
            packet.ReadBytes("RealmJoinTicket", realmJoinTicketSize);
        }
    }
}
