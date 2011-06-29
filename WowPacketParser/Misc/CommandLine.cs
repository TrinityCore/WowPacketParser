namespace WowPacketParser.Misc
{
    public sealed class CommandLine
    {
        public string[] Arguments { get; private set; }

        public CommandLine(string[] args)
        {
            Arguments = args;
        }

        public string GetValue(string param)
        {
            for (var i = 0; i < Arguments.Length; i++)
            {
                var str = Arguments[i];

                if (str != param)
                    continue;

                if (Arguments.Length > i)
                    return Arguments[i + 1];
            }

            return string.Empty;
        }
    }
}
