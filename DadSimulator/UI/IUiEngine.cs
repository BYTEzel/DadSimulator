using Microsoft.Xna.Framework;

namespace DadSimulator.UI
{
    public interface IUiEngine
    {
        void DrawRectangle(Rectangle rect, Color color);
        public void DrawRectangleInteractable(int yPosition, Color color, string headline, string textInBox);
        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline, float scaling = 1.0f);

    }
}