using GameEngine.Input;
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
    internal sealed class GameplayState : BaseGameState
    {
        private PlayerSprite _playerSprite;
        public override void HandleInput()
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameplayInputCommand.GameExit)
                {
                    NotifyEvent(Events.GAME_QUIT);
                }
                if (cmd is GameplayInputCommand.PlayerMoveLeft)
                {
                    _playerSprite.MoveLeft();
                }
                if (cmd is GameplayInputCommand.PlayerMoveRight)
                {
                    _playerSprite.MoveRight();
                }
                if (cmd is GameplayInputCommand.PlayerShoots)
                {
                    _playerSprite.Shoot();
                }
            });
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

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }

        private const string PlayerFighter = "Fighter";
        private const string BackgroundTexture = "Barren";
    }
}
