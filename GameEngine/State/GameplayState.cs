using GameEngine.Input;
using GameEngine.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private float _playerSpriteOffset;
        private Texture2D _bulletTexture;
        private List<BulletSprite> _bulletList;
        private bool _isShooting;
        private TimeSpan _lastShotTime = TimeSpan.Zero;
        private readonly TimeSpan _baseWeaponCooldown = TimeSpan.FromSeconds(0.2);
        private TimeSpan _weaponCooldown;

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GameplayInputMapper());
        }

        public override void LoadContent()
        {
            // scrolling background
            AddGameObject(new TerrainBackground(LoadTexture(BackgroundTexture)));

            // player sprite
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter));
            _playerSpriteOffset = _playerSprite.Width / 2f;
            int startingX = (_viewportWidth / 2) - (int)_playerSpriteOffset;
            int startingY = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(startingX, startingY);

            AddGameObject(_playerSprite);

            // player bullets
            _bulletTexture = LoadTexture(BulletTexture);
            _bulletList = new List<BulletSprite>();
            _weaponCooldown = _baseWeaponCooldown;
        }

        public override void HandleInput(GameTime gameTime)
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
                    ClampToWindow(_playerSprite);
                }
                if (cmd is GameplayInputCommand.PlayerMoveRight)
                {
                    _playerSprite.MoveRight();
                    ClampToWindow(_playerSprite);
                }
                if (cmd is GameplayInputCommand.PlayerShoots)
                {
                    Shoot(gameTime);
                }
            });
        }

        public override void Update(GameTime gameTime)
        {
            foreach (BulletSprite bullet in _bulletList)
            {
                bullet.MoveUp();
            }

            if (gameTime.TotalGameTime - _lastShotTime > _weaponCooldown)
            {
                _isShooting = false;
            }
        }

        private void Shoot(GameTime gameTime)
        {
            if (!_isShooting)
            {
                CreateBullets();
                _isShooting = true;
                _lastShotTime = gameTime.TotalGameTime;
            }
        }

        private void CreateBullets()
        {
            BulletSprite leftBullet = new BulletSprite(_bulletTexture);
            BulletSprite rightBullet = new BulletSprite(_bulletTexture);

            float bulletY = _playerSprite.Position.Y + 30;
            float leftBulletX = _playerSprite.Position.X + _playerSpriteOffset - 40;
            float rightBulletX = _playerSprite.Position.X + _playerSpriteOffset + 10;

            leftBullet.Position = new Vector2(leftBulletX, bulletY);
            rightBullet.Position = new Vector2(rightBulletX, bulletY);

            _bulletList.Add(leftBullet);
            _bulletList.Add(rightBullet);

            AddGameObject(leftBullet);
            AddGameObject(rightBullet);
        }

        private void ClampToWindow(BaseGameObject gameObject)
        {
            if (gameObject.Position.X < 0)
            {
                gameObject.Position = new Vector2(0, gameObject.Position.Y);
            }
            if (gameObject.Position.X > _viewportWidth - gameObject.Width)
            {
                gameObject.Position = new Vector2(_viewportWidth - gameObject.Width, gameObject.Position.Y);
            }
            if (gameObject.Position.Y < 0)
            {
                gameObject.Position = new Vector2(gameObject.Position.X, 0);
            }
            if (gameObject.Position.Y > _viewportHeight - gameObject.Height)
            {
                gameObject.Position = new Vector2(_playerSprite.Position.X, _viewportHeight - gameObject.Height);
            }
        }

        
        private const string PlayerFighter = "Fighter";
        private const string BackgroundTexture = "Barren";
        private const string BulletTexture = "bullet";
    }
}
