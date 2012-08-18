using PacketParser.Enums;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public sealed class GameObject : WoWObject
    {
        public override bool IsTemporarySpawn()
        {
            // If our gameobject got the following update field set,
            // it's probably a temporary spawn
            UpdateField uf;
            if (UpdateFields.TryGetValue((int)(Enums.Version.UpdateFields.GetUpdateFieldOffset(GameObjectField.GAMEOBJECT_FIELD_CREATED_BY)), out uf))
                return uf.UInt32Value != 0;

            return false;
        }

        public float[] GetRotation()
        {
            return UpdateFields.GetArray<GameObjectField, float>(GameObjectField.GAMEOBJECT_PARENTROTATION, 4);
        }

        public bool IsTransport()
        {
            UpdateField uf;
            if (UpdateFields.TryGetValue((int)Enums.Version.UpdateFields.GetUpdateFieldOffset(GameObjectField.GAMEOBJECT_BYTES_1), out uf))
                return (GameObjectType)((uf.UInt32Value & 0x0000FF00) >> 8) == GameObjectType.MOTransport;

            return false;
        }
    }
}
