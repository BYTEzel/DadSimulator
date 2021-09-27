using DadSimulator.Collider;
using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public struct Command
    {
        public char Key;
        public string Description;
    }

    public interface IInteractable
    {
        /// <summary>
        /// Get the name of the object.
        /// </summary>
        /// <returns>Name of the object.</returns>
        string GetName();
        /// <summary>
        /// Get a description of the state the interactable object is in.
        /// </summary>
        /// <returns>Description of the state.</returns>
        string GetState();
        /// <summary>
        /// Get the list of available commands.
        /// </summary>
        /// <returns>List of commands which can be executed.</returns>
        List<Command> GetCommands();
        /// <summary>
        /// Key corresponding to the respective command.
        /// A list of commands, including their keys can be retrieved by calling GetCommands().
        /// </summary>
        /// <param name="key">Key corresponding to a command.</param>
        void ExecuteCommand(char key);
        /// <summary>
        /// Get the point cloud which is stored within the implementing class.
        /// </summary>
        /// <returns>Aligned points.</returns>
        AlignedPointCloud GetInteractableAlignedPointCloud();
    }
}
