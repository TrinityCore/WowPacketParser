using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliCUFProfile
    {
        public string Name;
        public ushort FrameHeight;
        public ushort FrameWidth;
        public byte SortBy;
        public byte HealthText;
        public bool KeepGroupsTogether;
        public bool DisplayPets;
        public bool DisplayMainTankAndAssist;
        public bool DisplayHealPrediction;
        public bool DisplayAggroHighlight;
        public bool DisplayOnlyDispellableDebuffs;
        public bool DisplayPowerBar;
        public bool DisplayBorder;
        public bool UseClassColors;
        public bool HorizontalGroups;
        public bool DisplayNonBossDebuffs;
        public bool DynamicPosition;
        public byte TopPoint;
        public byte BottomPoint;
        public byte LeftPoint;
        public ushort TopOffset;
        public ushort BottomOffset;
        public ushort LeftOffset;
        public bool Locked;
        public bool Shown;
        public bool AutoActivate2Players;
        public bool AutoActivate3Players;
        public bool AutoActivate5Players;
        public bool AutoActivate10Players;
        public bool AutoActivate15Players;
        public bool AutoActivate25Players;
        public bool AutoActivate40Players;
        public bool AutoActivateSpec1;
        public bool AutoActivateSpec2;
        public bool AutoActivatePvP;
        public bool AutoActivatePvE;
    }
}
