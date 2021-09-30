using DadSimulator.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.Tests
{
    public class GraphicsLoader
    {
        private static readonly ITemplateLoader m_loader;

        static GraphicsLoader()
        {
            var sim = new DadSimulator();
            sim.RunOneFrame();
            m_loader = sim;
        }

        public static Texture2D LoadTemplate(Templates name)
        {
            return m_loader.LoadTemplate(name);
        }
        public static Color[,] LoadTemplateContent(Templates name)
        {
            return m_loader.LoadTemplateContent(name);
        }
    }
}
