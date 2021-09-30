using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public interface ICollisionChecker
    {
        void AddTextureContent(Color[,] content, Vector2 shift);
        void AddRectangle(Rectangle rectangle);
        void AddAlignedPointCloud(AlignedPointCloud apc);
        bool IsColliding(AlignedPointCloud apc);
        bool IsColliding(ICollidable collidable);
    }
}
