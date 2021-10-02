using Microsoft.Xna.Framework;

namespace DadSimulator.UI
{
    public interface IUiEngine
    {
        void DrawRectangle(Rectangle rect, Color color);
        public void DrawRectangleInteractable(Point positionTopLeft, Color color, string headline, string textInBox);
        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline);

    }
}