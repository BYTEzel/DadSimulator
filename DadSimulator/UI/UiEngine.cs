using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.UI
{
    public class UiEngine : IUiEngine
    {
        private readonly Texture2D m_rectBase;
        private readonly SpriteFont m_fontHeadline, m_fontText;

        public UiEngine(GraphicsDevice graphics, SpriteFont fontHeadline, SpriteFont fontText)
        {
            m_rectBase = new Texture2D(graphics, 1, 1);
            m_rectBase.SetData(new[] { Color.White });
            m_fontHeadline = fontHeadline;
            m_fontText = fontText;
        }

        ~UiEngine()
        {
            m_rectBase.Dispose();
        }

        public void DrawRectangle(SpriteBatch batch, Rectangle rect, Color color)
        {
            batch.Draw(m_rectBase, rect, color);
        }

        public void DrawRectangleInteractable(SpriteBatch batch, Point positionTopLeft, Color color, string headline, string textInBox)
        {
            var textSizeHeadline = m_fontHeadline.MeasureString(headline);
            var textSizeInBox = m_fontText.MeasureString(textInBox);
            int padding = 10;

            var rectangle = new Rectangle(
                positionTopLeft,
                new Point(
                    (int)System.Math.Max(textSizeHeadline.X, textSizeInBox.X) + padding * 2,
                    (int)(textSizeHeadline.Y + textSizeInBox.Y) + padding * 2));

            DrawRectangle(batch, rectangle, color);
            batch.DrawString(m_fontHeadline, headline, new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + padding), Color.White);
            batch.DrawString(m_fontText, textInBox, new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + textSizeHeadline.Y + padding), Color.White);
        }
    }
}
