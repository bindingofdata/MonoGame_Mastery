using GameEngine.Objects;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.State
{
    internal class SplashState : BaseGameState
    {
        public override void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SwitchState(new GameplayState());
            }
        }

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture("splash")));
        }
    }
}
