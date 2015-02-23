namespace WowPacketParser.Enums
{
    public enum Direction
    {
        /// <summary>
        /// CMSG opcodes.
        /// </summary>
        ClientToServer   = 0,
        /// <summary>
        /// SMSG opcodes.
        /// </summary>
        ServerToClient   = 1,
        /// <summary>
        /// Battle.NET Client to Server.
        /// </summary>
        BNClientToServer = 2,
        /// <summary>
        /// Battle.NET Server to Client
        /// </summary>
        BNServerToClient = 3,
        /// <summary>
        /// Bidirectional opcode (MSG, UMSG, TEST...)
        /// </summary>
        Bidirectional    = 4
    }
}
