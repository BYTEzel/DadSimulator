using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.IO
{
    public enum Templates { Test, TestTextureTransparency, Character }

    public interface ITemplateLoader
    {
        Texture2D LoadTemplate(Templates name);
        Color[,] LoadTemplateContent(Templates name);
    }
}
