using Microsoft.Xna.Framework;

namespace DadSimulator.Collider
{
    public class AlignedPointCloud
    {
        public Vector2 Shift;
        public PointCloud PointCloud;

        public AlignedPointCloud()
        {
            Shift = Vector2.Zero;
            PointCloud = new PointCloud();
        }
    }
}
