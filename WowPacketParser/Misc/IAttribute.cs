using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParser.Misc
{
    public interface IAttribute
    {
        bool IsPrimaryKey { get { return false; } set { } }
        bool IsVisible() => false;
    }
}
