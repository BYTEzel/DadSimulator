using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.UI
{
    public interface IUiEngine
    {
        void DrawRectangle(SpriteBatch batch, Rectangle rect, Color color);
        public void DrawRectangleInteractable(SpriteBatch batch, Point positionTopLeft, Color color, string headline, string textInBox);
    }
}