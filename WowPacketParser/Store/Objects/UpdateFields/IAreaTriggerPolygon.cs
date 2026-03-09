using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerPolygon
    {
        float? Height { get; }
        float? HeightTarget { get; }
        DynamicUpdateField<Vector2?> Vertices { get; }
        DynamicUpdateField<Vector2?> VerticesTarget { get; }
    }
}
