using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParser.Parsing.Parsers
{
    public abstract class UpdateFieldsHandlerBase
    {
        public virtual IObjectData ReadCreateObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IObjectData ReadUpdateObjectData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IItemData ReadCreateItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IItemData ReadUpdateItemData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IContainerData ReadCreateContainerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IContainerData ReadUpdateContainerData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteEmpoweredItemData ReadCreateAzeriteEmpoweredItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteEmpoweredItemData ReadUpdateAzeriteEmpoweredItemData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteItemData ReadCreateAzeriteItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteItemData ReadUpdateAzeriteItemData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IUnitData ReadCreateUnitData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IUnitData ReadUpdateUnitData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IPlayerData ReadCreatePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IPlayerData ReadUpdatePlayerData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IActivePlayerData ReadCreateActivePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IActivePlayerData ReadUpdateActivePlayerData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IGameObjectData ReadCreateGameObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IGameObjectData ReadUpdateGameObjectData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IDynamicObjectData ReadCreateDynamicObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IDynamicObjectData ReadUpdateDynamicObjectData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual ICorpseData ReadCreateCorpseData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual ICorpseData ReadUpdateCorpseData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IAreaTriggerData ReadCreateAreaTriggerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAreaTriggerData ReadUpdateAreaTriggerData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual ISceneObjectData ReadCreateSceneObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual ISceneObjectData ReadUpdateSceneObjectData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IConversationData ReadCreateConversationData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IConversationData ReadUpdateConversationData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IMeshObjectData ReadCreateMeshObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IMeshObjectData ReadUpdateMeshObjectData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IVendorData ReadCreateVendorData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IVendorData ReadUpdateVendorData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingDecorData ReadCreateHousingDecorData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingDecorData ReadUpdateHousingDecorData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingRoomData ReadCreateHousingRoomData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingRoomData ReadUpdateHousingRoomData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingRoomComponentMeshData ReadCreateHousingRoomComponentMeshData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingRoomComponentMeshData ReadUpdateHousingRoomComponentMeshData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingPlayerHouseData ReadCreateHousingPlayerHouseData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingPlayerHouseData ReadUpdateHousingPlayerHouseData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingCornerstoneData ReadCreateHousingCornerstoneData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingCornerstoneData ReadUpdateHousingCornerstoneData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingPlotAreaTriggerData ReadCreateHousingPlotAreaTriggerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingPlotAreaTriggerData ReadUpdateHousingPlotAreaTriggerData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual INeighborhoodMirrorData ReadCreateNeighborhoodMirrorData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual INeighborhoodMirrorData ReadUpdateNeighborhoodMirrorData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IMirroredPositionData ReadCreateMirroredPositionData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IMirroredPositionData ReadUpdateMirroredPositionData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IPlayerHouseInfoComponentData ReadCreatePlayerHouseInfoComponentData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IPlayerHouseInfoComponentData ReadUpdatePlayerHouseInfoComponentData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingStorageData ReadCreateHousingStorageData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingStorageData ReadUpdateHousingStorageData(Packet packet, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingFixtureData ReadCreateHousingFixtureData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IHousingFixtureData ReadUpdateHousingFixtureData(Packet packet, params object[] indexes)
        {
            return null;
        }
    }
}
