using System;
using DadSimulator.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.GraphicObjects
{
    public class LevelBackgroundDayNight : LevelBackground
    {
        private Timer m_timer;
        private Color m_drawColor;
        
        public LevelBackgroundDayNight(Texture2D texture2D, Vector2 position, Timer timer) : base(texture2D, position)
        {
            m_timer = timer;
        }

        public override void Update(double elapsedTime)
        {
            var sunHighest = new TimeSpan(12, 0, 0);
            var sunLowest = new TimeSpan(24, 0, 0);
            
            var tmp = m_timer.GetGameTime();
            var currentTime = new TimeSpan(tmp.Hours, tmp.Minutes, tmp.Seconds);

            var isSunGoingDown = currentTime.TotalSeconds > sunHighest.TotalSeconds;

            var spanIntensity = byte.MaxValue - byte.MinValue;
            byte intensity;
            if (isSunGoingDown)
            {
                intensity = (byte) (byte.MaxValue - (spanIntensity * currentTime.TotalSeconds / Math.Abs(sunLowest.TotalSeconds - sunHighest.TotalSeconds)));
            }
            else
            {
                intensity = (byte) (spanIntensity * currentTime.TotalSeconds / Math.Abs(sunHighest.TotalSeconds - sunLowest.TotalSeconds));
            }
            m_drawColor = new Color(intensity, intensity, intensity, byte.MaxValue);
            base.Update(elapsedTime);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(m_texture, Position, null, m_drawColor);
        }
    }
}
