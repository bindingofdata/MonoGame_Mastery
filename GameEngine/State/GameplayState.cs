using GameEngine.Objects;

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
        private PlayerSprite _playerSprite;
        public override void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                NotifyEvent(Events.GAME_QUIT);
            }
        }

        public override void LoadContent()
        {
            // scrolling background
            AddGameObject(new TerrainBackground(LoadTexture(BackgroundTexture)));

            // player sprite
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter));
            int startingX = (_viewportWidth / 2) - (_playerSprite.Width / 2);
            int startingY = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(startingX, startingY);

            AddGameObject(_playerSprite);
        }

        private const string PlayerFighter = "Fighter";
        private const string BackgroundTexture = "Barren";
    }
}
