using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CharacterHandler
    {
        public static readonly Dictionary<Guid, CharacterInfo> Characters =
            new Dictionary<Guid, CharacterInfo>();

        public static readonly List<StartInfo> StartInfos = new List<StartInfo>();

        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Name: " + name);

            var race = (Race)packet.ReadByte();
            Console.WriteLine("Race: " + race);

            var chClass = (Class)packet.ReadByte();
            Console.WriteLine("Class: " + chClass);

            var gender = (Gender)packet.ReadByte();
            Console.WriteLine("Gender: " + gender);

            var skin = packet.ReadByte();
            Console.WriteLine("Skin: " + skin);

            var face = packet.ReadByte();
            Console.WriteLine("Face: " + face);

            var hairStyle = packet.ReadByte();
            Console.WriteLine("Hair Style: " + hairStyle);

            var hairColor = packet.ReadByte();
            Console.WriteLine("Hair Color: " + hairColor);

            var facialHair = packet.ReadByte();
            Console.WriteLine("Facial Hair: " + facialHair);

            var outfitId = packet.ReadByte();
            Console.WriteLine("Outfit ID: " + outfitId);
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.CMSG_CHAR_RENAME)]
        public static void HandleClientCharRename(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var newName = packet.ReadCString();
            Console.WriteLine("New Name: " + newName);
        }

        [Parser(Opcode.SMSG_CHAR_RENAME)]
        public static void HandleServerCharRename(Packet packet)
        {
            var result = (ResponseCode)packet.ReadByte();
            Console.WriteLine("Response: " + result);

            if (result != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var name = packet.ReadCString();
            Console.WriteLine("Name: " + name);
        }

        [Parser(Opcode.SMSG_CHAR_CREATE)]
        [Parser(Opcode.SMSG_CHAR_DELETE)]
        public static void HandleCharResponse(Packet packet)
        {
            var response = (ResponseCode)packet.ReadByte();
            Console.WriteLine("Response: " + response);
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            var hairStyle = packet.ReadByte();
            Console.WriteLine("Hair Style: " + hairStyle);

            var hairColor = packet.ReadByte();
            Console.WriteLine("Hair Color: " + hairColor);

            var facialHair = packet.ReadByte();
            Console.WriteLine("Facial Hair: " + facialHair);
        }

        [Parser(Opcode.SMSG_BARBER_SHOP_RESULT)]
        public static void HandleBarberShopResult(Packet packet)
        {
            var status = (BarberShopResult)packet.ReadInt32();
            Console.WriteLine("Result: " + status);
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var name = packet.ReadCString();
            Console.WriteLine("New Name: " + name);

            var gender = (Gender)packet.ReadByte();
            Console.WriteLine("Gender: " + gender);

            var skin = packet.ReadByte();
            Console.WriteLine("Skin: " + skin);

            var face = packet.ReadByte();
            Console.WriteLine("Face: " + face);

            var hairColor = packet.ReadByte();
            Console.WriteLine("Hair Color: " + hairColor);

            var hairStyle = packet.ReadByte();
            Console.WriteLine("Hair Style: " + hairStyle);

            var facialHair = packet.ReadByte();
            Console.WriteLine("Facial Hair: " + facialHair);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            var response = (ResponseCode)packet.ReadByte();
            Console.WriteLine("Response: " + response);

            if (response != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var name = packet.ReadCString();
            Console.WriteLine("New Name: " + name);

            var gender = (Gender)packet.ReadByte();
            Console.WriteLine("Gender: " + gender);

            var skin = packet.ReadByte();
            Console.WriteLine("Skin: " + skin);

            var face = packet.ReadByte();
            Console.WriteLine("Face: " + face);

            var hairStyle = packet.ReadByte();
            Console.WriteLine("Hair Style: " + hairStyle);

            var hairColor = packet.ReadByte();
            Console.WriteLine("Hair Color: " + hairColor);

            var facialHair = packet.ReadByte();
            Console.WriteLine("Facial Hair: " + facialHair);
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            Characters.Clear();

            var count = packet.ReadByte();
            Console.WriteLine("Count: " + count);

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid();
                Console.WriteLine("GUID: " + guid);

                var name = packet.ReadCString();
                Console.WriteLine("Name: " + name);

                var race = (Race)packet.ReadByte();
                Console.WriteLine("Race: " + race);

                var clss = (Class)packet.ReadByte();
                Console.WriteLine("Class: " + clss);

                var gender = (Gender)packet.ReadByte();
                Console.WriteLine("Gender: " + gender);

                var skin = packet.ReadByte();
                Console.WriteLine("Skin: " + skin);

                var face = packet.ReadByte();
                Console.WriteLine("Face: " + face);

                var hairStyle = packet.ReadByte();
                Console.WriteLine("Hair Style: " + hairStyle);

                var hairColor = packet.ReadByte();
                Console.WriteLine("Hair Color: " + hairColor);

                var facialHair = packet.ReadByte();
                Console.WriteLine("Facial Hair: " + facialHair);

                var level = packet.ReadByte();
                Console.WriteLine("Level: " + level);

                var zone = packet.ReadInt32();
                Console.WriteLine("Zone ID: " + zone);

                var mapId = packet.ReadInt32();
                Console.WriteLine("Map ID: " + mapId);

                var pos = packet.ReadVector3();
                Console.WriteLine("Position: " + pos);

                var guild = packet.ReadInt32();
                Console.WriteLine("Guild ID: " + guild);

                var flags = (CharacterFlag)packet.ReadInt32();
                Console.WriteLine("Character Flags: " + flags);

                var customize = (CustomizationFlag)packet.ReadInt32();
                Console.WriteLine("Customization Flags: " + customize);

                var firstLogin = packet.ReadBoolean();
                Console.WriteLine("First Login: " + firstLogin);

                var petDispId = packet.ReadInt32();
                Console.WriteLine("Pet Display ID: " + petDispId);

                var petLevel = packet.ReadInt32();
                Console.WriteLine("Pet Level: " + petLevel);

                var petFamily = (CreatureFamily)packet.ReadInt32();
                Console.WriteLine("Pet Family: " + petFamily);

                for (var j = 0; j < 19; j++)
                {
                    var dispId = packet.ReadInt32();
                    Console.WriteLine("Equip Display ID " + j + ": " + dispId);

                    var invType = (InventoryType)packet.ReadByte();
                    Console.WriteLine("Equip Inventory Type " + j + ": " + invType);

                    var auraId = packet.ReadInt32();
                    Console.WriteLine("Equip Aura ID " + j + ": " + auraId);
                }

                for (var j = 0; j < 4; j++)
                {
                    var bagDispId = packet.ReadInt32();
                    Console.WriteLine("Bag Display ID " + j + ": " + bagDispId);

                    var bagInvType = (InventoryType)packet.ReadByte();
                    Console.WriteLine("Bag Inventory Type " + j + ": " + bagInvType);

                    var bagAuraId = packet.ReadInt32();
                    Console.WriteLine("Bag Aura ID " + j + ": " + bagAuraId);
                }

                if (firstLogin)
                {
                    var shouldAdd = true;
                    foreach (var item in StartInfos)
                    {
                        if (item.Race != race && item.Class != clss)
                            continue;

                        shouldAdd = false;
                        break;
                    }

                    if (shouldAdd)
                    {
                        var startInfo = new StartInfo();
                        startInfo.Race = race;
                        startInfo.Class = clss;
                        startInfo.Position = pos;
                        startInfo.Map = mapId;
                        startInfo.Zone = zone;

                        StartInfos.Add(startInfo);
                        SQLStore.WriteData(SQLStore.StartPositions.GetCommand(race, clss, mapId, zone, pos));
                    }
                }

                var chInfo = new CharacterInfo();
                chInfo.Guid = guid;
                chInfo.Race = race;
                chInfo.Class = clss;
                chInfo.Name = name;
                chInfo.FirstLogin = firstLogin;
                chInfo.Level = level;

                Characters.Add(guid, chInfo);
            }
        }

        // Belongs here?
        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            var unk1 = packet.ReadSingle();
            Console.WriteLine("Unk Float: " + unk1);

            var unk2 = packet.ReadByte();
            Console.WriteLine("Unk UInt8: " + unk2);

            var amount = packet.ReadInt32();
            Console.WriteLine("Count: " + amount);

            for (int i = 0; i < amount; i++)
            {
                var listId = packet.ReadInt32();
                Console.WriteLine("Faction List ID: " + listId);

                var standing = packet.ReadInt32();
                Console.WriteLine("Standing: " + standing);
            }
        }
    }
}
