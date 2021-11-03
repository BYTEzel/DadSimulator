using System;

namespace DadSimulator.Misc
{
    public interface ITimeOffset
    {
        void ChangeTimeOffset(TimeSpan timeDiff);
    }
}
