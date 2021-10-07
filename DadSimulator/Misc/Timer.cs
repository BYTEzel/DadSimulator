using Microsoft.Xna.Framework;
using System;

namespace DadSimulator.Misc
{
    public class Timer
    {
        private double m_gameTimeInSeconds;
        private readonly string[] m_days = { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };

        public Timer()
        {

        }

        public void Update(GameTime time)
        {
            m_gameTimeInSeconds = time.TotalGameTime.TotalSeconds;
        }

        public TimeSpan GetGameTime()
        {
            return TimeSpan.FromSeconds(m_gameTimeInSeconds * 60);
        }

        public string GetGameDateAndTime()
        {
            var gameTime = GetGameTime();
            var day = m_days[gameTime.Days % m_days.Length];
            return $"{day}, {gameTime.ToString(@"hh\:mm\:ss")}";
        }
    }
}
