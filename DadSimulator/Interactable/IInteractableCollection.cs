using System.Collections.Generic;

namespace DadSimulator.Interactable
{
    public interface IInteractableCollection
    {
        List<IInteractable> GetInteractables();
    }
}
