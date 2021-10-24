using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DadSimulator.IO
{
    public enum Templates 
    {
        Test, TestTextureTransparency, 
        Character, 
        LevelGrassBackground, LevelFloor, LevelInterior, LevelInteriorBackground, LevelWalls, LevelWindowsImages,
        WashingMachine
    }

    public interface ITemplateLoader
    {
        /// <summary>
        /// Load a template from the database.
        /// </summary>
        /// <param name="name">Name of the template.</param>
        /// <returns>Texture corresponding to the template.</returns>
        Texture2D LoadTemplate(Templates name);
        /// <summary>
        /// Load the pixel content of the given template.
        /// </summary>
        /// <param name="name">Name of the template.</param>
        /// <returns>All pixel values in [RGBA] of the template in row/x-ordering as first dimension.</returns>
        Color[,] LoadTemplateContent(Templates name);
    }
}
