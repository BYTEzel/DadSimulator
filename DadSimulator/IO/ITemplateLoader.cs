using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.IO
{
    public enum Templates { Test, Character }
    public interface ITemplateLoader
    {
        Texture2D LoadTemplate(Templates name);
    }
}
