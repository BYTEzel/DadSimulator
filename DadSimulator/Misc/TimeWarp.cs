using System;

namespace DadSimulator.Misc
{
    public class TimeWarp : IUpdate
    {
        private readonly ITimeOffset m_timer;
        private bool m_warpInProgress;
        private TimeSpan m_warpTarget;
        private TimeSpan m_currentWarpTime;
        private TimeSpan m_increment;

        public TimeWarp(ITimeOffset timer)
        {
            m_timer = timer;
            m_warpInProgress = false;
            m_warpTarget = new TimeSpan(0);
            m_currentWarpTime = new TimeSpan(0);
            m_increment = new TimeSpan(0);
        }

        private bool TimeWarpDone
        { 
            get
            {
                if (m_currentWarpTime >= m_warpTarget)
                {
                    return true;
                }
                return false;
            }
        }

        public bool WarpInProgress { get => m_warpInProgress; }

        public void Update(double elapsedTime)
        {
            if (m_warpInProgress)
            {
                if (TimeWarpDone)
                {
                    m_warpInProgress = false;
                }
                else
                {
                    m_currentWarpTime += m_increment;
                    m_timer.ChangeTimeOffset(m_increment);
                }
            }
        }

        public bool WarpTime(TimeSpan timeSpan, TimeSpan speed)
        {
            var timerStarted = false;
            if (!m_warpInProgress)
            {
                m_warpInProgress = true;
                m_currentWarpTime = new TimeSpan(0);
                m_warpTarget = timeSpan;
                m_increment = speed;
                timerStarted = true;
            }
            return timerStarted;

        }
    }
}
