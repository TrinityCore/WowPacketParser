using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL.SQLStore;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_GAMEOBJECT_QUERY)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            QueryHandler.ReadQueryHeader(packet);
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry();
            Console.WriteLine("Entry: " + entry.Key);

            if (entry.Value)
                return;

            var type = (GameObjectType)packet.ReadInt32();
            Console.WriteLine("Type: " + type);

            var dispId = packet.ReadInt32();
            Console.WriteLine("Display ID: " + dispId);

            var name = new string[4];
            for (var i = 0; i < 4; i++)
            {
                name[i] = packet.ReadCString();
                Console.WriteLine("Name " + i + ": " + name[i]);
            }

            var iconName = packet.ReadCString();
            Console.WriteLine("Icon Name: " + iconName);

            var castCaption = packet.ReadCString();
            Console.WriteLine("Cast Caption: " + castCaption);

            var unkStr = packet.ReadCString();
            Console.WriteLine("Unk String: " + unkStr);

            var data = new int[24];
            for (var i = 0; i < 24; i++)
            {
                data[i] = packet.ReadInt32();
                Console.WriteLine("Data " + i + ": " + data[i]);
            }

            var size = packet.ReadSingle();
            Console.WriteLine("Size: " + size);

            var qItem = new int[6];
            for (var i = 0; i < 6; i++)
            {
                qItem[i] = packet.ReadInt32();
                Console.WriteLine("Quest Item " + i + ": " + qItem[i]);
            }

            Store.WriteData(Store.GameObjects.GetCommand(entry.Key, type, dispId, name[0], iconName,
                castCaption, unkStr, data, size, qItem));
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            var goGuid = packet.ReadPackedGuid();
            Console.WriteLine("GO GUID: " + goGuid);

            var vehGuid = packet.ReadPackedGuid();
            Console.WriteLine("Vehicle GUID: " + vehGuid);

            var plrGuid = packet.ReadPackedGuid();
            Console.WriteLine("Player GUID: " + plrGuid);

            var dmg = packet.ReadInt32();
            Console.WriteLine("Damage: " + dmg);

            var spellId = packet.ReadInt32();
            Console.WriteLine("Spell ID: " + spellId);
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM)]
        public static void HandleGODespawnAnim(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }
    }
}
