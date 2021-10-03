using DadSimulator.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.UI
{
    public class UiEngine : IUiEngine
    {
        private readonly Texture2D m_rectBase;
        private readonly SpriteFont m_fontHeadline, m_fontText;
        private readonly SpriteBatch m_spriteBatch;
        private readonly ICamera m_camera;

        public UiEngine(GraphicsDevice graphics, SpriteBatch batch, SpriteFont fontHeadline, SpriteFont fontText, ICamera camera)
        {
            m_rectBase = new Texture2D(graphics, 1, 1);
            m_rectBase.SetData(new[] { Color.White });
            m_fontHeadline = fontHeadline;
            m_fontText = fontText;
            m_spriteBatch = batch;
            m_camera = camera;
        }

        ~UiEngine()
        {
            m_rectBase.Dispose();
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            m_spriteBatch.Draw(m_rectBase, rect, color);
        }

        public void DrawRectangleInteractable(int xPosition, Color color, string headline, string textInBox)
        {
            const float scalingHeadline = 0.7f;
            const float scalingText = 0.5f;

            var textSizeHeadline = m_fontHeadline.MeasureString(headline) * scalingHeadline;
            var textSizeInBox = m_fontText.MeasureString(textInBox) * scalingText;
            int padding = 10;

            var positionTopLeft = new Point(x: xPosition, y: (int)m_camera.GetCameraTopLeftPosition().Y + 50);

            var rectangle = new Rectangle(
                positionTopLeft,
                new Point(
                    x: (int)System.Math.Max(textSizeHeadline.X, textSizeInBox.X) + padding * 2,
                    y: (int)(textSizeHeadline.Y + textSizeInBox.Y) + (padding * 2)));

            DrawRectangle(rectangle, color);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + padding), Color.White, headline, true, scalingHeadline);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + textSizeHeadline.Y + padding), Color.White, textInBox, false, scalingText);
        }

        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline, float scaling = 1.0f)
        {
            var font = isHeadline ? m_fontHeadline : m_fontText;
            m_spriteBatch.DrawString(font, text, positionTopLeft, color, 0, Vector2.Zero, scaling, SpriteEffects.None, 0);

        }
    }
}
