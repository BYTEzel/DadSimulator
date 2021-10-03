using DadSimulator.Misc;
using Microsoft.Xna.Framework;

namespace DadSimulator.Camera
{
    public interface ICamera
    {
        Matrix Transform { get; }

        void UpdatePosition();
        void Follow(IPosition target);

        Vector2 GetCameraTopLeftPosition();
    }
}