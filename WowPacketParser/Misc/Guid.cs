using System;
using System.Text;
using PacketParser.Enums;
using PacketParser.Processing;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public struct Guid
    {
        public readonly ulong Full;
        private readonly HighGuidType HighType;

        public Guid(ulong id)
            : this()
        {
            Full = id;
            HighType = _GetHighType();
        }

        public bool HasEntry()
        {
            switch (GetHighType())
            {
                case HighGuidType.Unit:
                case HighGuidType.GameObject:
                case HighGuidType.Vehicle:
                    return true;
                default:
                    return false;
            }
        }

        public ulong GetLow()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player:
                case HighGuidType.DynObject:
                case HighGuidType.Group:
                case HighGuidType.Item:
                    return Full & 0x000FFFFFFFFFFFFF;
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                case HighGuidType.MOTransport:
                case HighGuidType.Vehicle:
                case HighGuidType.Unit:
                case HighGuidType.Pet:
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                        return Full & 0x00000000FFFFFFFF;
                    return Full & 0x0000000000FFFFFF;
            }

            // TODO: check if entryless guids dont use now more bytes
            return Full & 0x00000000FFFFFFFF;
        }

        public uint GetEntry()
        {
            if (!HasEntry())
                return 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return (uint)((Full & 0x000FFFFF00000000) >> 32);
            return     (uint)((Full & 0x000FFFFFFF000000) >> 24);
        }

        public HighGuidType GetHighType()
        {
            return HighType;
        }

        private HighGuidType _GetHighType()
        {
            if (Full == 0)
                return HighGuidType.None;

            var highGUID = (HighGuidType)((Full & 0xF0F0000000000000) >> 52);

            return highGUID == 0 ? HighGuidType.Player : highGUID;
        }

        public string GetHighTypeString()
        {
            //return GetHighType().ToString();
            switch (GetHighType())
            {
                case HighGuidType.Player:
                    return "Player";
                case HighGuidType.DynObject:
                    return "DynObject";
                case HighGuidType.Item:
                    return "Item";
                case HighGuidType.GameObject:
                    return "GameObject";
                case HighGuidType.Transport:
                    return "Transport";
                case HighGuidType.MOTransport:
                    return "MOTransport";
                case HighGuidType.Vehicle:
                    return "Vehicle";
                case HighGuidType.Unit:
                    return "Unit";
                case HighGuidType.Pet:
                    return "Pet";
                case HighGuidType.BattleGround1:
                    return "BattleGround1";
                case HighGuidType.BattleGround2:
                    return "BattleGround2";
                case HighGuidType.InstanceSave:
                    return "InstanceSave";
                case HighGuidType.Group:
                    return "Group";
                case HighGuidType.Guild:
                    return "Guild";
                default:
                    return "Unknown (" + ((uint)GetHighType()).ToString();
            }
        }

        public ObjectType GetObjectType()
        {
            switch (GetHighType())
            {
                case HighGuidType.Player:
                    return ObjectType.Player;
                case HighGuidType.DynObject:
                    return ObjectType.DynamicObject;
                case HighGuidType.Item:
                    return ObjectType.Item;
                case HighGuidType.GameObject:
                case HighGuidType.Transport:
                case HighGuidType.MOTransport:
                    return ObjectType.GameObject;
                case HighGuidType.Vehicle:
                case HighGuidType.Unit:
                case HighGuidType.Pet:
                    return ObjectType.Unit;
                default:
                    return ObjectType.Object;
            }
        }

        public static bool operator ==(Guid first, Guid other)
        {
            return first.Full == other.Full;
        }

        public static bool operator !=(Guid first, Guid other)
        {
            return !(first == other);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Guid && Equals((Guid)obj);
        }

        public bool Equals(Guid other)
        {
            return other.Full == Full;
        }

        public override int GetHashCode()
        {
            return Full.GetHashCode();
        }

        public override string ToString()
        {
            if (Full == 0)
                return "0x0";

            StringBuilder builder = new StringBuilder(80);
            // If our guid has an entry and it is an unit or a GO, print its
            // name next to the entry (from a database, if enabled)
            if (HasEntry())
            {
                var type = Utilities.ObjectTypeToStore(GetObjectType());

                builder.Append("Full 0x");
                builder.Append(Full.ToString("X8"));
                builder.Append(" Type: ");
                builder.Append(GetHighTypeString());
                builder.Append(" Entry: ");
                builder.Append(PacketFileProcessor.Current.GetProcessor<NameStore>().GetName(type, (int)GetEntry()));
                builder.Append(" Low: ");
                builder.Append(GetLow());
                return builder.ToString();
            }

            switch (GetHighType())
            {
                case HighGuidType.BattleGround1:
                {
                    var bgType = Full & 0x00000000000000FF;
                    builder.Append("Full 0x");
                    builder.Append(Full.ToString("X8"));
                    builder.Append(" Type: ");
                    builder.Append(GetHighTypeString());
                    builder.Append(" BgType: ");
                    builder.Append(PacketFileProcessor.Current.GetProcessor<NameStore>().GetName(StoreNameType.Battleground, (int)bgType));
                    return builder.ToString();
                }
                case HighGuidType.BattleGround2:
                {
                    var bgType    = (Full & 0x00FF0000) >> 16;
                    var unkId     = (Full & 0x0000FF00) >> 8;
                    var arenaType = (Full & 0x000000FF) >> 0;
                    builder.Append("Full 0x");
                    builder.Append(Full.ToString("X8"));
                    builder.Append(" Type: ");
                    builder.Append(GetHighTypeString());
                    builder.Append(" BgType: ");
                    builder.Append(PacketFileProcessor.Current.GetProcessor<NameStore>().GetName(StoreNameType.Battleground, (int)bgType));
                    builder.Append(" Unk: ");
                    builder.Append(unkId);
                    if (arenaType > 0)
                    {
                        builder.Append(" ArenaType: ");
                        builder.Append(arenaType);
                    }
                    return builder.ToString();
                }
            }

            var name = PacketFileProcessor.Current.GetProcessor<NameStore>().GetPlayerName(this);
            builder.Append("Full 0x");
            builder.Append(Full.ToString("X8"));
            builder.Append(" Type: ");
            builder.Append(GetHighTypeString());
            builder.Append(" Low: ");
            builder.Append(GetLow());
            if (!String.IsNullOrEmpty(name))
            {
                builder.Append(" Name: ");
                builder.Append(name);
            }
            return builder.ToString();
        }
    }
}
