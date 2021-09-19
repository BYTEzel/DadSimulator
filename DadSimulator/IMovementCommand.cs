using System.Collections.Generic;

namespace DadSimulator
{
    enum Directions { Up, Right, Down, Left}
    interface IMovementCommand
    {
        List<Directions> GetDirections();
    }
}
