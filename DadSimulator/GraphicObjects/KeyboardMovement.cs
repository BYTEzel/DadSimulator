using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace DadSimulator.GraphicObjects
{
    class KeyboardMovement : IMovementCommand
    {
        public List<Directions> GetDirections()
        {
            var commandList = new List<Directions>();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                commandList.Add(Directions.Up);
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                commandList.Add(Directions.Down);
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                commandList.Add(Directions.Left);
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                commandList.Add(Directions.Right);
            }

            return commandList;
        }
    }
}
