using DadSimulator.Misc;
using Microsoft.Xna.Framework;

namespace DadSimulator.Camera
{
    public class Camera : ICamera
    {
        public Matrix Transform { get; private set; }
        private readonly Size m_screenSize;
        private IPosition m_target;

        public Camera(Size screenSize, IPosition target=null)
        {
            m_screenSize = screenSize;
            m_target = target;
        }

        public void UpdatePosition()
        {
            var targetPosition = m_target.GetPosition();
            var position = Matrix.CreateTranslation(
              -targetPosition.X,
              -targetPosition.Y,
              0);
            var offset = Matrix.CreateTranslation(
                m_screenSize.Width / 2,
                m_screenSize.Height / 2,
                0);
            Transform = position * offset;
        }

        public Vector2 GetCameraTopLeftPosition()
        {
            var targetPosition = m_target.GetPosition();
            var transformationToTopLeft = new Vector2(m_screenSize.Width / 2, m_screenSize.Height / 2);
            return targetPosition - transformationToTopLeft;
        }

        public void Follow(IPosition target)
        {
            m_target = target;
        }

        public Vector2 GetScreenCenter()
        {
            return m_target.GetPosition();
        }
    }
}
