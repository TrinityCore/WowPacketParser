using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGoUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGoReportUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
            use.Report = true;
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectActivateAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("AnimKitID");
            packet.ReadBit("Maintain");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_DESPAWN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectDespawn(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Owner");
            packet.ReadInt32("Damage");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CLOSE_INTERACTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectCloseInteractionResponse(Packet packet)
        {
            packet.ReadInt32("InteractionType");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            customAnim.GameObject = packet.ReadPackedGuid128("ObjectGUID");
            customAnim.Anim = packet.ReadInt32("CustomAnim");
            customAnim.PlayAsDespawn = packet.ReadBit("PlayAsDespawn");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_INTERACTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("InteractionType");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectPlaySpellVisual(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadPackedGuid128("ActivatorGUID");
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_SET_STATE_LOCAL, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGameObjectSetStateLocal(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadByte("State");
        }

        [Parser(Opcode.SMSG_PAGE_TEXT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleGoPageText(Packet packet)
        {
            packet.ReadPackedGuid128("GameObjectGUID");
        }
    }
}
