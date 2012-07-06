using System.Collections.Generic;
using PacketParser.Enums;

namespace PacketDumper.DataStructures
{
    public class StartAction
    {
        public ICollection<Action> Actions;
    }

    public class Action
    {
        public uint Button;

        public uint Id;

        public ActionButtonType Type;
    }
}
