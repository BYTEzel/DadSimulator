using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    public enum Directions { Up, Right, Down, Left}
    
    public interface IUserCommand
    {
        List<Directions> GetDirections();
        bool IsActionKeyPressed();
        char GetActionKey();
    }
}
