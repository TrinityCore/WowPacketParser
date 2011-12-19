using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class StartAction
    {
        public List<Action> Actions;
    }

    public class Action
    {
        public uint Button;

        public uint Id;

        public ActionButtonType Type;
    }
}
