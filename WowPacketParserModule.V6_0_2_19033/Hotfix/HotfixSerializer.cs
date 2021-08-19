using Sigil;
using System;
using System.Reflection;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixSerializer]
    public class HotfixSerializer<T> : IHotfixSerializer<T> where T : class, new()
    {
        public HotfixSerializer()
        {
            // Override static data
            // Not exactly liking this, buuuuut .... eh
            _binaryReaders[TypeCode.String] = typeof (Packet).GetMethod("ReadWoWString", new[] { typeof(string), typeof(int), typeof(object[]) });
        } 

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

                    deserializationEmitter.LoadLocal(deserializationResultLocal);

                    if (!isArray)
                    {
                        var typeCode = Type.GetTypeCode(propType);

                        deserializationEmitter.LoadArgument(0);
                        deserializationEmitter.LoadConstant(propInfo.Name);

                        if (typeCode == TypeCode.String)
                        {
                            deserializationEmitter.LoadArgument(0);
                            deserializationEmitter.CallVirtual(typeof (Packet).GetMethod("ReadInt16", Type.EmptyTypes));
                        }
                        deserializationEmitter.LoadConstant(0);
                        deserializationEmitter.NewArray<object>(); // object[0]
                        deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                        deserializationEmitter.Call(propInfo.GetSetMethod());
                    }
                    else
                    {
                        var hotfixAttr = propInfo.GetCustomAttribute<HotfixArrayAttribute>();
                        var typeCode = Type.GetTypeCode(propType.GetElementType());

                        deserializationEmitter.LoadConstant(hotfixAttr.Size);
                        deserializationEmitter.NewArray(propType.GetElementType());
                        deserializationEmitter.CallVirtual(propInfo.GetSetMethod());

                        var loopBodyLabel = deserializationEmitter.DefineLabel();
                        var loopConditionLabel = deserializationEmitter.DefineLabel();

                        using (var iterationLocal = deserializationEmitter.DeclareLocal<int>())
                        {
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.StoreLocal(iterationLocal);
                            deserializationEmitter.Branch(loopConditionLabel);
                            deserializationEmitter.MarkLabel(loopBodyLabel);
                            deserializationEmitter.LoadLocal(deserializationResultLocal);
                            deserializationEmitter.CallVirtual(propInfo.GetGetMethod());
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.LoadArgument(0);
                            deserializationEmitter.LoadConstant(propInfo.Name);
                            if (typeCode == TypeCode.String)
                            {
                                deserializationEmitter.LoadArgument(0);
                                deserializationEmitter.CallVirtual(typeof(Packet).GetMethod("ReadInt16", Type.EmptyTypes));
                            }
                            deserializationEmitter.LoadConstant(1);
                            deserializationEmitter.NewArray<object>();
                            deserializationEmitter.Duplicate();
                            deserializationEmitter.LoadConstant(0);
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.Box<int>();
                            deserializationEmitter.StoreElement<object>();
                            deserializationEmitter.CallVirtual(_binaryReaders[typeCode]);
                            deserializationEmitter.StoreElement(propType.GetElementType());
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.LoadConstant(1);
                            deserializationEmitter.Add();
                            deserializationEmitter.StoreLocal(iterationLocal);
                            deserializationEmitter.MarkLabel(loopConditionLabel);
                            deserializationEmitter.LoadLocal(iterationLocal);
                            deserializationEmitter.LoadConstant(hotfixAttr.Size);
                            deserializationEmitter.CompareLessThan();
                            deserializationEmitter.BranchIfTrue(loopBodyLabel);
                        }
                    }
                }

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
