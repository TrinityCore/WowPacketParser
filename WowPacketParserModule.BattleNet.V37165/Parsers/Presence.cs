using System;
using System.Collections.Generic;
using System.IO;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Presence
    {
        [BattlenetParser(PresenceServerCommand.UpdateNotify)]
        public static void HandleUpdateNotify(BattlenetPacket packet)
        {
            packet.ReadSkip(6);
            packet.Read<uint>("IdMaster", 0, 32);
            packet.ReadSkip(6);
            var count = packet.Read<int>(0, 4);
            for (var i = 0; i < count; ++i)
                packet.Read<uint>("Handle", 0, 32, "Update", "HandlesCleared", i);

            count = packet.Read<int>(0, 4);
            var varSizes = new Queue<ushort>();
            for (var i = 0; i < count; ++i)
                varSizes.Enqueue(packet.Read<ushort>(0, 16));

            count = packet.Read<int>(0, 4);
            var handles = new List<uint>();
            for (var i = 0; i < count; ++i)
                handles.Add(packet.Read<uint>(0, 32));

            var data = packet.ReadBytes(packet.Read<int>(0, 11));
            HandleUpdateNotifyFields(packet.Stream, handles, data, varSizes);

            if (packet.ReadBoolean())
            {
                packet.Read<uint>("Target", 0, 32, "Level0");
                packet.ReadBoolean("IsLastPacket", "Level0");
            }

            packet.ReadBoolean("ServerOnly");
            packet.Read<byte>("Online", 0, 8);
            packet.Read<uint>("IdLocal", 0, 32);
        }

        class FieldInfo
        {
            public PresenceFieldType FieldType { get; set; }
            public bool VariableLength { get; set; }
            public ushort FixedLength { get; set; }
        }

        private static Dictionary<uint, FieldInfo> _fields = new Dictionary<uint, FieldInfo>();

        private static void HandleUpdateNotifyFields(Packet outputStream, List<uint> handles, byte[] data, Queue<ushort> varSizes)
        {
            var reader = new BinaryReader(new MemoryStream(data));
            for (var i = 0; i < handles.Count; ++i)
            {
                var handle = handles[i];
                FieldInfo field = _fields[handle];
                byte[] fieldData;
                if (field.VariableLength)
                    fieldData = reader.ReadBytes(varSizes.Dequeue());
                else
                    fieldData = reader.ReadBytes(field.FixedLength);

                outputStream.AddValue("Handle", handle, i);
                var bitStream = new BattlenetBitStream(fieldData);
                switch (field.FieldType)
                {
                    case PresenceFieldType.U8:
                        outputStream.AddValue("U8", bitStream.Read<byte>(0, 8), i);
                        break;
                    case PresenceFieldType.S8:
                        outputStream.AddValue("S8", bitStream.Read<sbyte>(sbyte.MinValue, 8), i);
                        break;
                    case PresenceFieldType.U16:
                        outputStream.AddValue("U16", bitStream.Read<ushort>(0, 16), i);
                        break;
                    case PresenceFieldType.S16:
                        outputStream.AddValue("S16", bitStream.Read<short>(short.MinValue, 16), i);
                        break;
                    case PresenceFieldType.U32:
                        outputStream.AddValue("U32", bitStream.Read<uint>(0, 32), i);
                        break;
                    case PresenceFieldType.S32:
                        outputStream.AddValue("S32", bitStream.Read<int>(int.MinValue, 32), i);
                        break;
                    case PresenceFieldType.U64:
                        outputStream.AddValue("U64", bitStream.Read<ulong>(0, 64), i);
                        break;
                    case PresenceFieldType.S64:
                        outputStream.AddValue("S64", bitStream.Read<long>(long.MinValue, 64), i);
                        break;
                    case PresenceFieldType.Float32:
                        outputStream.AddValue("Float32", bitStream.ReadSingle(), i);
                        break;
                    case PresenceFieldType.Float64:
                        outputStream.AddValue("Float64", bitStream.ReadDouble(), i);
                        break;
                    case PresenceFieldType.Bool:
                        outputStream.AddValue("Bool", bitStream.ReadBoolean(), i);
                        break;
                    case PresenceFieldType.FourCC:
                        outputStream.AddValue("FourCC", bitStream.ReadFourCC(), i);
                        break;
                    case PresenceFieldType.StringLiteral:
                        outputStream.AddValue("StringLiteral", bitStream.ReadString(bitStream.Read<int>(0, 9)), i);
                        break;
                    case PresenceFieldType.StringTableEntry:
                        outputStream.AddValue("TableId", bitStream.Read<ushort>(0, 16), i, "StringTableEntry");
                        outputStream.AddValue("Offset", bitStream.Read<ushort>(0, 16), i, "StringTableEntry");
                        break;
                    case PresenceFieldType.ImageTableEntry:
                        outputStream.AddValue("TableId", bitStream.Read<ushort>(0, 16), i, "ImageTableEntry");
                        outputStream.AddValue("Offset", bitStream.Read<ushort>(0, 16), i, "ImageTableEntry");
                        break;
                    case PresenceFieldType.OpaqueData:
                        outputStream.AddValue("OpaqueData", Convert.ToHexString(bitStream.ReadBytes(bitStream.Read<int>(0, 7))), i);
                        break;
                    case PresenceFieldType.ToonFullName:
                        outputStream.AddValue("Region", bitStream.Read<byte>(0, 8), i, "ToonFullName");
                        outputStream.AddValue("ProgramId", bitStream.ReadFourCC(), i, "ToonFullName");
                        outputStream.AddValue("Realm", bitStream.Read<uint>(0, 32), i, "ToonFullName");
                        outputStream.AddValue("Name", bitStream.ReadString(bitStream.Read<int>(2, 7)), i, "ToonFullName");
                        break;
                    case PresenceFieldType.AccountName:
                        outputStream.AddValue("GivenName", bitStream.ReadString(bitStream.Read<int>(0, 8)), i, "AccountName");
                        outputStream.AddValue("Surname", bitStream.ReadString(bitStream.Read<int>(0, 8)), i, "AccountName");
                        break;
                    case PresenceFieldType.ProfileAddress:
                        outputStream.AddValue("Id", bitStream.Read<ulong>(0, 64), i, "ProfileAddress");
                        outputStream.AddValue("Label", bitStream.Read<uint>(0, 32), i, "ProfileAddress");
                        break;
                    case PresenceFieldType.S2GameInfo:
                        outputStream.AddValue("VariantIndex", bitStream.Read<uint>(0, 6), i, "ShortLink");
                        outputStream.AddValue("Speed", bitStream.ReadFourCC(), i, "ShortLink");
                        var entries = bitStream.Read<int>(0, 3);
                        for (var j = 0; j < entries; ++j)
                        {
                            outputStream.AddValue("Id", bitStream.Read<uint>(0, 32), i, "ShortLink", j, "Handle");
                            outputStream.AddValue("Version", bitStream.Read<uint>(0, 32), i, "ShortLink", j, "Handle");
                            outputStream.AddValue("Type", bitStream.Read<uint>(0, 4), i, "ShortLink", j);
                        }
                        var joinable = !bitStream.ReadBoolean();
                        outputStream.AddValue("Joinable", joinable, i, "Advert");
                        if (joinable)
                        {
                            outputStream.AddValue("ServerLabel", bitStream.Read<uint>(0, 32), i, "Advert");
                            outputStream.AddValue("ServerEpoch", bitStream.Read<int>(int.MinValue, 32), i, "Advert");
                            outputStream.AddValue("AdvertId", bitStream.Read<uint>(0, 32), i, "Advert");
                        }
                        break;
                    case PresenceFieldType.AccountInfo:
                        outputStream.AddValue("AccountId", bitStream.Read<uint>(0, 32), i, "AccountInfo");
                        outputStream.AddValue("Region", bitStream.Read<byte>(0, 8), i, "AccountInfo");
                        outputStream.AddValue("GivenName", bitStream.ReadString(bitStream.Read<int>(0, 8)), i, "AccountInfo", "FullName");
                        outputStream.AddValue("Surname", bitStream.ReadString(bitStream.Read<int>(0, 8)), i, "AccountInfo", "FullName");
                        break;
                    case PresenceFieldType.ToonHandle:
                        outputStream.AddValue("Region", bitStream.Read<byte>(0, 8), i, "ToonHandle");
                        outputStream.AddValue("ProgramId", bitStream.ReadFourCC(), i, "ToonHandle");
                        outputStream.AddValue("Realm", bitStream.Read<uint>(0, 32), i, "ToonHandle");
                        outputStream.AddValue("Id", bitStream.Read<ulong>(0, 64), i, "ToonHandle");
                        break;
                    case PresenceFieldType.GameAccountHandle:
                        outputStream.AddValue("Region", bitStream.Read<byte>(0, 8), i, "GameAccountHandle");
                        outputStream.AddValue("ProgramId", bitStream.ReadFourCC(), i, "GameAccountHandle");
                        outputStream.AddValue("Id", bitStream.Read<uint>(0, 32), i, "GameAccountHandle");
                        break;
                    case PresenceFieldType.Achievement:
                        outputStream.AddValue("AchievementId", bitStream.Read<ulong>(0, 64), i, "Achievement");
                        outputStream.AddValue("Completion", bitStream.Read<int>(int.MinValue, 32), i, "Achievement");
                        outputStream.AddValue("EarnedCount", bitStream.Read<uint>(0, 32), i, "Achievement");
                        break;
                    case PresenceFieldType.AccountNickname:
                        outputStream.AddValue("AccountNickname", bitStream.ReadString(bitStream.Read<int>(0, 7)), i);
                        break;
                }
            }
        }

        [BattlenetParser(PresenceServerCommand.FieldSpecAnnounce)]
        public static void HandleFieldSpecAnnounce(BattlenetPacket packet)
        {
            _fields.Clear();

            var fields = packet.Read<int>(0, 7);
            for (var i = 0; i < fields; ++i)
            {
                var field = new FieldInfo();

                packet.ReadBoolean("Writable", i, "Spec");
                packet.ReadBoolean("ServerOnly", i, "Spec");
                packet.ReadBoolean("Ephemeral", i, "Spec");
                field.VariableLength = packet.ReadBoolean("Variable", i, "Spec", "Size");
                if (!field.VariableLength)
                    field.FixedLength = packet.Read<ushort>("Fixed", 0, 16, i, "Spec", "Size");

                field.FieldType = packet.Read<PresenceFieldType>("Id", 0, 8, i, "Spec");
                packet.ReadBoolean("ClientOnly", i, "Spec");
                var handle = packet.Read<uint>("Handle", 0, 32, i);

                _fields.Add(handle, field);
            }
        }
    }
}
