using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DadSimulator.GraphicObjects
{
    class KeyboardUserCommand : IUserCommand
    {
        private const Keys m_actionKey = Keys.E;

        public char GetActionKey()
        {
            return m_actionKey.ToString().ToCharArray()[0];
        }

        public List<Directions> GetDirections()
        {
            var commandList = new List<Directions>();
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                commandList.Add(Directions.Up);
            }

            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                commandList.Add(Directions.Down);
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                commandList.Add(Directions.Left);
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                commandList.Add(Directions.Right);
            }

            return commandList;
        }

        public bool IsActionKeyPressed()
        {
            var keyboard = Keyboard.GetState();
            return keyboard.IsKeyDown(m_actionKey);
        }
    }
}
