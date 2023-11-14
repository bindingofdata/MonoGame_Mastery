using Microsoft.Xna.Framework.Input;

using System;

namespace Engine.Input
{
    public sealed class InputManager
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
