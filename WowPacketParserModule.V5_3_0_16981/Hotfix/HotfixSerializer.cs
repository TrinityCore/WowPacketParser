using Sigil;
using System;
using System.Diagnostics;
using System.Reflection;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;

namespace WowPacketParserModule.V5_3_0_16981.Hotfix
{
    [HotfixSerializer]
    public class HotfixSerializer<T> : IHotfixSerializer<T> where T : class, new()
    {
        public override void GenerateDeserializer()
        {
            try
            {
                var deserializationEmitter = Emit<Func<Packet, T>>.NewDynamicMethod();
                var deserializationResultLocal = deserializationEmitter.DeclareLocal<T>();
                deserializationEmitter.NewObject<T>();
                deserializationEmitter.StoreLocal(deserializationResultLocal);

                foreach (var propInfo in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (propInfo.GetGetMethod() == null || propInfo.GetSetMethod() == null || !ShouldRead(propInfo))
                        continue;

                    var propType = propInfo.PropertyType;
                    var isArray = propType.IsArray;

                    if (!isArray)
                    {
                        var typeCode = Type.GetTypeCode(propType);

                        if (typeCode == TypeCode.String)
                        {
                            var conditionLabel = deserializationEmitter.DefineLabel();
                            deserializationEmitter.LoadArgument(0);
                            deserializationEmitter.CallVirtual(typeof(Packet).GetMethod("ReadInt16", Type.EmptyTypes));
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.BranchIfLessOrEqual(conditionLabel);
                            deserializationEmitter.LoadLocal(deserializationResultLocal);
                            deserializationEmitter.LoadArgument(0);
                            deserializationEmitter.LoadConstant(propInfo.Name);
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.NewArray<object>();
                            deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                            deserializationEmitter.Call(propInfo.GetSetMethod());
                            deserializationEmitter.MarkLabel(conditionLabel);
                        }
                        else
                        {
                            deserializationEmitter.LoadLocal(deserializationResultLocal);
                            deserializationEmitter.LoadArgument(0);
                            deserializationEmitter.LoadConstant(propInfo.Name);
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.NewArray<object>();
                            deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                            deserializationEmitter.Call(propInfo.GetSetMethod());
                        }
                    }
                    else
                    {
                        var hotfixAttr = propInfo.GetCustomAttribute<HotfixArrayAttribute>();
                        Trace.Assert(hotfixAttr != null);
                        var typeCode = Type.GetTypeCode(propType.GetElementType());

                        deserializationEmitter.LoadLocal(deserializationResultLocal);
                        deserializationEmitter.LoadConstant(hotfixAttr.Size);
                        deserializationEmitter.NewArray(propType.GetElementType());
                        deserializationEmitter.CallVirtual(propInfo.GetSetMethod());

                        var loopBodyLabel = deserializationEmitter.DefineLabel();
                        var loopConditionLabel = deserializationEmitter.DefineLabel();

                        using (var iterationLocal = deserializationEmitter.DeclareLocal<int>())
                        {
                            // for (var i = 0; ...; ...)
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.StoreLocal(iterationLocal);

                            deserializationEmitter.Branch(loopConditionLabel);
                            deserializationEmitter.MarkLabel(loopBodyLabel);
                            if (typeCode == TypeCode.String)
                            {
                                var conditionLabel = deserializationEmitter.DefineLabel();

                                // if (packet.ReadInt16() > 0)
                                deserializationEmitter.LoadArgument(0); // Packet
                                deserializationEmitter.CallVirtual(typeof(Packet).GetMethod("ReadInt16", Type.EmptyTypes));
                                deserializationEmitter.LoadConstant(0);
                                deserializationEmitter.CompareGreaterThan();
                                deserializationEmitter.BranchIfFalse(conditionLabel);

                                // instance.Property[i] = packet.<Reader>("PropertyName", i);
                                deserializationEmitter.LoadLocal(deserializationResultLocal);
                                deserializationEmitter.CallVirtual(propInfo.GetGetMethod());
                                deserializationEmitter.LoadLocal(iterationLocal);
                                deserializationEmitter.LoadArgument(0); // Packet
                                deserializationEmitter.LoadConstant(propInfo.Name);
                                deserializationEmitter.LoadConstant(1);
                                deserializationEmitter.NewArray<object>();
                                deserializationEmitter.Duplicate();
                                deserializationEmitter.LoadConstant(0);
                                deserializationEmitter.LoadLocal(iterationLocal);
                                deserializationEmitter.Box<int>();
                                deserializationEmitter.StoreElement<object>();
                                deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                                deserializationEmitter.StoreElement<string>();

                                deserializationEmitter.MarkLabel(conditionLabel);
                            }
                            else
                            {
                                // instance.Property[i] = packet.<Reader>("PropertyName", i);
                                deserializationEmitter.LoadLocal(deserializationResultLocal);
                                deserializationEmitter.CallVirtual(propInfo.GetGetMethod());
                                deserializationEmitter.LoadLocal(iterationLocal);
                                deserializationEmitter.LoadArgument(0);
                                deserializationEmitter.LoadConstant(propInfo.Name);
                                deserializationEmitter.LoadConstant(1);
                                deserializationEmitter.NewArray<object>();
                                deserializationEmitter.Duplicate();
                                deserializationEmitter.LoadConstant(0);
                                deserializationEmitter.LoadLocal(iterationLocal);
                                deserializationEmitter.Box<int>();
                                deserializationEmitter.StoreElement<object>();
                                deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                                deserializationEmitter.StoreElement(propType.GetElementType());
                            }

                            // for (...; ...; i += 1)
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.LoadConstant(1);
                            deserializationEmitter.Add();
                            deserializationEmitter.StoreLocal(iterationLocal);
                            deserializationEmitter.MarkLabel(loopConditionLabel);
                            // for (...; i < arraySize; ...)
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.LoadConstant(hotfixAttr.Size);
                            deserializationEmitter.CompareLessThan();
                            deserializationEmitter.BranchIfTrue(loopBodyLabel);
                        }
                    }
                }

                // return instance;
                deserializationEmitter.LoadLocal(deserializationResultLocal);
                deserializationEmitter.Return();

                _deserializer = deserializationEmitter.CreateDelegate();
            }
            catch (SigilVerificationException sve)
            {
                Console.WriteLine(sve);
            }
        }
    }
}
