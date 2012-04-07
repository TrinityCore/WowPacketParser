using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public sealed class GameObject : WoWObject
    {
        public override bool IsTemporarySpawn()
        {
            // If our gameobject got the following update field set,
            // it's probably a temporary spawn
            UpdateField uf;
            if (UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_FIELD_CREATED_BY), out uf))
                return uf.UInt32Value != 0;

            return false;
        }

        public float[] GetRotation()
        {
            return UpdateFields.GetArray<GameObjectField, float>(GameObjectField.GAMEOBJECT_PARENTROTATION, 4);
        }
    }
}
