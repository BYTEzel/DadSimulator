using Microsoft.Xna.Framework;
using System;

namespace DadSimulator.Collider
{
    public enum IntersectionType { NoIntersection, Intersection, CompletelyContained, Equal }
    
    public struct IntersectionResult
    {
        public IntersectionType Type;
        public AlignedPointCloud AlignedPointCloud;
        public AlignedRectangle AlignedBoundingBox;
    }

    public class Collision
    {
        /// <summary>
        /// Checks, if the two point clouds intersect.
        /// </summary>
        /// <param name="refPc">Reference point cloud.</param>
        /// <param name="comparePc">Point cloud which is compared to the reference.</param>
        /// <returns>IntersectionResult where the points of <paramref name="refPc"/> are returned, 
        /// which intersect with the points of <paramref name="comparePc"/>.</returns>
        public static IntersectionResult Intersection(AlignedPointCloud refPc, AlignedPointCloud comparePc)
        {
            IntersectionResult result = InitIntersectionResult(refPc);

            int x1, x2, y1, y2;
            bool isInitialized;
            ComputeIntersection(refPc, comparePc, result, out x1, out x2, out y1, out y2, out isInitialized);
            result = AssignBoundingBox(result, x1, x2, y1, y2, isInitialized);
            result = CheckIntersectionResultType(refPc, comparePc, result);
            return result;
        }

        private static IntersectionResult AssignBoundingBox(IntersectionResult result, int x1, int x2, int y1, int y2, bool isInitialized)
        {
            if (isInitialized)
            {
                result.AlignedBoundingBox.Rectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            else
            {
                result.AlignedBoundingBox = null;
            }

            return result;
        }

        private static IntersectionResult CheckIntersectionResultType(AlignedPointCloud refPc, AlignedPointCloud comparePc, IntersectionResult result)
        {
            if (result.AlignedPointCloud.PointCloud.PointsInOrigin.Count > 0)
            {
                result.Type = IntersectionType.Intersection;
            }

            if (result.AlignedPointCloud.PointCloud.PointsInOrigin.Count == refPc.PointCloud.PointsInOrigin.Count)
            {
                result.Type = IntersectionType.CompletelyContained;
            }

            if (refPc.PointCloud.PointsInOrigin.Count == comparePc.PointCloud.PointsInOrigin.Count && 
                result.AlignedPointCloud.PointCloud.PointsInOrigin.Count == refPc.PointCloud.PointsInOrigin.Count)
            {
                result.Type = IntersectionType.Equal;
            }

            return result;
        }

        private static void ComputeIntersection(AlignedPointCloud refPc, AlignedPointCloud comparePc, IntersectionResult result, out int x1, out int x2, out int y1, out int y2, out bool isInitialized)
        {
            if (refPc.PointCloud.PointsInOrigin.Count <= 0 || comparePc.PointCloud.PointsInOrigin.Count <= 0)
            {
                throw new ArgumentException("Invalid amount of points, unable to compute intersection");
            }

            var refShiftX = (int)refPc.Shift.X;
            var refShiftY = (int)refPc.Shift.Y;
            var compareShiftX = (int)comparePc.Shift.X;
            var compareShiftY = (int)comparePc.Shift.Y;
            x1 = 0;
            x2 = 0;
            y1 = 0;
            y2 = 0;
            isInitialized = false;

            foreach (var point in comparePc.PointCloud.PointsInOrigin)
            {
                if (refPc.PointCloud.PointsInOrigin.FindIndex(x =>
                    x.X + refShiftX == point.X + compareShiftX &&
                    x.Y + refShiftY == point.Y + compareShiftY) > -1)
                {
                    var newPoint = new Point(point.X - compareShiftX, point.Y - compareShiftY);
                    if (!isInitialized)
                    {
                        x1 = newPoint.X;
                        x2 = newPoint.X;
                        y1 = newPoint.Y;
                        y2 = newPoint.Y;
                        isInitialized = true;
                    }
                    else
                    {
                        x1 = Math.Min(x1, newPoint.X);
                        x2 = Math.Max(x2, newPoint.X);
                        y1 = Math.Min(y1, newPoint.Y);
                        y2 = Math.Max(y2, newPoint.Y);
                    }
                    result.AlignedPointCloud.PointCloud.PointsInOrigin.Add(newPoint);
                }
            }
        }

        private static IntersectionResult InitIntersectionResult(AlignedPointCloud refPc)
        {
            var result = new IntersectionResult()
            {
                Type = IntersectionType.NoIntersection,
                AlignedPointCloud = new AlignedPointCloud(),
                AlignedBoundingBox = new AlignedRectangle()
            };
            result.AlignedPointCloud.Shift = refPc.Shift;
            result.AlignedBoundingBox.Shift = refPc.Shift;
            return result;
        }
    }
}
