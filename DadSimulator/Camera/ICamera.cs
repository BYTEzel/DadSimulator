using Microsoft.Xna.Framework;

namespace DadSimulator.Camera
{
    public interface ICamera
    {
        Matrix Transform { get; }

        void UpdatePosition();
    }
}