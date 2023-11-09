using Microsoft.VisualBasic;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Input
{
    internal class InputManager
    {
        private BaseInputMapper _commandMap;
        public InputManager(BaseInputMapper commandMap)
        {
            _commandMap = commandMap;
        }

        public void GetCommands(Action<BaseInputCommand> actOnCmd)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (BaseInputCommand command in _commandMap.GetKeyboardState(keyboardState))
            {
                actOnCmd(command);
            }

            MouseState mouseState = Mouse.GetState();
            foreach (BaseInputCommand command in _commandMap.GetMouseState(mouseState))
            {
                actOnCmd(command);
            }

            GamePadState gamePadState = GamePad.GetState(0);
            foreach (BaseInputCommand command in _commandMap.GetGamePadState(gamePadState))
            {
                actOnCmd(command);
            }
        }
    }
}
