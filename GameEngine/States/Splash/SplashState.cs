using Engine.State;
using FlyingShooter.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States
{
    internal sealed class SplashState : BaseGameState
    {
        public override void HandleInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                SwitchState(new GamePlayState());
            }
        }

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture(TextureMap.SplashScreenBG)));
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            
        }

        protected override void SetInputManager()
        {

        }
    }
}
