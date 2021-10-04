﻿using DadSimulator.Camera;
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

        public void DrawLine(Vector2 point1, Vector2 point2, float thickness, Color color)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)System.Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(distance, thickness);
            m_spriteBatch.Draw(m_rectBase, point1, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

        public void DrawRectangle(Rectangle rect, Color color)
        {
            m_spriteBatch.Draw(m_rectBase, rect, color);
        }

        public void DrawRectangleInteractable(Vector2 positionInteractable, RelativePosition relativePosition, Color color, string headline, string textInBox)
        {
            const float scalingHeadline = 0.7f;
            const float scalingText = 0.5f;
            const int padding = 10;
            const int shift = 50;

            var textSizeHeadline = m_fontHeadline.MeasureString(headline) * scalingHeadline;
            var textSizeInBox = m_fontText.MeasureString(textInBox) * scalingText;

            Point positionTopLeft = ComputeTopLeftPosition(positionInteractable, relativePosition,
                padding, shift, textSizeHeadline, textSizeInBox);

            Rectangle rectangle = ComputeRectangle(padding, textSizeHeadline, textSizeInBox, positionTopLeft);

            DrawRectangle(rectangle, color);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + padding), Color.White, headline, true, scalingHeadline);
            DrawText(new Vector2(positionTopLeft.X + padding, positionTopLeft.Y + textSizeHeadline.Y + padding), Color.White, textInBox, false, scalingText);
        }

        private static Rectangle ComputeRectangle(int padding, Vector2 textSizeHeadline, Vector2 textSizeInBox, Point positionTopLeft)
        {
            return new Rectangle(
                            positionTopLeft,
                            new Point(
                                x: (int)System.Math.Max(textSizeHeadline.X, textSizeInBox.X) + padding * 2,
                                y: (int)(textSizeHeadline.Y + textSizeInBox.Y) + (padding * 2)));
        }

        private static Point ComputeTopLeftPosition(Vector2 positionInteractable, RelativePosition relativePosition, int padding, int shift, Vector2 textSizeHeadline, Vector2 textSizeInBox)
        {
            float posY;
            if (relativePosition == RelativePosition.Top)
            {
                posY = positionInteractable.Y - (shift + 2 * padding + textSizeHeadline.Y + textSizeInBox.Y);
            }
            else
            {
                posY = positionInteractable.Y + shift;
            }

            var positionTopLeft = new Point(
                x: (int)(positionInteractable.X - padding - (System.Math.Max(textSizeHeadline.X, textSizeInBox.X) / 2)),
                y: (int)(posY));
            return positionTopLeft;
        }

        public void DrawText(Vector2 positionTopLeft, Color color, string text, bool isHeadline, float scaling = 1.0f)
        {
            var font = isHeadline ? m_fontHeadline : m_fontText;
            m_spriteBatch.DrawString(font, text, positionTopLeft, color, 0, Vector2.Zero, scaling, SpriteEffects.None, 0);

        }
    }
}
