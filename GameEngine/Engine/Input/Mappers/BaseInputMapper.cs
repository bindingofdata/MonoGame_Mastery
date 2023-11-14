using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

namespace Engine.Input
{
    public abstract class BaseInputMapper
    {
        public virtual IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyboardState)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState mouseState)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetGamePadState(GamePadState gamePadState)
        {
            return new List<BaseInputCommand>();
        }
    }
}
