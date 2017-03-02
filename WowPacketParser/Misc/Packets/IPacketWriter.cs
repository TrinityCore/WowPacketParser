using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public interface IPacketWriter
    {
        //Previousely: WriteLine(string format, params object[] args)
        void WriteItem(string format, params object[] args);

        void CloseItem();

    }
}
