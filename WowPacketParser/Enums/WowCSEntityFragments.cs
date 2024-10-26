using System.Collections.Generic;

namespace WowPacketParser.Enums
{
    public enum WowCSEntityFragments : int
    {
        CGObject = 0,
        Tag_Item = 1,
        Tag_Container = 2,
        Tag_AzeriteEmpoweredItem = 3,
        Tag_AzeriteItem = 4,
        Tag_Unit = 5,
        Tag_Player = 6,
        Tag_GameObject = 7,
        Tag_DynamicObject = 8,
        Tag_Corpse = 9,
        Tag_AreaTrigger = 10,
        Tag_SceneObject = 11,
        Tag_Conversation = 12,
        Tag_AIGroup = 13,
        Tag_Scenario = 14,
        Tag_LootObject = 15,
        Tag_ActivePlayer = 16,
        Tag_ActiveClient_S = 17,
        Tag_ActiveObject_C = 18,
        Tag_VisibleObject_C = 19,
        Tag_UnitVehicle = 20,
        FEntityPosition = 112,
        FEntityLocalMatrix = 113,
        FEntityWorldMatrix = 114,
        CActor = 115,
        FVendor_C = 117,
        FMirroredObject_C = 119,
        End = 255
    }

    public static class WowCSUtilities
    {
        public static bool IsUpdateable(WowCSEntityFragments fragment)
        {
            switch (fragment)
            {
                case WowCSEntityFragments.CGObject:
                case WowCSEntityFragments.FVendor_C:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsIndirect(WowCSEntityFragments fragment)
        {
            switch (fragment)
            {
                case WowCSEntityFragments.CGObject:
                case WowCSEntityFragments.CActor:
                    return true;
                default:
                    return false;
            }
        }

        public static int GetUpdateBitIndex(List<WowCSEntityFragments> entityFragments, WowCSEntityFragments fragment)
        {
            var index = 0;
            for (var i = 0; i < entityFragments.Count; ++i)
            {
                if (!IsUpdateable(entityFragments[i]))
                    continue;

                if (entityFragments[i] == fragment)
                    return index;

                ++index;
                if (IsIndirect(entityFragments[i]))
                    ++index;
            }

            return -1;
        }
    }
}
