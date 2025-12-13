using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;

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

        [Parser(Opcode.SMSG_GAME_OBJECT_SET_STATE_LOCAL)]
        public static void HandleGameObjectSetStateLocal(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadByte("State");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_INTERACTION)]
        public static void HandleGameObjectInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("InteractionType");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CLOSE_INTERACTION)]
        public static void HandleGameObjectCloseInteractionResponse(Packet packet)
        {
            packet.ReadInt32("InteractionType");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL)]
        public static void HandleGameObjectPlaySpellVisual(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadPackedGuid128("ActivatorGUID");
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGoUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
        }
        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        public static void HandleGoReportUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
            use.Report = true;
        }

    }
}
