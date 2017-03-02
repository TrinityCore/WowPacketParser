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
        void WriteLine(string format, params object[] args);

        //Previousely: WriteLine(string value)
        void WriteLine(string value);

        void Clear();

    }
}
