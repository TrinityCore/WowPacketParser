using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Tests
{
    [TestFixture]
    public class OpcodeTest
    {
        [Test]
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
                catch (FileNotFoundException e)
                {
                    //Console.WriteLine(e);
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
                foreach (var usedOpcode in usedOpcodes)
                {
                    if (!usedOpcode.Value)
                        Console.WriteLine("Warning: {0} is not used in any handler.", usedOpcode.Key);
                }
            }

            Assert.IsTrue(allUsed, "Found unused opcodes defined.");
        }
    }
}
