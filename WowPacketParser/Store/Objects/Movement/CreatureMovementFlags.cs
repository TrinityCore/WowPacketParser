namespace WowPacketParser.Store.Objects.Movement
{
    public enum CreatureMovementFlags
    {
        None                    = 0, // pathfinding
        ExactPath               = 1, // sent fully
        ExactPathFlying         = 2, // sent fully + flying flag
        ExactPathFlyingCyclic   = 3, // sent fully + flying flag + cyclic flag
        ExactPathAndJump        = 4, // sent fully + parabolic movement at the end
        Invalid                 = 100,            
    }
}
