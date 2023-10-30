using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.State
{
    internal class GameplayState : BaseGameState
    {
        public override void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                NotifyEvent(Events.GAME_QUIT);
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }
    }
}
