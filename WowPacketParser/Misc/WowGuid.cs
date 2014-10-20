using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc.BigMath;

namespace WowPacketParser.Misc
{
    public abstract class WowGuid
    {
        public Int128 Full { get; set; }

        public static WowGuid Empty = new WowGuid64(0);

        public bool HasEntry()
        {
            switch (GetHighType())
            {
                case HighGuidType.Creature:
                case HighGuidType.GameObject:
                case HighGuidType.Vehicle:
                    return true;
                default:
                    return false;
            }
        }

        public abstract ulong GetLow();
        public abstract uint GetEntry();
        public abstract HighGuidType GetHighType();

        public ObjectType GetObjectType()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player:
                    return ObjectType.Player;
                case HighGuidType.DynamicObject:
                    return ObjectType.DynamicObject;
                case HighGuidType.Item:
                    return ObjectType.Item;
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                    //case HighGuidType.MOTransport: ??
                    return ObjectType.GameObject;
                case HighGuidType.Vehicle:
                case HighGuidType.Creature:
                case HighGuidType.Pet:
                    return ObjectType.Unit;
                default:
                    return ObjectType.Object;
            }
        }

        public static bool operator ==(WowGuid first, WowGuid other)
        {
            return first == null && other == null || first != null && first.Equals(other);
        }

        public static bool operator !=(WowGuid first, WowGuid other)
        {
            return !(first == other);
        }

        public static implicit operator Int128(WowGuid guid)
        {
            return guid.Full;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is WowGuid && Equals((WowGuid)obj);
        }

        public bool Equals(WowGuid other)
        {
            return other.Full == Full;
        }

        public override int GetHashCode()
        {
            return Full.GetHashCode();
        }
    }

    public class WowGuid128 : WowGuid
    {
        public WowGuid128(Int128 id)
        {
            Full = id;
        }

        public override HighGuidType GetHighType()
        {
            return (HighGuidType)(byte)((Full >> 58) & 0x3F);
        }

        public byte GetSubType() // move to base?
        {
            return (byte)((Full >> 120) & 0x3F);
        }

        public ushort GetRealmId() // move to base?
        {
            return (ushort)((Full >> 42) & 0x1FFF);
        }

        public ushort GetServerId() // move to base?
        {
            return (ushort)((Full >> 104) & 0x1FFF);
        }

        public ushort GetMapId() // move to base?
        {
            return (ushort)((Full >> 29) & 0x1FFF);
        }

        public override uint GetEntry()
        {
            return (uint)((Full >> 6) & 0x7FFFFF); // Id
        }

        public override ulong GetLow()
        {
            return (ulong)((Full >> 64) & 0xFFFFFFFFFF); // CreationBits
        }

        public override string ToString()
        {
            return Full.ToString(); // TODO
        }
    }

    public class WowGuid64 : WowGuid
    {
        public WowGuid64(ulong id)
        {
            Full = id;
        }

        public WowGuid64()
        {
            Full = 0;
        }

        public override ulong GetLow()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player:
                case HighGuidType.DynamicObject:
                case HighGuidType.RaidGroup:
                case HighGuidType.Item:
                    return (Full & 0x000FFFFFFFFFFFFF).Low;
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                //case HighGuidType.MOTransport: ??
                case HighGuidType.Vehicle:
                case HighGuidType.Creature:
                case HighGuidType.Pet:
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                        return (Full & 0x00000000FFFFFFFF).Low;
                    return (Full & 0x0000000000FFFFFF).Low;
            }

            // TODO: check if entryless guids dont use now more bytes
            return (Full & 0x00000000FFFFFFFF).Low;
        }

        public override uint GetEntry()
        {
            if (!HasEntry())
                return 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return (uint)((Full & 0x000FFFFF00000000) >> 32);
            return     (uint)((Full & 0x000FFFFFFF000000) >> 24);
        }

        public HighGuidTypeLegacy GetHighGuidTypeLegacy()
        {
            if (Full == 0)
                return HighGuidTypeLegacy.None;

            var highGUID = (HighGuidTypeLegacy)((Full & 0xF0F0000000000000).Low >> 52);

            return (highGUID == 0 || (int)highGUID == 8) ? HighGuidTypeLegacy.Player : highGUID;
        }

        public override HighGuidType GetHighType()
        {
            switch (GetHighGuidTypeLegacy())
            {
                case HighGuidTypeLegacy.None:
                    return HighGuidType.Null;
                case HighGuidTypeLegacy.Player:
                    return HighGuidType.Player;
                case HighGuidTypeLegacy.BattleGround1:
                    return HighGuidType.PVPQueueGroup; // ?? unused in wpp
                case HighGuidTypeLegacy.InstanceSave:
                    return HighGuidType.LFGList; // ?? unused in wpp
                case HighGuidTypeLegacy.Group:
                    return HighGuidType.RaidGroup;
                case HighGuidTypeLegacy.BattleGround2:
                    return HighGuidType.PVPQueueGroup; // ?? unused in wpp
                case HighGuidTypeLegacy.MOTransport:
                    return HighGuidType.Transport; // ?? unused in wpp
                case HighGuidTypeLegacy.Guild:
                    return HighGuidType.Guild;
                case HighGuidTypeLegacy.Item:
                    return HighGuidType.Item;
                case HighGuidTypeLegacy.DynObject:
                    return HighGuidType.DynamicObject;
                case HighGuidTypeLegacy.GameObject:
                    return HighGuidType.GameObject;
                case HighGuidTypeLegacy.Transport:
                    return HighGuidType.Transport;
                case HighGuidTypeLegacy.Unit:
                    return HighGuidType.Creature;
                case HighGuidTypeLegacy.Pet:
                    return HighGuidType.Pet;
                case HighGuidTypeLegacy.Vehicle:
                    return HighGuidType.Vehicle;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            if (Full == 0)
                return "0x0";

            // If our guid has an entry and it is an unit or a GO, print its
            // name next to the entry (from a database, if enabled)
            if (HasEntry())
            {
                var type = Utilities.ObjectTypeToStore(GetObjectType());

                return "Full: 0x" + Full.ToString("X8") + " Type: " + GetHighType()
                    + " Entry: " + StoreGetters.GetName(type, (int)GetEntry()) + " Low: " + GetLow();
            }

            var name = StoreGetters.GetName(this);

            switch (GetHighGuidTypeLegacy())
            {
                case HighGuidTypeLegacy.BattleGround1:
                {
                    var bgType = Full & 0x00000000000000FF;
                    return "Full: 0x" + Full.ToString("X8") + " Type: " + GetHighType()
                        + " BgType: " + StoreGetters.GetName(StoreNameType.Battleground, (int)bgType);
                }
                case HighGuidTypeLegacy.BattleGround2:
                {
                    var bgType    = (Full & 0x00FF0000) >> 16;
                    var unkId     = (Full & 0x0000FF00) >> 8;
                    var arenaType = (Full & 0x000000FF) >> 0;
                    return "Full: 0x" + Full.ToString("X8") + " Type: " + GetHighType()
                        + " BgType: " + StoreGetters.GetName(StoreNameType.Battleground, (int)bgType)
                        + " Unk: " + unkId + (arenaType > 0 ? (" ArenaType: " + arenaType) : String.Empty);
                }
            }

            return "Full: 0x" + Full.ToString("X8") + " Type: " + GetHighType()
                + " Low: " + GetLow() + (String.IsNullOrEmpty(name) ? String.Empty : (" Name: " + name));
        }
    }
}
