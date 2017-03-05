using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;

namespace WowPacketParser.Misc
{
    /// <summary>
    /// This class contains the representation of a packet in a simple text format.
    /// <seealso cref="IPacketFormatter">
    /// This implementation of IPacketFormatter basically contains the previous 
    /// textual output format. </seealso>
    /// </summary>
    public class TextPacketFormatter : IPacketFormatter
    {
        private StringBuilder _writer;
        private List<string> _collections;
        private bool _inCollection = false;

        public TextPacketFormatter()
        {
            _writer = new StringBuilder();
            _collections = new List<string>();
        }

        public void AppendItem(string itemName, params object[] args)
        {
            if (!Settings.DumpFormatWithText())
                return;
            if (_inCollection)
                _writer.AppendLine();

            if(args.Length == 0)
                _writer.AppendLine(itemName);
            else
                _writer.AppendLine(string.Format(itemName, args));
        }



        public string AppendHeaders(Direction direction, int opcode, long length, int connectionIndex, IPEndPoint endPoint, DateTime time, int number, bool isMultiple)
        {
            var headers = string.Format("{0}: {1} (0x{2}) Length: {3} ConnIdx: {4}{5} Time: {6} Number: {7}{8}",
                direction, Opcodes.GetOpcodeName(opcode, direction, false), opcode.ToString("X4"),
                length, connectionIndex, endPoint != null ? " EP: " + endPoint : "", time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                number, isMultiple ? " (part of another packet)" : "");
            _writer.AppendLine(headers);

            return headers;
        }

        public void OpenCollection(string collectionName, params object[] args)
        {
            if (!Settings.DumpFormatWithText())
                return;
            _collections.Add(collectionName);
            _inCollection = true;
            _writer.AppendLine("{");
        }

        public void CloseCollection(string collectionName)
        {
            if (_collections.Contains(collectionName))
                _collections.Remove(collectionName);
            _inCollection = false;
            _writer.AppendLine("}");
        }

        public void CloseItem()
        {
            _writer.Clear();
        }

        public override string ToString()
        {
            return _writer.ToString();
        }

        public void AppendItemWithContent(string itemName, string itemContent, params object[] args)
        {
            AppendItem(itemName, args);
            AppendItem(itemContent);
        }
    }
}
