namespace WowPacketParser.Enums
{
    public enum Direction
    {
        /// <summary>
        /// CMSG, MSG, and all other exotic opcodes fit here.
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
    }
}
