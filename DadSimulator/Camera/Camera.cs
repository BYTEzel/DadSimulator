using DadSimulator.Misc;
using Microsoft.Xna.Framework;

namespace DadSimulator.Camera
{
    public class Camera : ICamera
    {
        public Matrix Transform { get; private set; }
        private readonly Size m_screenSize;
        private IPosition m_target;
        private float m_zoom;

        public Camera(Size screenSize, float zoom, IPosition target=null)
        {
            m_screenSize = screenSize;
            m_target = target;
            m_zoom = zoom;
        }

        public void UpdatePosition()
        {
            var targetPosition = m_target.GetPosition();
            var position = Matrix.CreateTranslation(
              -targetPosition.X,
              -targetPosition.Y,
              0);
            var zoom = Matrix.CreateScale(m_zoom);
            var offset = Matrix.CreateTranslation(
                m_screenSize.Width / 2,
                m_screenSize.Height / 2,
                0);
            Transform = position * zoom * offset;
        }

        public Vector2 GetCameraTopLeftPosition()
        {
            var targetPosition = m_target.GetPosition();
            var transformationToTopLeft = new Vector2(m_screenSize.Width / 2, m_screenSize.Height / 2) / m_zoom;
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
