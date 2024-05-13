using System;
using WowPacketParser.Enums;
using WowPacketParser.PacketStructures;
using WowPacketParser.Proto;

namespace WowPacketParser.Misc
{
    public abstract class WowGuid
    {
        public ulong Low { get; protected set; }
        public HighGuid HighGuid { get; protected set; }
        public ulong High { get; protected set; }

        public bool HasEntry()
        {
            switch (GetHighType())
            {
                case HighGuidType.Creature:
                case HighGuidType.GameObject:
                case HighGuidType.Pet:
                case HighGuidType.Vehicle:
                case HighGuidType.AreaTrigger:
                case HighGuidType.Cast:
                    return true;
                default:
                    return false;
            }
        }

        public abstract ulong GetLow();
        public abstract uint GetEntry();

        public HighGuidType GetHighType()
        {
            return HighGuid.GetHighGuidType();
        }

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
                case HighGuidType.AreaTrigger:
                    return ObjectType.AreaTrigger;
                default:
                    return ObjectType.Object;
            }
        }

        public static bool operator ==(WowGuid first, WowGuid other)
        {
            if (ReferenceEquals(first, other))
                return true;

            if (((object) first == null) || ((object) other == null))
                return false;

            return first.Equals(other);
        }

        public static bool operator !=(WowGuid first, WowGuid other)
        {
            return !(first == other);
        }

        public override bool Equals(object obj)
        {
            return obj is WowGuid && Equals((WowGuid)obj);
        }

        public bool Equals(WowGuid other)
        {
            return other.Low == Low && other.High == High;
        }

        public override int GetHashCode()
        {
            return new { Low, High }.GetHashCode();
        }

        public bool IsEmpty()
        {
            return High == 0 && Low == 0;
        }

        public static implicit operator UniversalGuid(WowGuid guid) => guid.ToUniversalGuid();

        public abstract UniversalGuid ToUniversalGuid();
    }

    public class WowGuid128 : WowGuid
    {
        public WowGuid128(ulong low, ulong high)
        {
            Low = low;
            High = high;
            if (ClientVersion.Build >= ClientVersionBuild.V7_0_3_22248)
                HighGuid = new HighGuid703((byte)((High >> 58) & 0x3F));
            else if (ClientVersion.Build >= ClientVersionBuild.V6_2_4_21315)
                HighGuid = new HighGuid624((byte)((High >> 58) & 0x3F));
            else
                HighGuid = new HighGuid623((byte)((High >> 58) & 0x3F));
        }

        public byte GetSubType() // move to base?
        {
            return (byte)(High & 0x3F);
        }

        public ushort GetRealmId() // move to base?
        {
            return (ushort)((High >> 42) & 0x1FFF);
        }

        public uint GetServerId() // move to base?
        {
            return (uint)((Low >> 40) & 0xFFFFFF);
        }

        public ushort GetMapId() // move to base?
        {
            return (ushort)((High >> 29) & 0x1FFF);
        }

        public override uint GetEntry()
        {
            return (uint)((High >> 6) & 0x7FFFFF); // Id
        }

        public override UniversalGuid ToUniversalGuid()
        {
            return this.ToUniversal();
        }

        public override ulong GetLow()
        {
            return Low & 0xFFFFFFFFFF; // CreationBits
        }

        public override string ToString()
        {
            if (Low == 0 && High == 0)
                return "Full: 0x0";

            if (HasEntry())
            {
                StoreNameType type = StoreNameType.None;
                if (GetHighType() == HighGuidType.Cast)
                    type = StoreNameType.Spell;
                else
                    type = Utilities.ObjectTypeToStore(GetObjectType());

                // ReSharper disable once UseStringInterpolation
                return string.Format("Full: 0x{0}{1} {2}/{3} R{4}/S{5} Map: {6} Entry: {7} Low: {8}", High.ToString("X16"), Low.ToString("X16"),
                    GetHighType(), GetSubType(), GetRealmId(), GetServerId(), StoreGetters.GetName(StoreNameType.Map, GetMapId()),
                    StoreGetters.GetName(type, (int)GetEntry()), GetLow());
            }

            // TODO: Implement extra format for battleground, see WowGuid64.ToString()

            string name = StoreGetters.GetName(this);

            // ReSharper disable once UseStringInterpolation
            return string.Format("Full: 0x{0}{1} {2}/{3} R{4}/S{5} Map: {6} Low: {7}", High.ToString("X16"), Low.ToString("X16"),
                    GetHighType(), GetSubType(), GetRealmId(), GetServerId(), StoreGetters.GetName(StoreNameType.Map, GetMapId()),
                    GetLow() + (String.IsNullOrEmpty(name) ? String.Empty : (" Name: " + name)));
        }
    }

    public class WowGuid64 : WowGuid
    {
        public static WowGuid Empty = new WowGuid64(0);

        public WowGuid64(ulong id)
        {
            Low = id;
            HighGuid = new HighGuidLegacy(GetHighGuidTypeLegacy());
        }

        public override ulong GetLow()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player:
                case HighGuidType.DynamicObject:
                case HighGuidType.RaidGroup:
                case HighGuidType.Item:
                    return Low & 0x000FFFFFFFFFFFFF;
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                //case HighGuidType.MOTransport: ??
                case HighGuidType.Vehicle:
                case HighGuidType.Creature:
                case HighGuidType.Pet:
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                        return Low & 0x00000000FFFFFFFFul;
                    return Low & 0x0000000000FFFFFFul;
            }

            // TODO: check if entryless guids dont use now more bytes
            return Low & 0x00000000FFFFFFFFul;
        }

        public override uint GetEntry()
        {
            if (!HasEntry())
                return 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return (uint)((Low & 0x000FFFFF00000000) >> 32);
            return (uint)((Low & 0x000FFFFFFF000000) >> 24);
        }

        public override UniversalGuid ToUniversalGuid()
        {
            return this.ToUniversal();
        }

        public HighGuidTypeLegacy GetHighGuidTypeLegacy()
        {
            if (Low == 0)
                return HighGuidTypeLegacy.None;

            var highGUID = (HighGuidTypeLegacy)((Low & 0xF0F0000000000000) >> 52);
            switch ((int)highGUID & 0xF00)
            {
                case 0x0:
                    return HighGuidTypeLegacy.Player;
                case 0x400:
                    return HighGuidTypeLegacy.Item;
                default:
                    return highGUID;
            }
        }

        public override string ToString()
        {
            if (Low == 0)
                return "0x0";

            // If our guid has an entry and it is an unit or a GO, print its
            // name next to the entry (from a database, if enabled)
            if (HasEntry())
            {
                var type = Utilities.ObjectTypeToStore(GetObjectType());

                return "Full: 0x" + Low.ToString("X8") + " Type: " + GetHighType()
                    + " Entry: " + StoreGetters.GetName(type, (int)GetEntry()) + " Low: " + GetLow();
            }

            var name = StoreGetters.GetName(this);

            switch (GetHighGuidTypeLegacy())
            {
                case HighGuidTypeLegacy.BattleGround1:
                {
                    var bgType = Low & 0x00000000000000FF;
                    return "Full: 0x" + Low.ToString("X8") + " Type: " + GetHighType()
                        + " BgType: " + StoreGetters.GetName(StoreNameType.Battleground, (int)bgType);
                }
                case HighGuidTypeLegacy.BattleGround2:
                {
                    var bgType = (Low & 0x00FF0000) >> 16;
                    var unkId = (Low & 0x0000FF00) >> 8;
                    var arenaType = (Low & 0x000000FF) >> 0;
                    return "Full: 0x" + Low.ToString("X8") + " Type: " + GetHighType()
                        + " BgType: " + StoreGetters.GetName(StoreNameType.Battleground, (int)bgType)
                        + " Unk: " + unkId + (arenaType > 0 ? (" ArenaType: " + arenaType) : String.Empty);
                }
            }

            return "Full: 0x" + Low.ToString("X8") + " Type: " + GetHighType()
                + " Low: " + GetLow() + (String.IsNullOrEmpty(name) ? String.Empty : (" Name: " + name));
        }
    }
}
