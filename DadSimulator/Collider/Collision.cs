using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public enum IntersectionType { NoIntersection, Intersection, Equal }
    
    public struct IntersectionResult
    {
        public IntersectionType Type;
        public AlignedPointCloud AlignedPointCloud;
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
            var result = new IntersectionResult() { Type = IntersectionType.NoIntersection, AlignedPointCloud = new AlignedPointCloud() };
            result.AlignedPointCloud.Shift = refPc.Shift;

            var refShiftX = (int)refPc.Shift.X;
            var refShiftY = (int)refPc.Shift.Y;
            var compareShiftX = (int)comparePc.Shift.X;
            var compareShiftY = (int)comparePc.Shift.Y;
            foreach (var point in comparePc.PointCloud.PointsInOrigin)
            {
                if (refPc.PointCloud.PointsInOrigin.Find(x => 
                    x.X + refShiftX == point.X + compareShiftX &&
                    x.Y + refShiftY == point.Y + compareShiftY ) != null)
                {
                    result.AlignedPointCloud.PointCloud.PointsInOrigin.Add(new Point(point.X - compareShiftX, point.Y - compareShiftY));
                }
            }

            if (result.AlignedPointCloud.PointCloud.PointsInOrigin.Count > 0)
            {
                result.Type = IntersectionType.Intersection;
            }

            if (result.AlignedPointCloud.PointCloud.PointsInOrigin.Count == refPc.PointCloud.PointsInOrigin.Count)
            {
                result.Type = IntersectionType.Equal;
            }

            return result;
        }
    }
}
