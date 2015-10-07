using System.Collections.Generic;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Parsing.Parsers;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Authentication
    {
        private static readonly List<string> ModulesWaitingForData = new List<string>(16);

        private static void ReadRequestCommon(BattlenetPacket packet, string fieldName)
        {
            packet.ReadFourCC("Program", fieldName);
            packet.ReadFourCC("Platform", fieldName);
            packet.ReadFourCC("Locale", fieldName);

            var components = packet.Read<int>(6);
            for (var i = 0; i < components; ++i)
            {
                packet.ReadFourCC("ProgramId", fieldName, i, "Versions");
                packet.ReadFourCC("Component", fieldName, i, "Versions");
                packet.Read<uint>("Version", 32, fieldName, i, "Versions");
            }
        }

        [BattlenetParser(AuthenticationClientCommand.LogonRequest)]
        public static void HandleLogonRequest(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            if (packet.Read<bool>(1))
                packet.ReadString("Account", packet.Read<int>(9) + 3);
        }

        [BattlenetParser(AuthenticationClientCommand.ProofResponse)]
        public static void HandleProofResponse(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                var dataSize = packet.Read<int>("Data size", 10, i);
                var data = packet.ReadBytes(dataSize);
                if (!module.HandleData(ModulesWaitingForData[i], data, i).HasValue)
                    packet.Stream.AddValue("Data", data, i);
            }

            ModulesWaitingForData.Clear();
        }

        [BattlenetParser(AuthenticationClientCommand.LogonRequest3)]
        public static void HandleLogonRequest3(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            if (packet.Read<bool>(1))
                packet.ReadString("Account", packet.Read<int>(9) + 3);

            packet.Read<ulong>("Compatibility", 64);
        }

        [BattlenetParser(AuthenticationServerCommand.LogonResponse3)]
        public static void HandleLogonResponse3(BattlenetPacket packet)
        {
            var failed = packet.Read<bool>(1);
            if (failed)
            {
                if (packet.Read<bool>(1)) // has module
                {
                    packet.ReadString("Type", 4, "Strings");
                    packet.ReadFourCC("Region", "Strings");
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32, "Strings"));
                    var dataSize = packet.Read<int>("Data size", 10);
                    var data = packet.ReadBytes(dataSize);
                    var module = new BattlenetModuleHandler(packet);

                    if (!module.HandleData(id, data).HasValue)
                        packet.Stream.AddValue("Data", data, "Strings");
                }

                var errorType = packet.Read<byte>(2);
                if (errorType == 1)
                {
                    packet.Read<ushort>("Error", 16);
                    packet.Read<uint>("Failure", 32);
                }
            }
            else
            {
                var modules = packet.Read<byte>(3);
                var module = new BattlenetModuleHandler(packet);
                for (var i = 0; i < modules; ++i)
                {
                    packet.ReadString("Type", 4, i, "FinalRequest");
                    packet.ReadFourCC("Region", i, "FinalRequest");
                    var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32, "FinalRequest"));
                    var dataSize = packet.Read<int>("Data size", i, 10);
                    var data = packet.ReadBytes(dataSize);

                    if (!module.HandleData(id, data, i).HasValue)
                        packet.Stream.AddValue("Data", data, i, "FinalRequest");
                }

                packet.Stream.AddValue("PingTimeout", packet.Read<uint>(32) + int.MinValue);

                if (packet.Read<bool>(1))
                    Connection.ReadRegulatorInfo(packet, "RegulatorRules");

                packet.ReadString("GivenName", packet.Read<int>(8));
                packet.ReadString("Surname", packet.Read<int>(8));

                packet.Read<uint>("AccountId", 32);
                packet.Read<byte>("Region", 8);
                packet.Read<AccountFlags>("Flags", 64);

                packet.Read<byte>("GameAccountRegion", 8);
                packet.ReadString("GameAccountName", packet.Read<int>(5) + 1);
                packet.Read<GameAccountFlags>("GameAccountFlags", 64);

                packet.Read<uint>("LogonFailures", 32);
                if (packet.Read<bool>(1))
                    packet.ReadBytes("Raf", packet.Read<int>(10));
            }
        }

        [BattlenetParser(AuthenticationServerCommand.ProofRequest)]
        public static void HandleServerProofRequest(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                packet.ReadString("Type", 4, i);
                packet.ReadFourCC("Region", i);
                var id = Utilities.ByteArrayToHexString(packet.ReadBytes("ModuleId", 32, i));
                var dataSize = packet.Read<int>("Data size", 10, i);
                var data = packet.ReadBytes(dataSize);

                var result = module.HandleData(id, data, i);
                if (!result.HasValue)
                    packet.Stream.AddValue("Data", data, i);
                else if (!result.Value)
                    ModulesWaitingForData.Add(id);
            }
        }
    }
}
