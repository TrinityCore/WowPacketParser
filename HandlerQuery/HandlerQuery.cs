using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mono.Reflection;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace HandlerQuery
{
    static class HandlerQuery
    {
        static HandlerQuery()
        {
            var stopwatch = Stopwatch.StartNew();

            var asm = Assembly.LoadFrom("WowPacketParser.exe");
            var types = asm.GetTypes();
            var methods = (from type in types
                           where type.IsAbstract
                           where type.IsPublic
                           from method in type.GetMethods()
                           where method.IsPublic
                           let attrs = (ParserAttribute[])method.GetCustomAttributes(typeof(ParserAttribute), false)
                           where attrs.Length > 0
                           let parms = method.GetParameters()
                           where parms.Length > 0
                           where parms[0].ParameterType == typeof(Packet)
                           select method).ToList();

            _instructions = new Dictionary<MethodInfo, List<Instruction>>(methods.Count);
            foreach (var methodInfo in methods)
            {
                try
                {
                    _instructions.Add(methodInfo, new List<Instruction>(methodInfo.GetInstructions()));
                }
                catch (ArgumentException) { }
            }

            FilterInstructions();
            ConvertToReads();

            stopwatch.Stop();
            Console.WriteLine("Read dictionary built (it took: {0}).", stopwatch.Elapsed);
        }

        static private readonly Dictionary<MethodInfo, List<Instruction>> _instructions;
        static private Dictionary<MethodInfo, List<ReadMethods>> _readMethods;

        static public Dictionary<MethodInfo, List<ReadMethods>> GetReadDictionary()
        {
            return _readMethods;
        }

        static private void FilterInstructions()
        {
            foreach (var instruction in _instructions)
                instruction.Value.RemoveAll(inst =>
                    inst.OpCode.Value != 111 ||
                    !inst.Operand.ToString().Contains("Read"));
        }

        static private void ConvertToReads()
        {
            _readMethods = new Dictionary<MethodInfo, List<ReadMethods>>();

            foreach (var instructions in _instructions)
            {
                var reads = new List<ReadMethods>();
                foreach (var instruction in instructions.Value)
                {
                    var method = instruction.Operand as MethodInfo;

                    if (method != null)
                    {
                        var str = method.Name.Remove(0, 4); // Remove "Read"
                        ReadMethods type;

                        switch (str)
                        {
                            // unsupported:
                            // ReadBit, ReadBits, ReadBytes,

                            case "ead": // from CanRead
                            case "ToEnd": // from ReadToEnd
                            case "Bit":
                            case "Bits":
                            case "Bytes":
                            {
                                type = ReadMethods.Ignore;
                                break;
                            }
                            case "EntryWithName":
                            {
                                var typeArgs = method.GetGenericArguments();
                                if (typeArgs.Length == 1)
                                    type = SimpleTypeToReadMethod(typeArgs[0].Name);
                                else
                                {
                                    type = ReadMethods.Unknown;
                                    Console.WriteLine("Method {0} got an unexpected number of generic types", method);
                                }
                                break;
                            }
                            case "Value":
                            case "Enum":
                            {
                                // FIXME: 2nd parameter of ReadEnum (TypeCode)
                                // and 1st parameter of ReadValue defines what
                                // to read. Find out a way to know what it is.
                                // for now assuming it's an int32.
                                type = ReadMethods.ReadInt32;
                                break;
                            }
                            case "Vector2":
                            {
                                type = ReadMethods.Ignore;
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                break;
                            }
                            case "Vector3":
                            {
                                type = ReadMethods.Ignore;
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                break;
                            }
                            case "Vector4":
                            {
                                type = ReadMethods.Ignore;
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                reads.Add(ReadMethods.ReadFloat);
                                break;
                            }
                            default:
                            {
                                type = SimpleTypeToReadMethod(str);
                                break;
                            }
                        }

                        if (type == ReadMethods.Unknown)
                            Console.WriteLine("ReadMethod not found for method {0}", method);
                        else if (type != ReadMethods.Ignore)
                            reads.Add(type);
                    }
                    else
                    {
                        Console.WriteLine("Instruction's operand ({0}) is not a MethodInfo", instruction.Operand);
                    }
                }

                _readMethods.Add(instructions.Key, reads);
            }
        }

        static private ReadMethods SimpleTypeToReadMethod(string type)
        {
            switch (type)
            {
                case "Boolean":
                case "Byte":
                case "SByte":
                    return ReadMethods.ReadInt8;
                case "Int16":
                case "UInt16":
                    return ReadMethods.ReadInt16;
                case "Int32":
                case "UInt32":
                case "Entry":
                case "LfgEntry":
                case "Time":
                case "PackedTime":
                case "PackedVector3":
                    return ReadMethods.ReadInt32;
                case "Int64":
                case "UInt64":
                    return ReadMethods.ReadInt64;
                case "Single":
                    return ReadMethods.ReadFloat;
                case "Guid":
                    return ReadMethods.ReadGuid;
                case "PackedGuid":
                    return ReadMethods.ReadPackedGuid;
                case "IPAddress":
                    return ReadMethods.ReadIP;
                case "CString":
                case "WoWString":
                case "Chars":
                    return ReadMethods.ReadString;
                default:
                    Console.WriteLine("ReadMethod not found for type {0}", type);
                    return ReadMethods.Ignore;
            }
        }

    }
}
