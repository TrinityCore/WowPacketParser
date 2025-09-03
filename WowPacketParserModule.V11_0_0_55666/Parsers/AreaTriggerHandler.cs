using System;
using System.Collections.Generic;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class AreaTriggerHandler
    {
        public static void ProcessAreaTriggerSpline(AreaTriggerCreateProperties createProperties, IAreaTriggerData data,
            Packet packet, params object[] indexes)
        {
            var moveTime = data.TimeToTarget ?? createProperties.AreaTriggerData.TimeToTarget ?? 1000;

            var pointCount = data.Spline.Points.Count;
            var points = new List<AreaTriggerCreatePropertiesSplinePoint>(pointCount);
            var distance = 0.0;
            var prevPoint = new Vector3();

            for (var i = 0; i < pointCount; ++i)
            {
                var point = data.Spline.Points[i];
                if (createProperties != null)
                {
                    points.Add(new AreaTriggerCreatePropertiesSplinePoint
                    {
                        areatriggerGuid = createProperties.Guid,
                        Idx = (uint)i,
                        X = point.X,
                        Y = point.Y,
                        Z = point.Z
                    });
                }

                if (i > 1 && i < pointCount - 1) // ignore first and last points (dummy spline points)
                    distance += Vector3.GetDistance(prevPoint, point);

                prevPoint = point;
            }

            if (distance > 0)
            {
                packet.AddValue("Computed Distance", distance, indexes);
                var speed = packet.AddValue("Computed Speed", (distance / moveTime) * 1000, indexes);
                if (createProperties != null)
                    createProperties.Speed = (float)speed;
            }

            foreach (var point in points)
                Storage.AreaTriggerCreatePropertiesSplinePoints.Add(point);
        }

        public static void ProcessAreaTriggerOrbit(AreaTriggerCreateProperties createProperties, IAreaTriggerData data,
            Packet packet, params object[] indexes)
        {
            var orbit = new AreaTriggerCreatePropertiesOrbit
            {
                CreateProperties = createProperties,
                ExtraTimeForBlending = data.Orbit.ExtraTimeForBlending ?? createProperties.AreaTriggerData.Orbit?.ExtraTimeForBlending,
                CircleRadius = data.Orbit.Radius ?? createProperties.AreaTriggerData.Orbit?.Radius,
                BlendFromRadius = data.Orbit.BlendFromRadius ?? createProperties.AreaTriggerData.Orbit?.BlendFromRadius,
                InitialAngle = data.Orbit.InitialAngle ?? createProperties.AreaTriggerData.Orbit?.InitialAngle,
                ZOffset = data.ZOffset ?? createProperties.AreaTriggerData.ZOffset,
                CounterClockwise = data.Orbit.CounterClockwise ?? createProperties.AreaTriggerData.Orbit?.CounterClockwise,
                CanLoop = ((data.Flags ?? createProperties.AreaTriggerData.Flags) & 0x0004) != 0
            };

            var moveTime = data.TimeToTarget ?? createProperties.AreaTriggerData.TimeToTarget ?? 1000;
            var distance = 2 * Math.PI * orbit.CircleRadius.Value;
            packet.AddValue("Computed Distance", distance, indexes);
            var speed = packet.AddValue("Computed Speed", (distance / moveTime) * 1000, indexes);

            if (createProperties != null)
                createProperties.Speed = (float)speed;

            Storage.AreaTriggerCreatePropertiesOrbits.Add(orbit);
        }
    }
}
