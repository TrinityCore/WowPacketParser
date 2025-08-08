using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT)]
        public static void HandleGameObjectActivateAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("AnimKitID");
            packet.ReadBit("Maintain");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            customAnim.GameObject = packet.ReadPackedGuid128("ObjectGUID");
            customAnim.Anim = packet.ReadInt32("CustomAnim");
            customAnim.PlayAsDespawn = packet.ReadBit("PlayAsDespawn");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_DESPAWN)]
        public static void HandleGameObjectDespawn(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_FORCE_OBJECT_RELINK)]
        public static void HandleForceObjectRelink(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Owner");
            packet.ReadInt32("Damage");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_PAGE_TEXT)]
        public static void HandleGoPageText(Packet packet)
        {
            packet.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_RESET_STATE)]
        public static void HandleGameObjectResetState(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }
    }
}
