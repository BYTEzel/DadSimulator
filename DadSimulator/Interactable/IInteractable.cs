using DadSimulator.Misc;

namespace DadSimulator.Interactable
{
    public struct Command
    {
        public char Key;
        public string Description;
    }

    public interface IInteractable : IPosition
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
        /// Get the available commands.
        /// </summary>
        /// <returns>Command which can be executed.</returns>
        string GetCommand();
        /// <summary>
        /// Command of GetCommand() is executed.
        /// </summary>
        void ExecuteCommand();
    }
}
