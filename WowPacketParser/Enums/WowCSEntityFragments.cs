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
        Tag_HousingRoom,
        Tag_MeshObject,
        Tag_HouseExteriorPiece,
        Tag_HouseExteriorRoot,
        FEntityPosition,
        FEntityLocalMatrix,
        FEntityWorldMatrix,
        FTransportLink,
        FPlayerOwnershipLink,
        CActor,
        FVendor_C,
        FMirroredObject_C,
        FMeshObjectData_C,
        FHousingDecor_C,
        FHousingRoom_C,
        FHousingRoomComponentMesh_C,
        FHousingPlayerHouse_C,
        FJamHousingCornerstone_C,
        FHousingDecorActor_C,
        FHousingPlotAreaTrigger_C,
        FNeighborhoodMirrorData_C,
        FMirroredPositionData_C,
        PlayerHouseInfoComponent_C,
        FHousingStorage_C,
        FHousingFixture_C,
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

    public enum WowCSEntityFragments1127 : int
    {
        FEntityPosition = 1,
        CGObject = 2,
        FTransportLink = 5,
        FPlayerOwnershipLink = 13,
        CActor = 15,
        FVendor_C = 17,
        FMirroredObject_C = 18,
        FMeshObjectData_C = 19,
        FHousingDecor_C = 20,
        FHousingRoom_C = 21,
        FHousingRoomComponentMesh_C = 22,
        FHousingPlayerHouse_C = 23,
        FJamHousingCornerstone_C = 27,
        FHousingDecorActor_C = 28,
        FHousingPlotAreaTrigger_C = 29,
        FNeighborhoodMirrorData_C = 30,
        FMirroredPositionData_C = 31,
        PlayerHouseInfoComponent_C = 32,
        FHousingStorage_C = 33,
        FHousingFixture_C = 34,
        Tag_Item = 200,
        Tag_Container = 201,
        Tag_AzeriteEmpoweredItem = 202,
        Tag_AzeriteItem = 203,
        Tag_Unit = 204,
        Tag_Player = 205,
        Tag_GameObject = 206,
        Tag_DynamicObject = 207,
        Tag_Corpse = 208,
        Tag_AreaTrigger = 209,
        Tag_SceneObject = 210,
        Tag_Conversation = 211,
        Tag_AIGroup = 212,
        Tag_Scenario = 213,
        Tag_LootObject = 214,
        Tag_ActivePlayer = 215,
        Tag_ActiveClient_S = 216,
        Tag_ActiveObject_C = 217,
        Tag_VisibleObject_C = 218,
        Tag_UnitVehicle = 219,
        Tag_HousingRoom = 220,
        Tag_MeshObject = 221,
        Tag_HouseExteriorPiece = 224,
        Tag_HouseExteriorRoot = 225,
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
                case WowCSEntityFragments.FMeshObjectData_C:
                case WowCSEntityFragments.FHousingDecor_C:
                case WowCSEntityFragments.FHousingRoom_C:
                case WowCSEntityFragments.FHousingRoomComponentMesh_C:
                case WowCSEntityFragments.FHousingPlayerHouse_C:
                case WowCSEntityFragments.FJamHousingCornerstone_C:
                case WowCSEntityFragments.FHousingPlotAreaTrigger_C:
                case WowCSEntityFragments.FNeighborhoodMirrorData_C:
                case WowCSEntityFragments.FMirroredPositionData_C:
                case WowCSEntityFragments.PlayerHouseInfoComponent_C:
                case WowCSEntityFragments.FHousingStorage_C:
                case WowCSEntityFragments.FHousingFixture_C:
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
                case WowCSEntityFragments.FPlayerOwnershipLink:
                case WowCSEntityFragments.PlayerHouseInfoComponent_C:
                    return true;
                case WowCSEntityFragments.FVendor_C:
                    return (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_0_7_58630) && ClientVersion.RemovedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_2_7_64632)) ||
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

        public static WowCSEntityFragments ToUniversal(WowCSEntityFragments1127 fragment)
        {
            return fragment switch
            {
                WowCSEntityFragments1127.End => WowCSEntityFragments.End,
                WowCSEntityFragments1127.FEntityPosition => WowCSEntityFragments.FEntityPosition,
                WowCSEntityFragments1127.CGObject => WowCSEntityFragments.CGObject,
                WowCSEntityFragments1127.FTransportLink => WowCSEntityFragments.FTransportLink,
                WowCSEntityFragments1127.FPlayerOwnershipLink => WowCSEntityFragments.FPlayerOwnershipLink,
                WowCSEntityFragments1127.CActor => WowCSEntityFragments.CActor,
                WowCSEntityFragments1127.FVendor_C => WowCSEntityFragments.FVendor_C,
                WowCSEntityFragments1127.FMirroredObject_C => WowCSEntityFragments.FMirroredObject_C,
                WowCSEntityFragments1127.FMeshObjectData_C => WowCSEntityFragments.FMeshObjectData_C,
                WowCSEntityFragments1127.FHousingDecor_C => WowCSEntityFragments.FHousingDecor_C,
                WowCSEntityFragments1127.FHousingRoom_C => WowCSEntityFragments.FHousingRoom_C,
                WowCSEntityFragments1127.FHousingRoomComponentMesh_C => WowCSEntityFragments.FHousingRoomComponentMesh_C,
                WowCSEntityFragments1127.FHousingPlayerHouse_C => WowCSEntityFragments.FHousingPlayerHouse_C,
                WowCSEntityFragments1127.FJamHousingCornerstone_C => WowCSEntityFragments.FJamHousingCornerstone_C,
                WowCSEntityFragments1127.FHousingDecorActor_C => WowCSEntityFragments.FHousingDecorActor_C,
                WowCSEntityFragments1127.FHousingPlotAreaTrigger_C => WowCSEntityFragments.FHousingPlotAreaTrigger_C,
                WowCSEntityFragments1127.FNeighborhoodMirrorData_C => WowCSEntityFragments.FNeighborhoodMirrorData_C,
                WowCSEntityFragments1127.FMirroredPositionData_C => WowCSEntityFragments.FMirroredPositionData_C,
                WowCSEntityFragments1127.PlayerHouseInfoComponent_C => WowCSEntityFragments.PlayerHouseInfoComponent_C,
                WowCSEntityFragments1127.FHousingStorage_C => WowCSEntityFragments.FHousingStorage_C,
                WowCSEntityFragments1127.FHousingFixture_C => WowCSEntityFragments.FHousingFixture_C,
                WowCSEntityFragments1127.Tag_Item => WowCSEntityFragments.Tag_Item,
                WowCSEntityFragments1127.Tag_Container => WowCSEntityFragments.Tag_Container,
                WowCSEntityFragments1127.Tag_AzeriteEmpoweredItem => WowCSEntityFragments.Tag_AzeriteEmpoweredItem,
                WowCSEntityFragments1127.Tag_AzeriteItem => WowCSEntityFragments.Tag_AzeriteItem,
                WowCSEntityFragments1127.Tag_Unit => WowCSEntityFragments.Tag_Unit,
                WowCSEntityFragments1127.Tag_Player => WowCSEntityFragments.Tag_Player,
                WowCSEntityFragments1127.Tag_GameObject => WowCSEntityFragments.Tag_GameObject,
                WowCSEntityFragments1127.Tag_DynamicObject => WowCSEntityFragments.Tag_DynamicObject,
                WowCSEntityFragments1127.Tag_Corpse => WowCSEntityFragments.Tag_Corpse,
                WowCSEntityFragments1127.Tag_AreaTrigger => WowCSEntityFragments.Tag_AreaTrigger,
                WowCSEntityFragments1127.Tag_SceneObject => WowCSEntityFragments.Tag_SceneObject,
                WowCSEntityFragments1127.Tag_Conversation => WowCSEntityFragments.Tag_Conversation,
                WowCSEntityFragments1127.Tag_AIGroup => WowCSEntityFragments.Tag_AIGroup,
                WowCSEntityFragments1127.Tag_Scenario => WowCSEntityFragments.Tag_Scenario,
                WowCSEntityFragments1127.Tag_LootObject => WowCSEntityFragments.Tag_LootObject,
                WowCSEntityFragments1127.Tag_ActivePlayer => WowCSEntityFragments.Tag_ActivePlayer,
                WowCSEntityFragments1127.Tag_ActiveClient_S => WowCSEntityFragments.Tag_ActiveClient_S,
                WowCSEntityFragments1127.Tag_ActiveObject_C => WowCSEntityFragments.Tag_ActiveObject_C,
                WowCSEntityFragments1127.Tag_VisibleObject_C => WowCSEntityFragments.Tag_VisibleObject_C,
                WowCSEntityFragments1127.Tag_UnitVehicle => WowCSEntityFragments.Tag_UnitVehicle,
                WowCSEntityFragments1127.Tag_HousingRoom => WowCSEntityFragments.Tag_HousingRoom,
                WowCSEntityFragments1127.Tag_MeshObject => WowCSEntityFragments.Tag_MeshObject,
                WowCSEntityFragments1127.Tag_HouseExteriorPiece => WowCSEntityFragments.Tag_HouseExteriorPiece,
                WowCSEntityFragments1127.Tag_HouseExteriorRoot => WowCSEntityFragments.Tag_HouseExteriorRoot,
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

        public WowCSEntityFragment(WowCSEntityFragments1127 versionValue)
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
