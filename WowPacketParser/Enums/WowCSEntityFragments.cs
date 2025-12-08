using System;
using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums
{
    public enum WowCSEntityFragments : int
    {
        CGObject,
        Tag_Item,
        Tag_Container,
        Tag_AzeriteEmpoweredItem,
        Tag_AzeriteItem,
        Tag_Unit,
        Tag_Player,
        Tag_GameObject,
        Tag_DynamicObject,
        Tag_Corpse,
        Tag_AreaTrigger,
        Tag_SceneObject,
        Tag_Conversation,
        Tag_AIGroup,
        Tag_Scenario,
        Tag_LootObject,
        Tag_ActivePlayer,
        Tag_ActiveClient_S,
        Tag_ActiveObject_C,
        Tag_VisibleObject_C,
        Tag_UnitVehicle,
        FEntityPosition,
        FEntityLocalMatrix,
        FEntityWorldMatrix,
        CActor,
        FVendor_C,
        FMirroredObject_C,
        End
    }

    public enum WowCSEntityFragments1100 : int
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
                case WowCSEntityFragments.FVendor_C:
                    return ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_0_7_58630) ||
                        ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_15_8_63829) ||
                        ClientVersion.AddedInVersion(ClientBranch.MoP, ClientVersionBuild.V5_5_0_61735) ||
                        ClientVersion.AddedInVersion(ClientBranch.Cata, ClientVersionBuild.V4_4_2_59185) ||
                        ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_4_59817);
                default:
                    return false;
            }
        }

        public static int GetUpdateBitIndex(List<WowCSEntityFragment> entityFragments, WowCSEntityFragments fragment)
        {
            var index = 0;
            for (var i = 0; i < entityFragments.Count; ++i)
            {
                if (!IsUpdateable(entityFragments[i].UniversalValue))
                    continue;

                if (entityFragments[i].UniversalValue == fragment)
                    return index;

                ++index;
                if (IsIndirect(entityFragments[i].UniversalValue))
                    ++index;
            }

            return -1;
        }

        public static WowCSEntityFragments ToUniversal(WowCSEntityFragments1100 fragment)
        {
            return fragment switch
            {
                WowCSEntityFragments1100.CGObject => WowCSEntityFragments.CGObject,
                WowCSEntityFragments1100.Tag_Item => WowCSEntityFragments.Tag_Item,
                WowCSEntityFragments1100.Tag_Container => WowCSEntityFragments.Tag_Container,
                WowCSEntityFragments1100.Tag_AzeriteEmpoweredItem => WowCSEntityFragments.Tag_AzeriteEmpoweredItem,
                WowCSEntityFragments1100.Tag_AzeriteItem => WowCSEntityFragments.Tag_AzeriteItem,
                WowCSEntityFragments1100.Tag_Unit => WowCSEntityFragments.Tag_Unit,
                WowCSEntityFragments1100.Tag_Player => WowCSEntityFragments.Tag_Player,
                WowCSEntityFragments1100.Tag_GameObject => WowCSEntityFragments.Tag_GameObject,
                WowCSEntityFragments1100.Tag_DynamicObject => WowCSEntityFragments.Tag_DynamicObject,
                WowCSEntityFragments1100.Tag_Corpse => WowCSEntityFragments.Tag_Corpse,
                WowCSEntityFragments1100.Tag_AreaTrigger => WowCSEntityFragments.Tag_AreaTrigger,
                WowCSEntityFragments1100.Tag_SceneObject => WowCSEntityFragments.Tag_SceneObject,
                WowCSEntityFragments1100.Tag_Conversation => WowCSEntityFragments.Tag_Conversation,
                WowCSEntityFragments1100.Tag_AIGroup => WowCSEntityFragments.Tag_AIGroup,
                WowCSEntityFragments1100.Tag_Scenario => WowCSEntityFragments.Tag_Scenario,
                WowCSEntityFragments1100.Tag_LootObject => WowCSEntityFragments.Tag_LootObject,
                WowCSEntityFragments1100.Tag_ActivePlayer => WowCSEntityFragments.Tag_ActivePlayer,
                WowCSEntityFragments1100.Tag_ActiveClient_S => WowCSEntityFragments.Tag_ActiveClient_S,
                WowCSEntityFragments1100.Tag_ActiveObject_C => WowCSEntityFragments.Tag_ActiveObject_C,
                WowCSEntityFragments1100.Tag_VisibleObject_C => WowCSEntityFragments.Tag_VisibleObject_C,
                WowCSEntityFragments1100.Tag_UnitVehicle => WowCSEntityFragments.Tag_UnitVehicle,
                WowCSEntityFragments1100.FEntityPosition => WowCSEntityFragments.FEntityPosition,
                WowCSEntityFragments1100.FEntityLocalMatrix => WowCSEntityFragments.FEntityLocalMatrix,
                WowCSEntityFragments1100.FEntityWorldMatrix => WowCSEntityFragments.FEntityWorldMatrix,
                WowCSEntityFragments1100.CActor => WowCSEntityFragments.CActor,
                WowCSEntityFragments1100.FVendor_C => WowCSEntityFragments.FVendor_C,
                WowCSEntityFragments1100.FMirroredObject_C => WowCSEntityFragments.FMirroredObject_C,
                WowCSEntityFragments1100.End => WowCSEntityFragments.End,
                _ => throw new ArgumentOutOfRangeException(nameof(fragment), fragment, null)
            };
        }
    }

    public readonly record struct WowCSEntityFragment : IComparable<WowCSEntityFragment>
    {
        public readonly WowCSEntityFragments UniversalValue;
        public readonly int VersionValue;

        public WowCSEntityFragment(WowCSEntityFragments1100 versionValue)
        {
            UniversalValue = WowCSUtilities.ToUniversal(versionValue);
            VersionValue = (int)versionValue;
        }

        public int CompareTo(WowCSEntityFragment other)
        {
            return VersionValue.CompareTo(other.VersionValue);
        }
    }
}
