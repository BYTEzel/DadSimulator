using Microsoft.Xna.Framework;

namespace DadSimulator.Misc
{
    public interface IPosition
    {
        /// <summary>
        /// Get the location of the object. This can be used for the player to check,
        /// if the object is in reach to be interacted with.
        /// </summary>
        /// <returns>Location of the interaction object.</returns>
        Vector2 GetPosition();
    }
}
