using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HandlerQuery
{
    static class Program
    {
        static void Main()
        {
            var dict = HandlerQuery.GetReadDictionary();

            Console.WriteLine(
                "| (0 - any) \n" + // not implemented
                "| 1 - int8 \n" +
                "| 2 - int16 \n" +
                "| 3 - int32 \n" +
                "| 4 - int64 \n" +
                "| 5 - float \n" +
                "| 6 - guid \n" +
                "| 7 - packedguid \n" +
                "| 8 - ip \n" +
                "| 9 - string \n" +
                "| = - exact search \n" +
                "| + - subset search \n");
            Console.WriteLine("Codes separated by spaces, type q to exit.");

            while (true)
            {
                Console.Write(">> ");
                var str = Console.ReadLine();
                if (str == null)
                    continue;

                if (str == "q")
                    break;

                var codeArrStr = str.Split(' ');
                if (codeArrStr.Length == 0)
                {
                    Console.WriteLine("Invalid query.");
                    continue;
                }

                var equal = false;
                var subset = false;

                var failed = false;
                var codeArr = new List<ReadMethods>(codeArrStr.Length);
                foreach (var s in codeArrStr)
                {
                    if (s == "=")
                    {
                        equal = true;
                        continue;
                    }

                    if (s == "+")
                    {
                        subset = true;
                        continue;
                    }

                    ReadMethods result;
                    if (Enum.TryParse(s, true, out result))
                        codeArr.Add(result);
                    else
                    {
                        Console.WriteLine("{0} is an invalid code", s);
                        failed = true;
                        break;
                    }
                }

                if (failed)
                    continue;

                var i = -1;
                foreach (KeyValuePair<MethodInfo, List<ReadMethods>> pair in dict)
                {
                    if (pair.Value.Count < codeArr.Count)
                        continue;

                    if (equal) // exactly equal
                    {
                        if (pair.Value.SequenceEqual(codeArr))
                        {
                            i++;
                            Console.WriteLine("[{0}] {1}", i, pair.Key);
                        }
                    }
                    else if (subset) // subset
                    {
                        if (!codeArr.Except(pair.Value).Any())
                        {
                            i++;
                            Console.WriteLine("[{0}] {1}", i, pair.Key);
                        }
                    }
                    else // starts with
                    {
                        if (pair.Value.GetRange(0, codeArr.Count).SequenceEqual(codeArr))
                        {
                            i++;
                            Console.WriteLine("[{0}] {1}", i, pair.Key);
                        }
                    }
                }

                if (i == -1)
                    Console.WriteLine("No results");

            }

            Console.WriteLine("Good bye.");
            Console.ReadKey(true);
        }
    }
}
