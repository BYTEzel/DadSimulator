using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public interface IInteractableCollection
    {
        /// <summary>
        /// Get all interactable objects available in the scene.
        /// </summary>
        /// <returns>All interactable objects.</returns>
        List<IInteractable> GetInteractables();
    }
}
