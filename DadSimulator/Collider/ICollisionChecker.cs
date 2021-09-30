using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public interface ICollisionChecker
    {
        /// <summary>
        /// Add a texture content to the map.
        /// </summary>
        /// <param name="content">Colors of a specific image/Template2D. All pixels which are not complete opaque (alpha > 0)
        /// are considered to be a obstacle.</param>
        /// <param name="shift">Shift of the image to a specific position in the image.</param>
        void AddTextureContent(Color[,] content, Vector2 shift);
        /// <summary>
        /// Add a rectangle as obstacle to the collision map.
        /// </summary>
        /// <param name="rectangle">Obstacle size.</param>
        void AddRectangle(Rectangle rectangle);
        /// <summary>
        /// Add a custom aligned point cloud as obstacle to the map.
        /// </summary>
        /// <param name="apc">Obstacle cloud.</param>
        void AddAlignedPointCloud(AlignedPointCloud apc);
        /// <summary>
        /// Checks, if all points in the point cloud are intersecting with the borders 
        /// provided in the internal map.
        /// </summary>
        /// <param name="apc">Points to be checked for intersecting with a border.</param>
        /// <returns>True, if a point intersects, false if no intersection occurs or if points are out of bounds.</returns>
        bool IsColliding(AlignedPointCloud apc);
        /// <summary>
        /// Checks, if the collidable object collides with the point cloud by using its aligned point cloud.
        /// </summary>
        /// <param name="collidable"></param>
        /// <returns></returns>
        bool IsColliding(ICollidable collidable);
    }
}
