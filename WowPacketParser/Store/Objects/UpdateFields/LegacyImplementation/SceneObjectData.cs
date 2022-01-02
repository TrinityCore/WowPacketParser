using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class SceneObjectData : ISceneObjectData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;
        private Dictionary<int, List<UpdateField>> DynamicUpdateFields => Object.DynamicUpdateFields;

        public SceneObjectData(WoWObject obj)
        {
            Object = obj;
        }

        public int? ScriptPackageID => UpdateFields.GetValue<SceneObjectField, int>(SceneObjectField.SCENEOBJECT_FIELD_SCRIPT_PACKAGE_ID);

        public uint? SceneType => UpdateFields.GetValue<SceneObjectField, uint>(SceneObjectField.SCENEOBJECT_FIELD_SCENE_TYPE);
    }
}
