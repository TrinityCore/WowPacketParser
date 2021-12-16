using System;
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

            var components = packet.Read<int>(0, 6);
            for (var i = 0; i < components; ++i)
            {
                packet.ReadFourCC("ProgramId", fieldName, i, "Versions");
                packet.ReadFourCC("Component", fieldName, i, "Versions");
                packet.Read<uint>("Version", 0, 32, fieldName, i, "Versions");
            }
        }

        [BattlenetParser(AuthenticationClientCommand.LogonRequest)]
        public static void HandleLogonRequest(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            if (packet.ReadBoolean())
                packet.ReadString("Account", 3, 9);
        }

        [BattlenetParser(AuthenticationClientCommand.ResumeRequest)]
        public static void HandleResumeRequest(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            packet.ReadString("Account", 3, 9);
            packet.Read<byte>("GameAccountRegion", 0, 8);
            packet.ReadString("GameAccountName", 1, 5);
        }

        [BattlenetParser(AuthenticationClientCommand.ProofResponse)]
        public static void HandleProofResponse(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(0, 3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                var dataSize = packet.Read<int>("Data size", 0, 10, i);
                var data = packet.ReadBytes(dataSize);
                if (!module.HandleData(ModulesWaitingForData[i], data, i).HasValue)
                    packet.Stream.AddValue("Data", data, i);
            }

            ModulesWaitingForData.Clear();
        }

        [BattlenetParser(AuthenticationClientCommand.GenerateSingleSignOnTokenRequest2)]
        public static void HandleGenerateSingleSignOnTokenRequest2(BattlenetPacket packet)
        {
            packet.Read<uint>("Token", 0, 32);
            if (packet.ReadBoolean())
                packet.ReadFourCC("TargetProgram");
        }

        [BattlenetParser(AuthenticationClientCommand.LogonRequest3)]
        public static void HandleLogonRequest3(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            if (packet.ReadBoolean())
                packet.ReadString("Account", 3, 9);

            packet.Read<ulong>("Compatibility", 0, 64);
        }

        [BattlenetParser(AuthenticationClientCommand.SingleSignOnRequest3)]
        public static void HandleSingleSignOnRequest3(BattlenetPacket packet)
        {
            ReadRequestCommon(packet, "Common");
            packet.ReadByteArray("SsoId", 0, 10);
            packet.Read<ulong>("Compatibility", 0, 64);
        }

        [BattlenetParser(AuthenticationServerCommand.LogonResponse3)]
        public static void HandleLogonResponse3(BattlenetPacket packet)
        {
            if (packet.ReadBoolean())
            {
                if (packet.ReadBoolean()) // has module
                {
                    packet.ReadFixedLengthString("Type", 4, "Strings");
                    packet.ReadFourCC("Region", "Strings");
                    var id = Convert.ToHexString(packet.ReadBytes("ModuleId", 32, "Strings"));
                    var dataSize = packet.Read<int>("Data size", 0, 10);
                    var data = packet.ReadBytes(dataSize);
                    var module = new BattlenetModuleHandler(packet);

                    if (!module.HandleData(id, data).HasValue)
                        packet.Stream.AddValue("Data", data, "Strings");
                }

                var errorType = packet.Read<byte>(2, 0);
                if (errorType == 1)
                {
                    packet.Read<ushort>("Error", 0, 16);
                    packet.Read<uint>("Failure", 0, 32);
                }
            }
            else
            {
                var modules = packet.Read<byte>(0, 3);
                var module = new BattlenetModuleHandler(packet);
                for (var i = 0; i < modules; ++i)
                {
                    packet.ReadFixedLengthString("Type", 4, "FinalRequest", i);
                    packet.ReadFourCC("Region", "FinalRequest", i);
                    var id = Convert.ToHexString(packet.ReadBytes("ModuleId", 32, "FinalRequest", i));
                    var dataSize = packet.Read<int>("Data size", 0, 10, i);
                    var data = packet.ReadBytes(dataSize);

                    if (!module.HandleData(id, data, i).HasValue)
                        packet.Stream.AddValue("Data", data, "FinalRequest", i);
                }

                packet.Read<uint>("PingTimeout", int.MinValue, 32);

                if (packet.ReadBoolean())
                    Connection.ReadRegulatorInfo(packet, "RegulatorRules");

                packet.ReadString("GivenName", 0, 8);
                packet.ReadString("Surname", 0, 8);

                packet.Read<uint>("AccountId", 0, 32);
                packet.Read<byte>("Region", 0, 8);
                packet.Read<AccountFlags>("Flags", 0, 64);

                packet.Read<byte>("GameAccountRegion", 0, 8);
                packet.ReadString("GameAccountName", 1, 5);
                packet.Read<GameAccountFlags>("GameAccountFlags", 0, 64);

                packet.Read<uint>("LogonFailures", 0, 32);
                if (packet.ReadBoolean())
                    packet.ReadByteArray("Raf", 0, 10);
            }
        }

        [BattlenetParser(AuthenticationServerCommand.ResumeResponse)]
        public static void HandleResumeResponse(BattlenetPacket packet)
        {
            if (packet.ReadBoolean())
            {
                if (packet.ReadBoolean()) // has module
                {
                    packet.ReadFixedLengthString("Type", 4, "Strings");
                    packet.ReadFourCC("Region", "Strings");
                    var id = Convert.ToHexString(packet.ReadBytes("ModuleId", 32, "Strings"));
                    var dataSize = packet.Read<int>("Data size", 0, 10);
                    var data = packet.ReadBytes(dataSize);
                    var module = new BattlenetModuleHandler(packet);

                    if (!module.HandleData(id, data).HasValue)
                        packet.Stream.AddValue("Data", data, "Strings");
                }

                var errorType = packet.Read<byte>(2, 0);
                if (errorType == 1)
                {
                    packet.Read<ushort>("Error", 0, 16);
                    packet.Read<uint>("Failure", 0, 32);
                }
            }
            else
            {
                var modules = packet.Read<byte>(0, 3);
                var module = new BattlenetModuleHandler(packet);
                for (var i = 0; i < modules; ++i)
                {
                    packet.ReadFixedLengthString("Type", 4, "FinalRequest", i);
                    packet.ReadFourCC("Region", "FinalRequest", i);
                    var id = Convert.ToHexString(packet.ReadBytes("ModuleId", 32, "FinalRequest", i));
                    var dataSize = packet.Read<int>("Data size", 0, 10, i);
                    var data = packet.ReadBytes(dataSize);

                    if (!module.HandleData(id, data, i).HasValue)
                        packet.Stream.AddValue("Data", data, "FinalRequest", i);
                }

                packet.Read<uint>("PingTimeout", int.MinValue, 32);

                if (packet.ReadBoolean())
                    Connection.ReadRegulatorInfo(packet, "RegulatorRules");
            }
        }

        [BattlenetParser(AuthenticationServerCommand.ProofRequest)]
        public static void HandleProofRequest(BattlenetPacket packet)
        {
            var modules = packet.Read<byte>(0, 3);
            var module = new BattlenetModuleHandler(packet);
            for (var i = 0; i < modules; ++i)
            {
                packet.ReadFixedLengthString("Type", 4, i);
                packet.ReadFourCC("Region", i);
                var id = Convert.ToHexString(packet.ReadBytes("ModuleId", 32, i));
                var dataSize = packet.Read<int>("Data size", 0, 10, i);
                var data = packet.ReadBytes(dataSize);

                var result = module.HandleData(id, data, i);
                if (!result.HasValue)
                    packet.Stream.AddValue("Data", data, i);
                else if (!result.Value)
                    ModulesWaitingForData.Add(id);
            }
        }

        [BattlenetParser(AuthenticationServerCommand.Patch)]
        public static void HandlePatch(BattlenetPacket packet)
        {
            packet.ReadFourCC("ProgramId");
            packet.ReadFourCC("Component");
            packet.ReadString("Instructions", 0, 8);
            packet.ReadBoolean("More");
        }

        [BattlenetParser(AuthenticationServerCommand.AuthorizedLicenses)]
        public static void HandleAuthorizedLicenses(BattlenetPacket packet)
        {
            packet.ReadBoolean("Persistent");
            var licenses = packet.Read<int>(0, 9);
            for (var i = 0; i < licenses; ++i)
            {
                if (packet.ReadBoolean())
                    packet.Read<int>("Expiration", int.MinValue, 32);
                packet.Read<uint>("Id", 0, 32);
            }
        }

        [BattlenetParser(AuthenticationServerCommand.GenerateSingleSignOnTokenResponse2)]
        public static void HandleGenerateSingleSignOnTokenResponse2(BattlenetPacket packet)
        {
            packet.Read<uint>("Token", 0, 32);
            if (!packet.ReadBoolean())
            {
                packet.ReadByteArray("SsoToken", 0, 9);
                packet.ReadByteArray("SsoId", 0, 10);
            }
            else
                packet.Read<uint>("Error", 0, 16);
        }
    }
}
