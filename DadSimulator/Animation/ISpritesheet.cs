using DadSimulator.GraphicObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Animation
{
    public interface ISpritesheet : IGraphicObject
    {
        Color Color { get; set; }
        float FPS { get; set; }
        Vector2 Position { get; set; }
        void SetAnimation(string animationName);
    }
}