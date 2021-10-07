using Microsoft.Xna.Framework;

namespace DadSimulator.UI
{
    public enum RelativePosition { Top, Bottom };

    public interface IUiEngine
    {
        void DrawLine(Vector2 point1, Vector2 point2, float thickness, Color color);
        void DrawRectangle(Rectangle rect, Color color);
        public void DrawRectangleInteractable(Vector2 positionInteractable, RelativePosition relativePosition, string headline, string textInBox, Color colorBox, Color colorBorder, int borderSize = 2);
        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline, float scaling = 1.0f);

    }
}