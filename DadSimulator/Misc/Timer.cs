using Microsoft.Xna.Framework;
using System;

namespace DadSimulator.Misc
{
    public class Timer : ITimeOffset, IUpdate
    {
        private double m_gameTimeInSeconds;
        private double m_timeOffset;
        private readonly string[] m_days = { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY" };

        public Timer()
        {
            m_timeOffset = 0;
        }

        public void Update(double timeSeconds)
        {
            m_gameTimeInSeconds = timeSeconds;
        }

        public void ChangeTimeOffset(TimeSpan timeDiff)
        {
            m_timeOffset += timeDiff.TotalSeconds;
        }

        public TimeSpan GetGameTime()
        {
            return TimeSpan.FromSeconds((m_gameTimeInSeconds * 60) + m_timeOffset);
        }

        public string GetGameDateAndTime()
        {
            var gameTime = GetGameTime();
            var day = m_days[gameTime.Days % m_days.Length];
            return $"{day}, {gameTime:hh\\:mm\\:ss}";
        }
    }
}
