using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Animation
{
    public interface ISpritesheet
    {
        Color Color { get; set; }
        float FPS { get; set; }
        Vector2 Position { get; set; }

        void Draw(SpriteBatch batch);
        void Initialize();
        void SetAnimation(string animationName);
        void Update(double elapsedTime);
    }
}