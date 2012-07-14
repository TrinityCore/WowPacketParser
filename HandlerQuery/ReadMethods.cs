namespace HandlerQuery
{
    enum ReadMethods
    {
        Unknown = -2, // error
        Ignore = -1, // do nothing
        Any = 0, // TODO: this could be used to match all
        ReadInt8 = 1,
        ReadInt16 = 2,
        ReadInt32 = 3,
        ReadInt64 = 4,
        ReadFloat = 5,
        ReadGuid = 6,
        ReadPackedGuid = 7,
        ReadIP = 8,
        ReadString = 9
    }
}
