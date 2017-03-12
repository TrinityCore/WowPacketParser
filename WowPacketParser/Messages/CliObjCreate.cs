using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliObjCreate
    {
        public bool NoBirthAnim;
        public bool EnablePortals;
        public bool PlayHoverAnim;
        public bool IsSuppressingGreetings;
        public CliMovementUpdate? Move; // Optional
        public CliMovementTransport? Passenger; // Optional
        public CliPosition? Stationary; // Optional
        public ulong? CombatVictim; // Optional
        public uint? ServerTime; // Optional
        public CliVehicleCreate? Vehicle; // Optional
        public CliAnimKitCreate? AnimKit; // Optional
        public long? Rotation; // Optional
        public CliAreaTrigger? AreaTrigger; // Optional
        public CliGameObject? GameObject; // Optional
        public bool ThisIsYou;
        public bool ReplaceActive;
        public CliSceneObjCreate? SceneObjCreate; // Optional
        public CliPlayerSceneInstances? ScenePendingInstances; // Optional
        public List<uint> PauseTimes;
    }
}
