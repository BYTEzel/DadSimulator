using DadSimulator.Misc;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.GraphicObjects
{
    public interface IGraphicObject : IUpdate
    {
        void Initialize();
        void Draw(SpriteBatch batch);
    }
}
