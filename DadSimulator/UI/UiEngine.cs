using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.UI
{
    public class UiEngine : IUiEngine
    {
        private readonly Texture2D m_rectBase;
        private readonly SpriteFont m_fontHeadline, m_fontText;
        private readonly SpriteBatch m_spriteBatch;

        public UiEngine(GraphicsDevice graphics, SpriteBatch batch, SpriteFont fontHeadline, SpriteFont fontText)
        {
            m_rectBase = new Texture2D(graphics, 1, 1);
            m_rectBase.SetData(new[] { Color.White });
            m_fontHeadline = fontHeadline;
            m_fontText = fontText;
            m_spriteBatch = batch;
        }

        ~UiEngine()
        {
            m_rectBase.Dispose();
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            m_spriteBatch.Draw(m_rectBase, rect, color);
        }

        public void DrawRectangleInteractable(Point positionTopLeft, Color color, string headline, string textInBox)
        {
            var textSizeHeadline = m_fontHeadline.MeasureString(headline);
            var textSizeInBox = m_fontText.MeasureString(textInBox);
            int padding = 10;

            var rectangle = new Rectangle(
                positionTopLeft,
                new Point(
                    (int)System.Math.Max(textSizeHeadline.X, textSizeInBox.X) + padding * 2,
                    (int)(textSizeHeadline.Y + textSizeInBox.Y) + padding * 2));

            DrawRectangle(rectangle, color);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + padding), Color.White, headline, true);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + textSizeHeadline.Y + padding), Color.White, textInBox, false);
        }

        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline)
        {
            var font = isHeadline ? m_fontHeadline : m_fontText;
            m_spriteBatch.DrawString(font, text, positionTopLeft, color);

        }
    }
}
