using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public class TextPacketFormatter : IPacketFormatter
    {
        private StringBuilder _writer;
        private List<string> _collections;
        private bool _inCollection = false;

        public TextPacketFormatter()
        {
            _writer = new StringBuilder();
        }

        public void AppendItem(string itemName, params object[] args)
        {
            if (!Settings.DumpFormatWithText())
                return;
            if (_inCollection)
                _writer.AppendLine();
            //_writer.AppendLine("<<<<<<<DEBUG:" + itemName + ">>>>>>");
            if(args.Length == 0)
                _writer.AppendLine(itemName);
            else
                _writer.AppendLine(string.Format(itemName, args));
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
            throw new NotImplementedException();
        }
    }
}
