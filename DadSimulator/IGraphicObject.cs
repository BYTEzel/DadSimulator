using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator
{
    interface IGraphicObject
    {
        void Initialize();
        void Update(double elapsedTime);
        void Draw(SpriteBatch batch);
    }
}
