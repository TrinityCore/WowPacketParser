using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class MultiPacketHandler
    {
        [Parser(Opcode.MSG_MULTIPLE_PACKETS1)] // CMSG_TIME_SYNC_RESP?
        public static void HandleMultiplePackets1(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("ClientToServer: CMSG_PAGE_TEXT_QUERY (0x0E48)");
                QueryHandler.HandlePageTextQuery(packet);
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_NAME_QUERY_RESPONSE (0x0E48)");
                QueryHandler.HandleNameQueryResponse(packet);
            }
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS2)]
        public static void HandleMultiplePackets2(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("ClientToServer: CMSG_NPC_TEXT_QUERY (0x006C)");
                NpcHandler.HandleNpcTextQuery(packet);
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_PLAY_SOUND (0x006C)");
                MiscellaneousHandler.HandlePlaySound(packet);
            }
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS3)]
        public static void HandleMultiplePackets3(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("ClientToServer: CMSG_GAMEOBJECT_QUERY (0x08BC)");
                GameObjectHandler.HandleGameObjectQuery(packet);
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_PET_NAME_QUERY_RESPONSE (0x08BC)");
                PetHandler.HandlePetNameQueryResponse(packet);
            }
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS4)]
        public static void HandleMultiplePackets4(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("ClientToServer: CMSG_PLAYED_TIME (0x0A12)");
                CharacterHandler.HandlePlayedTime(packet);
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_BINDPOINTUPDATE (0x0A12)");
                MovementHandler.HandleBindPointUpdate(packet);
            }
        }

        [Parser(Opcode.MSG_MULTIPLE_PACKETS5)]
        public static void HandleMultiplePackets5(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
            }
            else
            {
                packet.WriteLine("ServerToClient: SMSG_MOVE_TELEPORT (0x0EC5)");
                MovementHandler.HandleMoveTeleport(packet);
            }
        }
    }
}
