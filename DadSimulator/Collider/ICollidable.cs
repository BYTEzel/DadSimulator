using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public interface ICollidable
    {
        /// <summary>
        /// Get the name of the object corresponding to the collider.
        /// </summary>
        /// <returns>Name of the object.</returns>
        string GetName();

        /// <summary>
        /// Set the transformation which is applied on the point cloud.
        /// </summary>
        /// <param name="shift">Transformation for the points.</param>
        void SetShift(Vector2 shift);
        /// <summary>
        /// Get the point cloud which is stored within the implementing class.
        /// </summary>
        /// <returns>Aligned points.</returns>
        AlignedPointCloud GetColliderAlignedPointCloud();
    }
}
