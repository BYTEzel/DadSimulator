using DadSimulator.GraphicObjects;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DadSimulator.Animation
{
    public class Spritesheet : IGraphicObject
    {
        private readonly Texture2D m_spritesheet;
        private readonly List<string> m_animationNames;
        private readonly Size m_frameSize;

        private int m_animationIndex;
        private int m_frameIndex;
        private readonly int m_maxFrames;

        private double m_timeSinceLastUpdate;

        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public float FPS { get; set; }

        public Spritesheet(Texture2D spritesheet, int framesPerColumn, List<string> animationNames)
        {
            Position = Vector2.Zero;
            m_spritesheet = spritesheet;
            m_animationNames = animationNames;
            m_frameSize = new Size(
                Convert.ToUInt32(m_spritesheet.Width / framesPerColumn), 
                Convert.ToUInt32(m_spritesheet.Height / m_animationNames.Count));
            m_animationIndex = 0;
            m_frameIndex = 0;
            m_timeSinceLastUpdate = 0;
            m_maxFrames = framesPerColumn;
            FPS = 1.0f;
        }

        public void Draw(SpriteBatch batch)
        {
            var roiLeftUpperCorner = new Point((int)(m_frameIndex * m_frameSize.Width), (int)(m_animationIndex * m_frameSize.Height));
            var roi = new Rectangle(roiLeftUpperCorner, new Point((int)m_frameSize.Width, (int)m_frameSize.Height));
            batch.Draw(m_spritesheet, Position, roi, Color);
        }

        public void Initialize()
        {
        }

        public void SetAnimation(string animationName)
        {
            const int notFound = -1;
            var index = m_animationNames.FindIndex(x => x == animationName);
            if (notFound == index)
            {
                throw new ArgumentException("Unknown animation name");
            }

            m_animationIndex = index;
            m_frameIndex = 0;
        }

        public void Update(double elapsedTime)
        {
            m_timeSinceLastUpdate += elapsedTime;
            var updateFrequency = 1 / FPS;
            if (m_timeSinceLastUpdate > updateFrequency)
            {
                m_frameIndex = (m_frameIndex + 1) % m_maxFrames;
            }
        }
    }
}
