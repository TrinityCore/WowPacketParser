using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Enums.Version.V6_0_3_19103;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Tests
{
    [TestFixture]
    public class OpcodeTest
    {
        [Test, Ignore("Ignore TestHasHandler")]
        public void TestHasHandler()
        {
            var opcodes = Utilities.GetValues<Opcode>();
            var versions = Utilities.GetValues<ClientVersionBuild>();

            var usedOpcodes = opcodes.ToDictionary(opcode => opcode, opcode => false);
            usedOpcodes[Opcode.NULL_OPCODE] = true; // ignore

            foreach (var version in versions)
            {
                try
                {
                    var asm = Assembly.LoadFrom(string.Format(AppDomain.CurrentDomain.BaseDirectory + "/" + "WowPacketParserModule.{0}.dll", version));
                    var dict = Handler.LoadHandlers(asm, version);

                    foreach (var action in dict)
                    {
                        usedOpcodes[action.Key.Value] = true;
                    }
                }
                catch (FileNotFoundException)
                {
                    // do nothing, go to next possible assembly
                }
            }

            var defDict = Handler.LoadDefaultHandlers();
            foreach (var action in defDict)
            {
                usedOpcodes[action.Key.Value] = true;
            }

            var allUsed = usedOpcodes.All(pair => pair.Value);

            if (!allUsed)
            {
                foreach (var usedOpcode in usedOpcodes.Where(usedOpcode => !usedOpcode.Value))
                {
                    Console.WriteLine("Warning: {0} is not used in any handler.", usedOpcode.Key);
                }
            }

            Assert.IsTrue(allUsed, "Found unused opcodes defined.");
        }

        [Test, Ignore("Ignore TestHasValue")]
        public void TestHasValue()
        {
            var opcodes = Utilities.GetValues<Opcode>();

            var usedOpcodes = opcodes.ToDictionary(opcode => opcode, opcode => false);
            usedOpcodes[Opcode.NULL_OPCODE] = true; // ignore

            var versions = Utilities.GetValues<ClientVersionBuild>();
            var directions = Utilities.GetValues<Direction>().ToList();

            foreach (var version in versions)
            {
                foreach (var direction in directions)
                {
                    var dict = Opcodes.GetOpcodeDictionary(version, direction);
                    foreach (var pair in dict)
                    {
                        usedOpcodes[pair.Key] = true;
                    }
                }
            }

            var allUsed = usedOpcodes.All(pair => pair.Value);

            if (!allUsed)
            {
                foreach (var usedOpcode in usedOpcodes.Where(usedOpcode => !usedOpcode.Value))
                {
                    Console.WriteLine("Warning: {0} does not have any id in any version.", usedOpcode.Key);
                }
            }

            Assert.IsTrue(allUsed, "Found unused opcodes defined.");
        }


        [Test, Ignore("Ignore TestHasHandler6x")]
        public void TestHasHandler6x()
        {
            var clientOpcodes = Opcodes_6_0_3.Opcodes(Direction.ClientToServer).Select(pair => pair.Key);
            var serverOpcodes = Opcodes_6_0_3.Opcodes(Direction.ServerToClient).Select(pair => pair.Key);

            var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/" + "WowPacketParserModule.V6_0_2_19033.dll");
            Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> handlerDictionary = Handler.LoadHandlers(assembly, ClientVersionBuild.V6_0_3_19342);

            var handledOpcodes = new HashSet<Opcode>(handlerDictionary.Select(pair => pair.Key.Value));
            handledOpcodes.UnionWith(Handler.LoadDefaultHandlers().Select(pair => pair.Key.Value));

            var unhandledOpcodes = new HashSet<Opcode>();

            foreach (Opcode opcode in clientOpcodes)
            {
                if (!handledOpcodes.Contains(opcode))
                    unhandledOpcodes.Add(opcode);
            }

            foreach (Opcode opcode in serverOpcodes)
            {
                if (!handledOpcodes.Contains(opcode))
                    unhandledOpcodes.Add(opcode);
            }

            var anyUnhandled = unhandledOpcodes.Count > 0;

            if (anyUnhandled)
            {
                foreach (var opc in unhandledOpcodes)
                {
                    Console.WriteLine("Warning: {0} does not have any handler in 6.0.2.", opc);
                }
            }

            Assert.IsTrue(!anyUnhandled, "Found unhandled opcodes defined.");
        }
    }
}
