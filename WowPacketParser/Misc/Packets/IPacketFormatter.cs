using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    /// <summary>
    /// This interface defines the output representation of a single packet.
    /// Basically it distingueshes between packet headers, packet nodes  and
    /// collections (like for multi-packets).
    /// </summary>
    public interface IPacketFormatter
    {
        void AppendItem(string itemName, params object[] args);
        string AppendHeaders(Direction direction, int opcode, long length,
            int connectionIndex, IPEndPoint endPoint, DateTime time, int number,
            bool isMultiple);

        void AppendItemWithContent(string itemName, string itemContent, params object[] args);

        //void AppendCollection(string collectionName, params KeyValuePair<string,string>[] args);
        void OpenCollection(string collectionName, params object[] args);
        void CloseCollection(string collectionName);

        void CloseItem();

    }
}
