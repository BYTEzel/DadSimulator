using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public interface ICollisionChecker
    {
        void AddTextureContent(Color[,] content, Vector2 shift);
        void AddRectangle(Rectangle rectangle);
        void AddAlignedPointCloud(AlignedPointCloud apc);
        bool Intersect(AlignedPointCloud apc);
        bool Intersect(ICollider collider);
    }
}
