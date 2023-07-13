namespace WowPacketParser.Enums
{
    public enum MailType
    {
       Normal                   = 0,
       Auction                  = 2,
       Creature                 = 3,                        // client send CMSG_CREATURE_QUERY on this mailmessagetype
       GameObject               = 4,                        // client send CMSG_GAMEOBJECT_QUERY on this mailmessagetype
       Calendar                 = 5,
       Blackmarket              = 6,
       CommerceAuction          = 7,                        // wow token auction
       Auction_2                = 8,
       ArtisansConsortium       = 9,                        // crafting orders

       // legacy, unknown when it was removed
       Item                     = 5,
    }
}
