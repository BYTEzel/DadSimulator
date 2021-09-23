using System.Collections.Generic;

namespace DadSimulator.Collider
{
    public interface ICollidableCollection
    {
        List<ICollidable> GetCollectibleList();
    }
}
