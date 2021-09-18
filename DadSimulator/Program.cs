using System;

namespace DadSimulator
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DadSimulator())
                game.Run();
        }
    }
}
