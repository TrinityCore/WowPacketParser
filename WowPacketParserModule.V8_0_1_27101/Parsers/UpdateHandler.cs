using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V7_0_3_22248.Parsers;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using SplineFlag = WowPacketParserModule.V7_0_3_22248.Enums.SplineFlag;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class UpdateHandler
    {
        [HasSniffData] // in ReadCreateObjectBlock
        [Parser(Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(Packet packet)
        {
            var count = packet.ReadUInt32("NumObjUpdates");
            uint map = packet.ReadUInt16<MapId>("MapID");
            packet.ResetBitReader();
            var hasDestroyObject = packet.ReadBit("HasDestroyObjects");
            if (hasDestroyObject)
            {
                packet.ReadInt16("Int0");
                var destroyObjCount = packet.ReadUInt32("DestroyObjectsCount");
                for (var i = 0; i < destroyObjCount; i++)
                    packet.ReadPackedGuid128("Object GUID", i);
            }
            packet.ReadUInt32("Data size");

            for (var i = 0; i < count; i++)
            {
                var type = packet.ReadByte();
                var typeString = ((UpdateTypeCataclysm)type).ToString();

                packet.AddValue("UpdateType", typeString, i);
                switch (typeString)
                {
                    case "Values":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        CoreParsers.UpdateHandler.ReadValuesUpdateBlock(packet, guid, i);
                        break;
                    }
                    case "CreateObject1":
                    case "CreateObject2":
                    {
                        var guid = packet.ReadPackedGuid128("Object Guid", i);
                        ReadCreateObjectBlock(packet, guid, map, i);
                        break;
                    }
                }
            }
        }

        private static void ReadValuesCreateBlock(Dictionary<int, UpdateField> dict, Dictionary<int, List<UpdateField>> dynDict, Packet packet, ObjectType type, object index)
        {
            packet.ReadUInt32("ValuesBlockSize", index);
            UpdateFieldCreateFlag flags = (UpdateFieldCreateFlag)packet.ReadByte("Flags", index);

            int fieldCount = 0;
            switch (type)
            {
                case ObjectType.Container:
                    {
                        fieldCount = UpdateFields.GetUpdateField(ContainerField.CONTAINER_END);
                        break;
                    }
                case ObjectType.Item:
                    {
                        fieldCount = UpdateFields.GetUpdateField(ItemField.ITEM_END);
                        break;
                    }
                case ObjectType.AzeriteEmpoweredItem:
                    {
                        fieldCount = UpdateFields.GetUpdateField(AzeriteEmpoweredItemField.AZERITE_EMPOWERED_ITEM_END);
                        break;
                    }
                case ObjectType.AzeriteItem:
                    {
                        fieldCount = UpdateFields.GetUpdateField(AzeriteItemField.AZERITE_ITEM_END);
                        break;
                    }
                case ObjectType.Player:
                    {
                        fieldCount = UpdateFields.GetUpdateField(PlayerField.PLAYER_END);
                        break;
                    }
                case ObjectType.ActivePlayer:
                    {
                        fieldCount = UpdateFields.GetUpdateField(ActivePlayerField.ACTIVE_PLAYER_END);
                        break;
                    }
                case ObjectType.Unit:
                    {
                        fieldCount = UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_END);
                        break;
                    }
                case ObjectType.GameObject:
                    {
                        fieldCount = UpdateFields.GetUpdateField(GameObjectField.GAMEOBJECT_END);
                        break;
                    }
                case ObjectType.DynamicObject:
                    {
                        fieldCount = UpdateFields.GetUpdateField(DynamicObjectField.DYNAMICOBJECT_END);
                        break;
                    }
                case ObjectType.Corpse:
                    {
                        fieldCount = UpdateFields.GetUpdateField(CorpseField.CORPSE_END);
                        break;
                    }
                case ObjectType.AreaTrigger:
                    {
                        fieldCount = UpdateFields.GetUpdateField(AreaTriggerField.AREATRIGGER_END);
                        break;
                    }
                case ObjectType.SceneObject:
                    {
                        fieldCount = UpdateFields.GetUpdateField(SceneObjectField.SCENEOBJECT_FIELD_END);
                        break;
                    }
                case ObjectType.Conversation:
                    {
                        fieldCount = UpdateFields.GetUpdateField(ConversationField.CONVERSATION_END);
                        break;
                    }
            }

            int objectEnd = UpdateFields.GetUpdateField(ObjectField.OBJECT_END);
            var dynamicCounterStore = new List<int>();
            var currentArrayInfo = new List<UpdateFieldInfo>();
            int maxArrayIndex = 0;
            int maxArrayPosition = 0;
            int currentArrayPosition = 0;
            int currentArrayIndex = 0;
            bool isArray = false;
            bool isDynamicArray = false;
            for (var i = 0; i < fieldCount; ++i)
            {
                string key = "Block Value " + i;
                UpdateFieldInfo fieldInfo = null;

                if (i < objectEnd)
                {
                    key = UpdateFields.GetUpdateFieldName<ObjectField>(i);
                    fieldInfo = UpdateFields.GetUpdateFieldInfo<ObjectField>(i);
                }
                else
                {
                    switch (type)
                    {
                        case ObjectType.Container:
                            {
                                if (i < UpdateFields.GetUpdateField(ItemField.ITEM_END))
                                    goto case ObjectType.Item;

                                key = UpdateFields.GetUpdateFieldName<ContainerField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<ContainerField>(i);
                                break;
                            }
                        case ObjectType.Item:
                            {
                                key = UpdateFields.GetUpdateFieldName<ItemField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<ItemField>(i);
                                break;
                            }
                        case ObjectType.AzeriteEmpoweredItem:
                            {
                                if (i < UpdateFields.GetUpdateField(ItemField.ITEM_END))
                                    goto case ObjectType.Item;

                                key = UpdateFields.GetUpdateFieldName<AzeriteEmpoweredItemField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<AzeriteEmpoweredItemField>(i);
                                break;
                            }
                        case ObjectType.AzeriteItem:
                            {
                                if (i < UpdateFields.GetUpdateField(ItemField.ITEM_END))
                                    goto case ObjectType.Item;

                                key = UpdateFields.GetUpdateFieldName<AzeriteItemField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<AzeriteItemField>(i);
                                break;
                            }
                        case ObjectType.Player:
                            {
                                if (i < UpdateFields.GetUpdateField(UnitField.UNIT_END) || i < UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_END))
                                    goto case ObjectType.Unit;

                                key = UpdateFields.GetUpdateFieldName<PlayerField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<PlayerField>(i);
                                break;
                            }
                        case ObjectType.ActivePlayer:
                            {
                                if (i < UpdateFields.GetUpdateField(PlayerField.PLAYER_END))
                                    goto case ObjectType.Player;

                                key = UpdateFields.GetUpdateFieldName<ActivePlayerField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<ActivePlayerField>(i);
                                break;
                            }
                        case ObjectType.Unit:
                            {
                                key = UpdateFields.GetUpdateFieldName<UnitField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<UnitField>(i);
                                break;
                            }
                        case ObjectType.GameObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<GameObjectField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<GameObjectField>(i);
                                break;
                            }
                        case ObjectType.DynamicObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<DynamicObjectField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<DynamicObjectField>(i);
                                break;
                            }
                        case ObjectType.Corpse:
                            {
                                key = UpdateFields.GetUpdateFieldName<CorpseField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<CorpseField>(i);
                                break;
                            }
                        case ObjectType.AreaTrigger:
                            {
                                key = UpdateFields.GetUpdateFieldName<AreaTriggerField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<AreaTriggerField>(i);
                                break;
                            }
                        case ObjectType.SceneObject:
                            {
                                key = UpdateFields.GetUpdateFieldName<SceneObjectField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<SceneObjectField>(i);
                                break;
                            }
                        case ObjectType.Conversation:
                            {
                                key = UpdateFields.GetUpdateFieldName<ConversationField>(i);
                                fieldInfo = UpdateFields.GetUpdateFieldInfo<ConversationField>(i);
                                break;
                            }
                    }
                }
                int size = 1;
                bool dynamicCounter = false;
                UpdateFieldCreateFlag flag = UpdateFieldCreateFlag.None;
                UpdateFieldType updateFieldType = UpdateFieldType.Default;
                if (fieldInfo != null)
                {
                    //key = fieldInfo.Name;
                    size = fieldInfo.Size;
                    updateFieldType = fieldInfo.Format;
                    dynamicCounter = fieldInfo.IsCounter;
                    flag = fieldInfo.Flag;

                    if (i > objectEnd && flag != 0 && !flags.HasAnyFlag(flag))
                        continue;

                    // Reset array data if new array starts
                    if (fieldInfo.ArrayInfo == UpdateFieldArrayInfo.InfoStart)
                    {
                        isDynamicArray = false;
                        isArray = false;
                        currentArrayInfo.Clear();
                        maxArrayPosition = 0;
                        maxArrayIndex = 0;
                        currentArrayIndex = 0;
                        currentArrayPosition = 0;;
                    }
                    // Reset array data if end is reached
                    if (maxArrayPosition != 0 && currentArrayPosition > maxArrayPosition)
                    {
                        isDynamicArray = false;
                        isArray = false;
                        currentArrayInfo.Clear();
                        maxArrayPosition = 0;
                        maxArrayIndex = 0;
                        currentArrayIndex = 0;
                        currentArrayPosition = 0;
                    }

                    // Set isArray if field is part of an arrayGroup
                    if (fieldInfo.ArrayInfo == UpdateFieldArrayInfo.InfoStart)
                    {
                        isArray = true;
                    }

                    // fill necessary data if not collected yet
                    if (isArray && maxArrayPosition == 0 && maxArrayIndex == 0)
                    {
                        currentArrayInfo.Add(fieldInfo);

                        if (fieldInfo.Size > 1)
                        {
                            maxArrayPosition = fieldInfo.Size + currentArrayInfo.Count - 2;
                            maxArrayIndex = ((maxArrayPosition + 1) / currentArrayInfo.Count) - 1;
                        }
                    }

                    if (fieldInfo.ArrayInfo == UpdateFieldArrayInfo.InfoEnd)
                    {
                        switch (fieldInfo.Format)
                        {
                            case UpdateFieldType.DynamicByte:
                            case UpdateFieldType.DynamicUshort:
                            case UpdateFieldType.DynamicShort:
                            case UpdateFieldType.DynamicUint:
                            case UpdateFieldType.DynamicInt:
                            case UpdateFieldType.DynamicFloat:
                            case UpdateFieldType.DynamicGuid:
                                {
                                    if (!isDynamicArray)
                                    {
                                        isDynamicArray = true;
                                        maxArrayIndex = dynamicCounterStore[0];
                                    }
                                    break;
                                }
                        }
                    }
                }

                // override fieldinfo with array info
                if (isArray && maxArrayPosition != 0 && maxArrayIndex != 0)
                {
                    UpdateFieldInfo overrideFieldInfo = currentArrayInfo[currentArrayPosition % currentArrayInfo.Count];

                    if (overrideFieldInfo != null)
                    {
                        key = overrideFieldInfo.Name;
                        updateFieldType = overrideFieldInfo.Format;
                        currentArrayIndex = currentArrayPosition / currentArrayInfo.Count;
                    }
                }

                if (isDynamicArray)
                {
                    for (var arrayIndex = 0; arrayIndex < maxArrayIndex; arrayIndex++)
                    {
                        foreach (var arrayInfo in currentArrayInfo)
                        {
                            switch (arrayInfo.Format)
                            {
                                case UpdateFieldType.DynamicByte:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadByte(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadByte(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicShort:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadInt16(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadInt16(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicUshort:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadUInt16(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadUInt16(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicUint:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadUInt32(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadUInt32(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicInt:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadInt32(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadInt32(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicFloat:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadSingle(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(value));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadSingle(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(value));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                                case UpdateFieldType.DynamicGuid:
                                    {
                                        if (dynDict.ContainsKey(arrayInfo.Value))
                                        {
                                            var value = packet.ReadPackedGuid128(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            dynDict[arrayInfo.Value].Add(new UpdateField(0));
                                        }
                                        else
                                        {
                                            var store = new List<UpdateField>();
                                            var value = packet.ReadPackedGuid128(arrayIndex > 0 ? arrayInfo.Name + " + " + arrayIndex : arrayInfo.Name, index);
                                            store.Add(new UpdateField(0));
                                            dynDict.Add(arrayInfo.Value, store);
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                    dynamicCounterStore.RemoveAt(0);
                    isDynamicArray = false;
                    isArray = false;
                    currentArrayInfo.Clear();
                    maxArrayPosition = 0;
                    maxArrayIndex = 0;
                    currentArrayIndex = 0;
                    currentArrayPosition = 0;
                }
                else
                {
                    switch (updateFieldType)
                    {
                        case UpdateFieldType.Guid:
                            {
                                if (isArray)
                                {
                                    var value = packet.ReadPackedGuid128(key + " + " + currentArrayIndex, index);
                                    dict.Add(i, new UpdateField(0));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadPackedGuid128(key, index);
                                    dict.Add(i, new UpdateField(0));
                                }
                                break;
                            }
                        case UpdateFieldType.Byte:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadByte(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadByte(key, index);
                                    dict.Add(i, new UpdateField(value));

                                    if (dynamicCounter)
                                        dynamicCounterStore.Add(value);
                                }
                                break;
                            }
                        case UpdateFieldType.Ushort:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadUInt16(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadUInt16(key, index);
                                    dict.Add(i, new UpdateField(value));

                                    if (dynamicCounter)
                                        dynamicCounterStore.Add(value);
                                }
                                break;
                            }
                        case UpdateFieldType.Short:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadInt16(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadInt16(key, index);
                                    dict.Add(i, new UpdateField(value));

                                    if (dynamicCounter)
                                        dynamicCounterStore.Add(value);
                                }
                                break;
                            }
                        case UpdateFieldType.Default:
                        case UpdateFieldType.Uint:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadUInt32(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadUInt32(key, index);
                                    dict.Add(i, new UpdateField(value));

                                    if (dynamicCounter)
                                        dynamicCounterStore.Add((int)value);
                                }
                                break;
                            }
                        case UpdateFieldType.Int:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadInt32(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadInt32(key, index);
                                    dict.Add(i, new UpdateField(value));

                                    if (dynamicCounter)
                                        dynamicCounterStore.Add(value);
                                }
                                break;
                            }
                        case UpdateFieldType.Float:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadSingle(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                    dict.Add(i, new UpdateField(packet.ReadSingle(key, index)));
                                break;
                            }
                        case UpdateFieldType.Ulong:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadUInt64(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadUInt64(key, index);
                                    dict.Add(i, new UpdateField(value));
                                }
                                break;
                            }
                        case UpdateFieldType.Long:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadInt64(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadInt64(key, index);
                                    dict.Add(i, new UpdateField(value));
                                }
                                break;
                            }
                        case UpdateFieldType.Bytes:
                            {
                                if (isArray)
                                {
                                    dict.Add(i, new UpdateField(packet.ReadUInt32(key + " + " + currentArrayIndex, index)));
                                    currentArrayPosition++;
                                }
                                else
                                {
                                    var value = packet.ReadUInt32();
                                    byte[] intBytes = BitConverter.GetBytes(value);
                                    packet.AddValue(key, intBytes[0] + "/" + intBytes[1] + "/" + intBytes[2] + "/" + intBytes[3], index);
                                    dict.Add(i, new UpdateField(value));
                                }
                                break;
                            }
                        case UpdateFieldType.DynamicByte:
                            {
                                if (!isArray)
                                {
                                    var count = dynamicCounterStore[0];
                                    var store = new List<UpdateField>();
                                    for (int k = 0; k < count; ++k)
                                    {
                                        var value = packet.ReadByte(count > 1 ? key + " + " + k : key, index);
                                        store.Add(new UpdateField(value));
                                    }
                                    dynDict.Add(i, store);
                                    dynamicCounterStore.RemoveAt(0);
                                }
                                break;
                            }
                        case UpdateFieldType.DynamicUshort:
                            {
                                if (!isArray)
                                {
                                    var count = dynamicCounterStore[0];
                                    var store = new List<UpdateField>();
                                    for (int k = 0; k < count; ++k)
                                    {
                                        var value = packet.ReadUInt16(count > 1 ? key + " + " + k : key, index);
                                        store.Add(new UpdateField(value));
                                    }
                                    dynDict.Add(i, store);
                                    dynamicCounterStore.RemoveAt(0);
                                }
                                break;
                            }
                        case UpdateFieldType.DynamicShort:
                            {
                                if (!isArray)
                                {
                                    var count = dynamicCounterStore[0];
                                    var store = new List<UpdateField>();
                                    for (int k = 0; k < count; ++k)
                                    {
                                        var value = packet.ReadInt16(count > 1 ? key + " + " + k : key, index);
                                        store.Add(new UpdateField(value));
                                    }
                                    dynDict.Add(i, store);
                                    dynamicCounterStore.RemoveAt(0);
                                }
                                break;
                            }
                        case UpdateFieldType.DynamicUint:
                            {
                                if (!isArray)
                                {
                                    var count = dynamicCounterStore[0];
                                    var store = new List<UpdateField>();
                                    for (int k = 0; k < count; ++k)
                                    {
                                        var value = packet.ReadUInt32(count > 1 ? key + " + " + k : key, index);
                                        store.Add(new UpdateField(value));
                                    }
                                    dynDict.Add(i, store);
                                    dynamicCounterStore.RemoveAt(0);
                                }
                                break;
                            }
                        case UpdateFieldType.DynamicInt:
                            {
                                if (!isArray)
                                {
                                    var count = dynamicCounterStore[0];
                                    var store = new List<UpdateField>();
                                    for (int k = 0; k < count; ++k)
                                    {
                                        var value = packet.ReadInt32(count > 1 ? key + " + " + k : key, index);
                                        store.Add(new UpdateField(value));
                                    }
                                    dynDict.Add(i, store);
                                    dynamicCounterStore.RemoveAt(0);
                                }
                                break;
                            }
                        case UpdateFieldType.Custom:
                            {
                                // TODO: add custom handling
                                //if (key == UnitField.UNIT_FIELD_FACTIONTEMPLATE.ToString())
                                //    packet.AddValue(key, value + $" ({ StoreGetters.GetName(StoreNameType.Faction, fieldData[0].Int32Value, false) })", index);
                                break;
                            }
                    }
                }
            }
        }

        private static void ReadCreateObjectBlock(Packet packet, WowGuid guid, uint map, object index)
        {
            ObjectType objType = ObjectTypeConverter.Convert(packet.ReadByteE<ObjectType801>("Object Type", index));
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("HeirFlags", index);
            WoWObject obj;
            switch (objType)
            {
                case ObjectType.Unit:
                    obj = new Unit();
                    break;
                case ObjectType.GameObject:
                    obj = new GameObject();
                    break;
                case ObjectType.Player:
                    obj = new Player();
                    break;
                case ObjectType.AreaTrigger:
                    obj = new SpellAreaTrigger();
                    break;
                case ObjectType.Conversation:
                    obj = new ConversationTemplate();
                    break;
                default:
                    obj = new WoWObject();
                    break;
            }

            var moves = ReadMovementUpdateBlock(packet, guid, obj, index);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                var dict = new Dictionary<int, UpdateField>();
                var dynDict = new Dictionary<int, List<UpdateField>>();

                ReadValuesCreateBlock(dict, dynDict, packet, objType, index);

                obj.UpdateFields = dict;
                obj.DynamicUpdateFields = dynDict;
            }
            else
            {
                var updates = CoreParsers.UpdateHandler.ReadValuesUpdateBlockOnCreate(packet, objType, index);
                var dynamicUpdates = CoreParsers.UpdateHandler.ReadDynamicValuesUpdateBlockOnCreate(packet, objType, index);

                obj.UpdateFields = updates;
                obj.DynamicUpdateFields = dynamicUpdates;
            }

            obj.Type = objType;
            obj.Movement = moves;
            obj.Map = map;
            obj.Area = CoreParsers.WorldStateHandler.CurrentAreaId;
            obj.Zone = CoreParsers.WorldStateHandler.CurrentZoneId;
            obj.PhaseMask = (uint)CoreParsers.MovementHandler.CurrentPhaseMask;
            obj.Phases = new HashSet<ushort>(MovementHandler.ActivePhases.Keys);
            obj.DifficultyID = CoreParsers.MovementHandler.CurrentDifficultyID;

            // If this is the second time we see the same object (same guid,
            // same position) update its phasemask
            if (Storage.Objects.ContainsKey(guid))
            {
                var existObj = Storage.Objects[guid].Item1;
                CoreParsers.UpdateHandler.ProcessExistingObject(ref existObj, obj, guid); // can't do "ref Storage.Objects[guid].Item1 directly
            }
            else
                Storage.Objects.Add(guid, obj, packet.TimeSpan);

            if (guid.HasEntry() && (objType == ObjectType.Unit || objType == ObjectType.GameObject))
                packet.AddSniffData(Utilities.ObjectTypeToStore(objType), (int)guid.GetEntry(), "SPAWN");
        }

        public static void ReadTransportData(MovementInfo moveInfo, WowGuid guid, Packet packet, object index)
        {
            packet.ResetBitReader();
            moveInfo.TransportGuid = packet.ReadPackedGuid128("TransportGUID", index);
            moveInfo.TransportOffset = packet.ReadVector4("TransportPosition", index);
            var seat = packet.ReadByte("VehicleSeatIndex", index);
            packet.ReadUInt32("MoveTime", index);

            var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", index);
            var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", index);

            if (hasPrevMoveTime)
                packet.ReadUInt32("PrevMoveTime", index);

            if (hasVehicleRecID)
                packet.ReadInt32("VehicleRecID", index);

            if (moveInfo.TransportGuid.HasEntry() && moveInfo.TransportGuid.GetHighType() == HighGuidType.Vehicle &&
                guid.HasEntry() && guid.GetHighType() == HighGuidType.Creature)
            {
                VehicleTemplateAccessory vehicleAccessory = new VehicleTemplateAccessory
                {
                    Entry = moveInfo.TransportGuid.GetEntry(),
                    AccessoryEntry = guid.GetEntry(),
                    SeatId = seat
                };
                Storage.VehicleTemplateAccessories.Add(vehicleAccessory, packet.TimeSpan);
            }
        }

        private static MovementInfo ReadMovementUpdateBlock(Packet packet, WowGuid guid, WoWObject obj, object index)
        {
            var moveInfo = new MovementInfo();

            packet.ResetBitReader();

            packet.ReadBit("NoBirthAnim", index);
            packet.ReadBit("EnablePortals", index);
            packet.ReadBit("PlayHoverAnim", index);

            var hasMovementUpdate = packet.ReadBit("HasMovementUpdate", index);
            var hasMovementTransport = packet.ReadBit("HasMovementTransport", index);
            var hasStationaryPosition = packet.ReadBit("Stationary", index);
            var hasCombatVictim = packet.ReadBit("HasCombatVictim", index);
            var hasServerTime = packet.ReadBit("HasServerTime", index);
            var hasVehicleCreate = packet.ReadBit("HasVehicleCreate", index);
            var hasAnimKitCreate = packet.ReadBit("HasAnimKitCreate", index);
            var hasRotation = packet.ReadBit("HasRotation", index);
            var hasAreaTrigger = packet.ReadBit("HasAreaTrigger", index);
            var hasGameObject = packet.ReadBit("HasGameObject", index);
            var hasSmoothPhasing = packet.ReadBit("HasSmoothPhasing", index);

            packet.ReadBit("ThisIsYou", index);

            var sceneObjCreate = packet.ReadBit("SceneObjCreate", index);
            var playerCreateData = packet.ReadBit("HasPlayerCreateData", index);
            var hasConversation = packet.ReadBit("HasConversation", index);

            if (hasMovementUpdate)
            {
                packet.ResetBitReader();
                packet.ReadPackedGuid128("MoverGUID", index);

                packet.ReadUInt32("MoveTime", index);
                moveInfo.Position = packet.ReadVector3("Position", index);
                moveInfo.Orientation = packet.ReadSingle("Orientation", index);

                packet.ReadSingle("Pitch", index);
                packet.ReadSingle("StepUpStartElevation", index);

                var removeForcesIDsCount = packet.ReadInt32();
                packet.ReadInt32("MoveIndex", index);

                for (var i = 0; i < removeForcesIDsCount; i++)
                    packet.ReadPackedGuid128("RemoveForcesIDs", index, i);

                moveInfo.Flags = (MovementFlag)packet.ReadBitsE<V6_0_2_19033.Enums.MovementFlag>("Movement Flags", 30, index);
                moveInfo.FlagsExtra = (MovementFlagExtra)packet.ReadBitsE<Enums.MovementFlags2>("Extra Movement Flags", 18, index);

                var hasTransport = packet.ReadBit("Has Transport Data", index);
                var hasFall = packet.ReadBit("Has Fall Data", index);
                packet.ReadBit("HasSpline", index);
                packet.ReadBit("HeightChangeFailed", index);
                packet.ReadBit("RemoteTimeValid", index);

                if (hasTransport)
                    ReadTransportData(moveInfo, guid, packet, index);

                if (hasFall)
                {
                    packet.ResetBitReader();
                    packet.ReadUInt32("Fall Time", index);
                    packet.ReadSingle("JumpVelocity", index);

                    var hasFallDirection = packet.ReadBit("Has Fall Direction", index);
                    if (hasFallDirection)
                    {
                        packet.ReadVector2("Fall", index);
                        packet.ReadSingle("Horizontal Speed", index);
                    }
                }

                moveInfo.WalkSpeed = packet.ReadSingle("WalkSpeed", index) / 2.5f;
                moveInfo.RunSpeed = packet.ReadSingle("RunSpeed", index) / 7.0f;
                packet.ReadSingle("RunBackSpeed", index);
                packet.ReadSingle("SwimSpeed", index);
                packet.ReadSingle("SwimBackSpeed", index);
                packet.ReadSingle("FlightSpeed", index);
                packet.ReadSingle("FlightBackSpeed", index);
                packet.ReadSingle("TurnRate", index);
                packet.ReadSingle("PitchRate", index);

                var movementForceCount = packet.ReadUInt32("MovementForceCount", index);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                    packet.ReadSingle("UnkFloat", index);

                packet.ResetBitReader();

                moveInfo.HasSplineData = packet.ReadBit("HasMovementSpline", index);

                for (var i = 0; i < movementForceCount; ++i)
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("Id", index);
                    packet.ReadVector3("Origin", index);
                    packet.ReadVector3("Direction", index);
                    packet.ReadUInt32("TransportID", index);
                    packet.ReadSingle("Magnitude", index);
                    packet.ReadBits("Type", 2, index);
                }

                if (moveInfo.HasSplineData)
                {
                    packet.ResetBitReader();
                    packet.ReadInt32("ID", index);
                    packet.ReadVector3("Destination", index);

                    var hasMovementSplineMove = packet.ReadBit("MovementSplineMove", index);
                    if (hasMovementSplineMove)
                    {
                        packet.ResetBitReader();

                        packet.ReadUInt32E<SplineFlag>("SplineFlags", index);
                        packet.ReadInt32("Elapsed", index);
                        packet.ReadUInt32("Duration", index);
                        packet.ReadSingle("DurationModifier", index);
                        packet.ReadSingle("NextDurationModifier", index);

                        var face = packet.ReadBits("Face", 2, index);
                        var hasSpecialTime = packet.ReadBit("HasSpecialTime", index);

                        var pointsCount = packet.ReadBits("PointsCount", 16, index);

                        packet.ReadBitsE<SplineMode>("Mode", 2, index);

                        var hasSplineFilterKey = packet.ReadBit("HasSplineFilterKey", index);
                        var hasSpellEffectExtraData = packet.ReadBit("HasSpellEffectExtraData", index);
                        var hasJumpExtraData = packet.ReadBit("HasJumpExtraData", index);

                        if (hasSplineFilterKey)
                        {
                            packet.ResetBitReader();
                            var filterKeysCount = packet.ReadUInt32("FilterKeysCount", index);
                            for (var i = 0; i < filterKeysCount; ++i)
                            {
                                packet.ReadSingle("In", index, i);
                                packet.ReadSingle("Out", index, i);
                            }

                            packet.ReadBits("FilterFlags", 2, index);
                        }

                        switch (face)
                        {
                            case 1:
                                packet.ReadVector3("FaceSpot", index);
                                break;
                            case 2:
                                packet.ReadPackedGuid128("FaceGUID", index);
                                break;
                            case 3:
                                packet.ReadSingle("FaceDirection", index);
                                break;
                            default:
                                break;
                        }

                        if (hasSpecialTime)
                            packet.ReadUInt32("SpecialTime", index);

                        for (var i = 0; i < pointsCount; ++i)
                            packet.ReadVector3("Points", index, i);

                        if (hasSpellEffectExtraData)
                            MovementHandler.ReadMonsterSplineSpellEffectExtraData(packet, index);

                        if (hasJumpExtraData)
                            MovementHandler.ReadMonsterSplineJumpExtraData(packet, index);
                    }
                }
            }

            var pauseTimesCount = packet.ReadUInt32("PauseTimesCount", index);

            if (hasStationaryPosition)
            {
                moveInfo.Position = packet.ReadVector3();
                moveInfo.Orientation = packet.ReadSingle();

                packet.AddValue("Stationary Position", moveInfo.Position, index);
                packet.AddValue("Stationary Orientation", moveInfo.Orientation, index);
            }

            if (hasCombatVictim)
                packet.ReadPackedGuid128("CombatVictim Guid", index);

            if (hasServerTime)
                packet.ReadUInt32("ServerTime", index);

            if (hasVehicleCreate)
            {
                moveInfo.VehicleId = (uint)packet.ReadInt32("RecID", index);
                packet.ReadSingle("InitialRawFacing", index);
            }

            if (hasAnimKitCreate)
            {
                packet.ReadUInt16("AiID", index);
                packet.ReadUInt16("MovementID", index);
                packet.ReadUInt16("MeleeID", index);
            }

            if (hasRotation)
                moveInfo.Rotation = packet.ReadPackedQuaternion("GameObject Rotation", index);

            for (var i = 0; i < pauseTimesCount; ++i)
                packet.ReadUInt32("PauseTimes", index, i);

            if (hasMovementTransport)
                ReadTransportData(moveInfo, guid, packet, index);

            if (hasAreaTrigger && obj is SpellAreaTrigger)
            {
                AreaTriggerTemplate areaTriggerTemplate = new AreaTriggerTemplate
                {
                    Id = guid.GetEntry()
                };

                SpellAreaTrigger spellAreaTrigger = (SpellAreaTrigger)obj;
                spellAreaTrigger.AreaTriggerId = guid.GetEntry();

                packet.ResetBitReader();

                // CliAreaTrigger
                packet.ReadUInt32("ElapsedMs", index);

                packet.ReadVector3("RollPitchYaw1", index);

                areaTriggerTemplate.Flags   = 0;

                if (packet.ReadBit("HasAbsoluteOrientation", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAbsoluteOrientation;

                if (packet.ReadBit("HasDynamicShape", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasDynamicShape;

                if (packet.ReadBit("HasAttached", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAttached;

                if (packet.ReadBit("HasFaceMovementDir", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FaceMovementDirection;

                if (packet.ReadBit("HasFollowsTerrain", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.FollowsTerrain;

                if (packet.ReadBit("Unk bit WoD62x", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk1;

                if (packet.ReadBit("HasTargetRollPitchYaw", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasTargetRollPitchYaw;

                bool hasScaleCurveID = packet.ReadBit("HasScaleCurveID", index);
                bool hasMorphCurveID = packet.ReadBit("HasMorphCurveID", index);
                bool hasFacingCurveID = packet.ReadBit("HasFacingCurveID", index);
                bool hasMoveCurveID = packet.ReadBit("HasMoveCurveID", index);

                if (packet.ReadBit("HasAnimID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimId;

                if (packet.ReadBit("unkbit50", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.Unk3;

                if (packet.ReadBit("HasAnimKitID", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasAnimKitId;

                bool hasUnk801 = packet.ReadBit("unkbit801", index);

                if (packet.ReadBit("HasAreaTriggerSphere", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Sphere;

                if (packet.ReadBit("HasAreaTriggerBox", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Box;

                if (packet.ReadBit("HasAreaTriggerPolygon", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Polygon;

                if (packet.ReadBit("HasAreaTriggerCylinder", index))
                    areaTriggerTemplate.Type = (byte)AreaTriggerType.Cylinder;

                bool hasAreaTriggerSpline = packet.ReadBit("HasAreaTriggerSpline", index);

                if (packet.ReadBit("HasAreaTriggerCircularMovement", index))
                    areaTriggerTemplate.Flags |= (uint)AreaTriggerFlags.HasCircularMovement;

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.Unk3) != 0)
                    packet.ReadBit();

                if (hasAreaTriggerSpline)
                    AreaTriggerHandler.ReadAreaTriggerSpline(packet, index);

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasTargetRollPitchYaw) != 0)
                    packet.ReadVector3("TargetRollPitchYaw", index);

                if (hasScaleCurveID)
                    spellAreaTrigger.ScaleCurveId = (int)packet.ReadUInt32("ScaleCurveID", index);

                if (hasMorphCurveID)
                    spellAreaTrigger.MorphCurveId = (int)packet.ReadUInt32("MorphCurveID", index);

                if (hasFacingCurveID)
                    spellAreaTrigger.FacingCurveId = (int)packet.ReadUInt32("FacingCurveID", index);

                if (hasMoveCurveID)
                    spellAreaTrigger.MoveCurveId = (int)packet.ReadUInt32("MoveCurveID", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimId) != 0)
                    spellAreaTrigger.AnimId = packet.ReadInt32("AnimId", index);

                if ((areaTriggerTemplate.Flags & (int)AreaTriggerFlags.HasAnimKitId) != 0)
                    spellAreaTrigger.AnimKitId = packet.ReadInt32("AnimKitId", index);

                if (hasUnk801)
                    packet.ReadUInt32("Unk801", index);

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Sphere)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Box)
                {
                    Vector3 Extents = packet.ReadVector3("Extents", index);
                    Vector3 ExtentsTarget = packet.ReadVector3("ExtentsTarget", index);

                    areaTriggerTemplate.Data[0] = Extents.X;
                    areaTriggerTemplate.Data[1] = Extents.Y;
                    areaTriggerTemplate.Data[2] = Extents.Z;

                    areaTriggerTemplate.Data[3] = ExtentsTarget.X;
                    areaTriggerTemplate.Data[4] = ExtentsTarget.Y;
                    areaTriggerTemplate.Data[5] = ExtentsTarget.Z;
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Polygon)
                {
                    var verticesCount = packet.ReadUInt32("VerticesCount", index);
                    var verticesTargetCount = packet.ReadUInt32("VerticesTargetCount", index);

                    List<AreaTriggerTemplateVertices> verticesList = new List<AreaTriggerTemplateVertices>();

                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("HeightTarget", index);

                    for (uint i = 0; i < verticesCount; ++i)
                    {
                        AreaTriggerTemplateVertices areaTriggerTemplateVertices = new AreaTriggerTemplateVertices
                        {
                            AreaTriggerId = guid.GetEntry(),
                            Idx = i
                        };

                        Vector2 vertices = packet.ReadVector2("Vertices", index, i);

                        areaTriggerTemplateVertices.VerticeX = vertices.X;
                        areaTriggerTemplateVertices.VerticeY = vertices.Y;

                        verticesList.Add(areaTriggerTemplateVertices);
                    }

                    for (var i = 0; i < verticesTargetCount; ++i)
                    {
                        Vector2 verticesTarget = packet.ReadVector2("VerticesTarget", index, i);

                        verticesList[i].VerticeTargetX = verticesTarget.X;
                        verticesList[i].VerticeTargetY = verticesTarget.Y;
                    }

                    foreach (AreaTriggerTemplateVertices vertice in verticesList)
                        Storage.AreaTriggerTemplatesVertices.Add(vertice);
                }

                if (areaTriggerTemplate.Type == (byte)AreaTriggerType.Cylinder)
                {
                    areaTriggerTemplate.Data[0] = packet.ReadSingle("Radius", index);
                    areaTriggerTemplate.Data[1] = packet.ReadSingle("RadiusTarget", index);
                    areaTriggerTemplate.Data[2] = packet.ReadSingle("Height", index);
                    areaTriggerTemplate.Data[3] = packet.ReadSingle("HeightTarget", index);
                    areaTriggerTemplate.Data[4] = packet.ReadSingle("LocationZOffset", index);
                    areaTriggerTemplate.Data[5] = packet.ReadSingle("LocationZOffsetTarget", index);
                }

                if ((areaTriggerTemplate.Flags & (uint)AreaTriggerFlags.HasCircularMovement) != 0)
                {
                    packet.ResetBitReader();
                    var hasPathTarget = packet.ReadBit("HasPathTarget");
                    var hasCenter = packet.ReadBit("HasCenter", index);
                    packet.ReadBit("CounterClockwise", index);
                    packet.ReadBit("CanLoop", index);

                    packet.ReadUInt32("TimeToTarget", index);
                    packet.ReadInt32("ElapsedTimeForMovement", index);
                    packet.ReadUInt32("StartDelay", index);
                    packet.ReadSingle("Radius", index);
                    packet.ReadSingle("BlendFromRadius", index);
                    packet.ReadSingle("InitialAngel", index);
                    packet.ReadSingle("ZOffset", index);

                    if (hasPathTarget)
                        packet.ReadPackedGuid128("PathTarget", index);

                    if (hasCenter)
                        packet.ReadVector3("Center", index);
                }

                Storage.AreaTriggerTemplates.Add(areaTriggerTemplate);
            }

            if (hasGameObject)
            {
                packet.ResetBitReader();
                packet.ReadUInt32("WorldEffectID", index);

                var bit8 = packet.ReadBit("bit8", index);
                if (bit8)
                    packet.ReadUInt32("Int1", index);
            }

            if (hasSmoothPhasing)
            {
                packet.ResetBitReader();
                packet.ReadBit("ReplaceActive", index);
                var replaceObject = packet.ReadBit();
                if (replaceObject)
                    packet.ReadPackedGuid128("ReplaceObject", index);
            }

            if (sceneObjCreate)
            {
                packet.ResetBitReader();

                var hasSceneLocalScriptData = packet.ReadBit("HasSceneLocalScriptData", index);
                var petBattleFullUpdate = packet.ReadBit("HasPetBattleFullUpdate", index);

                if (hasSceneLocalScriptData)
                {
                    packet.ResetBitReader();
                    var dataLength = packet.ReadBits(7);
                    packet.ReadWoWString("Data", dataLength, index);
                }

                if (petBattleFullUpdate)
                    V6_0_2_19033.Parsers.BattlePetHandler.ReadPetBattleFullUpdate(packet, index);
            }

            if (playerCreateData)
            {
                packet.ResetBitReader();
                var hasSceneInstanceIDs = packet.ReadBit("ScenePendingInstances", index);
                var hasRuneState = packet.ReadBit("Runes", index);

                if (hasSceneInstanceIDs)
                {
                    var sceneInstanceIDs = packet.ReadUInt32("SceneInstanceIDsCount");
                    for (var i = 0; i < sceneInstanceIDs; ++i)
                        packet.ReadInt32("SceneInstanceIDs", index, i);
                }

                if (hasRuneState)
                {
                    packet.ReadByte("RechargingRuneMask", index);
                    packet.ReadByte("UsableRuneMask", index);
                    var runeCount = packet.ReadUInt32();
                    for (var i = 0; i < runeCount; ++i)
                        packet.ReadByte("RuneCooldown", index, i);
                }
            }

            if (hasConversation)
            {
                packet.ResetBitReader();
                if (packet.ReadBit("HasTextureKitID", index))
                    (obj as ConversationTemplate).TextureKitId = packet.ReadUInt32("TextureKitID", index);
            }

            return moveInfo;
        }
    }
}
