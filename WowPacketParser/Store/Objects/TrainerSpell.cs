using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Store.Objects
{
    public class TrainerSpell
    {
        public uint Spell;

        public uint Cost;

        public byte RequiredLevel;

        public uint RequiredSkill;

        public uint RequiredSkillLevel;
    }
}
