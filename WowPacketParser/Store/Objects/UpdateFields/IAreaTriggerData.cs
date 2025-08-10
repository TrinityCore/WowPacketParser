
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IAreaTriggerData
    {
        int? SpellID { get; }
        int? SpellForVisuals { get; }
        uint? TimeToTarget { get; }
        uint? TimeToTargetScale { get; }
        uint? DecalPropertiesID { get; }

        uint? Flags => null;
        uint? ScaleCurveId => null;
        uint? FacingCurveId => null;
        uint? MorphCurveId => null;
        uint? MoveCurveId => null;

        IAreaTriggerSplineCalculator Spline => null;
        IAreaTriggerOrbit Orbit => null;
        float? ZOffset => null;

        IAreaTriggerSphere Sphere => null;
        IAreaTriggerBox Box => null;
        IAreaTriggerPolygon Polygon => null;
        IAreaTriggerCylinder Cylinder => null;
        IAreaTriggerDisk Disk => null;
        IAreaTriggerBoundedPlane BoundedPlane => null;

        IVisualAnim VisualAnim => null;
    }
}
