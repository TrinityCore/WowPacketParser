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

        public virtual IObjectData ReadUpdateObjectData(Packet packet, IObjectData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IItemData ReadCreateItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IItemData ReadUpdateItemData(Packet packet, IItemData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IContainerData ReadCreateContainerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IContainerData ReadUpdateContainerData(Packet packet, IContainerData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IAzeriteEmpoweredItemData ReadCreateAzeriteEmpoweredItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteEmpoweredItemData ReadUpdateAzeriteEmpoweredItemData(Packet packet, IAzeriteEmpoweredItemData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IAzeriteItemData ReadCreateAzeriteItemData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAzeriteItemData ReadUpdateAzeriteItemData(Packet packet, IAzeriteItemData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IUnitData ReadCreateUnitData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IUnitData ReadUpdateUnitData(Packet packet, IUnitData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IPlayerData ReadCreatePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IPlayerData ReadUpdatePlayerData(Packet packet, IPlayerData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IActivePlayerData ReadCreateActivePlayerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IActivePlayerData ReadUpdateActivePlayerData(Packet packet, IActivePlayerData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IGameObjectData ReadCreateGameObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IGameObjectData ReadUpdateGameObjectData(Packet packet, IGameObjectData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IDynamicObjectData ReadCreateDynamicObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IDynamicObjectData ReadUpdateDynamicObjectData(Packet packet, IDynamicObjectData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual ICorpseData ReadCreateCorpseData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual ICorpseData ReadUpdateCorpseData(Packet packet, ICorpseData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IAreaTriggerData ReadCreateAreaTriggerData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IAreaTriggerData ReadUpdateAreaTriggerData(Packet packet, IAreaTriggerData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual ISceneObjectData ReadCreateSceneObjectData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual ISceneObjectData ReadUpdateSceneObjectData(Packet packet, ISceneObjectData existingData, params object[] indexes)
        {
            return existingData;
        }

        public virtual IConversationData ReadCreateConversationData(Packet packet, UpdateFieldFlag flags, params object[] indexes)
        {
            return null;
        }

        public virtual IConversationData ReadUpdateConversationData(Packet packet, IConversationData existingData, params object[] indexes)
        {
            return existingData;
        }
    }
}
