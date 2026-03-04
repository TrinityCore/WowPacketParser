using System.CodeDom.Compiler;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V5_5_0_61735.UpdateFields.V2_5_5_64796
{
    [GeneratedCode("UpdateFieldCodeGenerator.Formats.WowPacketParserHandler", "1.0.0.0")]
    public class GameObjectAssistActionData : IGameObjectAssistActionData
    {
        public string PlayerName { get; set; }
        public string MonsterName { get; set; }
        public uint VirtualRealmAddress { get; set; }
        public byte Sex { get; set; }
        public long Time { get; set; }
        public int DelveTier { get; set; }
    }
}
