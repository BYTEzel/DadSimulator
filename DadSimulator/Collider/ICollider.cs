using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public interface ICollider
    {
        /// <summary>
        /// Set the transformation which is applied on the point cloud.
        /// </summary>
        /// <param name="shift">Transformation for the points.</param>
        void SetShift(Vector2 shift);

        /// <summary>
        /// Get the point cloud which is stored within the implementing class.
        /// </summary>
        /// <returns>Aligned points.</returns>
        PointCloud GetAlignedPoints();

        /// <summary>
        /// Computes the intersection with the point cloud of the implementing class
        /// (which can be received by GetAlignedPoints.
        /// </summary>
        /// <param name="pointClouds">Points, to which the current point cloud is compared to.</param>
        /// <returns>A list of intersecting points.</returns>
        List<PointCloud> Intersection(IEnumerable<PointCloud> pointClouds);
    }
}
