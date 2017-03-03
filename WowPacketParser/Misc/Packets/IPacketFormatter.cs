using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public interface IPacketFormatter
    {
        //Previousely: WriteLine(string format, params object[] args)
        void AppendItem(string itemName, params object[] args);

        void AppendItemWithContent(string itemName, string itemContent, params object[] args);

        //void AppendCollection(string collectionName, params KeyValuePair<string,string>[] args);
        void OpenCollection(string collectionName, params object[] args);
        void CloseCollection(string collectionName);

        void CloseItem();

    }
}
