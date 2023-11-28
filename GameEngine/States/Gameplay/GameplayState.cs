using Engine.Input;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FlyingShooter.States.GamePlay;

using System;
using System.Collections.Generic;
using Engine.Objects;
using Engine;
using Engine.State;
using FlyingShooter.Objects;
using Microsoft.Xna.Framework.Audio;
using FlyingShooter.Particles;

namespace FlyingShooter.States
{
    internal sealed class GamePlayState : BaseGameState
    {
        // player
        private PlayerSprite _playerSprite;
        private float _playerSpriteOffset;

        // bullets
        private Texture2D _bulletTexture;
        private List<BulletSprite> _bulletList;
        private bool _isShootingBullet;
        private TimeSpan _lastBulletShotAt = TimeSpan.Zero;
        private readonly TimeSpan _baseBulletCooldown = TimeSpan.FromSeconds(0.2);
        private TimeSpan _bulletCooldown;

        // missiles
        private Texture2D _missileTexture;
        private Texture2D _exhaustTexture;
        private List<Missile> _missileList;
        private bool _isShootingMissile;
        private TimeSpan _lastMissleShotAt = TimeSpan.Zero;
        private readonly TimeSpan _baseMissileCooldown = TimeSpan.FromSeconds(1);
        private TimeSpan _missileCooldown;

        // explosion
        private const int MaxExplosionAge = 600; //10 seconds at 60 FPS
        private const int ExplosionActiveLength = 75;
        private Texture2D _explosionTexture;
        private List<Explosion> _explosionList = new List<Explosion>();

        // choppers
        private Texture2D _chopperTileMap;
        private ChopperGenerator _chopperGenerator;
        private List<ChopperSprite> _chopperList = new List<ChopperSprite>();

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new GamePlayInputMapper());
        }

        public override void LoadContent()
        {
            // scrolling background
            AddGameObject(new TerrainBackground(LoadTexture(TextureMap.GamePlayBG), Vector2.Zero));

            // player sprite
            _playerSprite = new PlayerSprite(LoadTexture(TextureMap.PlayerFighterTexture));
            _playerSpriteOffset = _playerSprite.Width / 2f;
            int startingX = _viewportWidth / 2 - (int)_playerSpriteOffset;
            int startingY = _viewportHeight - _playerSprite.Height - 30;
            _playerSprite.Position = new Vector2(startingX, startingY);

            AddGameObject(_playerSprite);

            // player bullets
            _bulletTexture = LoadTexture(TextureMap.BulletTexture);
            _bulletList = new List<BulletSprite>();
            _bulletCooldown = _baseBulletCooldown;
            _soundManager.RegisterSound(new GamePlayEvents.PlayerShootsBullets(), LoadSound(AudioMap.BulletSFX));

            // player missiles
            _missileTexture = LoadTexture(TextureMap.MissileTexture);
            _exhaustTexture = LoadTexture(TextureMap.ExhaustTexture);
            _missileList = new List<Missile>();
            _missileCooldown = _baseMissileCooldown;
            _soundManager.RegisterSound(new GamePlayEvents.PlayerShootsMissile(), LoadSound(AudioMap.MissileSFX), 0.4f, -0.2f, 0.0f);

            // explosions
            _explosionTexture = LoadTexture(TextureMap.ExplosionTexture);

            // enemies
            _chopperTileMap = LoadTexture(TextureMap.ChopperTileMap);
            _chopperGenerator = new ChopperGenerator(_chopperTileMap, 4, AddChopper);
            _chopperGenerator.GenerateChoppers();

            // bgm
            _soundManager.SetSoundtrack(new List<SoundEffectInstance>()
            {
                LoadMusic(AudioMap.GamePlayBGM_01).CreateInstance(),
                LoadMusic(AudioMap.GamePlayBGM_02).CreateInstance(),
            });
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommand.GameExit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
                if (cmd is GamePlayInputCommand.PlayerMoveLeft)
                {
                    _playerSprite.MoveLeft();
                    ClampToWindow(_playerSprite);
                }
                if (cmd is GamePlayInputCommand.PlayerMoveRight)
                {
                    _playerSprite.MoveRight();
                    ClampToWindow(_playerSprite);
                }
                if (cmd is GamePlayInputCommand.PlayerShoots)
                {
                    Shoot(gameTime);
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            // bullets
            foreach (BulletSprite bullet in _bulletList)
            {
                bullet.MoveUp();
            }
            _bulletList = CleanUpObjectList(_bulletList);

            if (gameTime.TotalGameTime - _lastBulletShotAt > _bulletCooldown)
            {
                _isShootingBullet = false;
            }

            // missiles
            foreach (Missile missile in _missileList)
            {
                missile.Update(gameTime);
            }
            _missileList = CleanUpObjectList(_missileList);

            if (gameTime.TotalGameTime - _lastMissleShotAt > _missileCooldown)
            {
                _isShootingMissile = false;
            }

            // choppers
            foreach (ChopperSprite chopper in _chopperList)
            {
                chopper.Update(gameTime);
            }
            _chopperList = CleanUpObjectList(_chopperList);
        }

        private void AddChopper(ChopperSprite chopper)
        {
            chopper.OnObjectChanged += _chopperSprite_OnObjectChanged;
            _chopperList.Add(chopper);
            AddGameObject(chopper);
        }

        private void _chopperSprite_OnObjectChanged(object sender, BaseGameStateEvent e)
        {
            ChopperSprite chopper = sender as ChopperSprite;
            switch (e)
            {
                case GamePlayEvents.EnemyLostLife ge:
                    if (ge.CurrentLife <= 0)
                    {
                        AddExplosion(new Vector2(chopper.Position.X - 40, chopper.Position.Y - 40));
                        chopper.Destroy();
                    }
                    break;
            }
        }

        private void AddExplosion(Vector2 position)
        {
            Explosion explosion = new Explosion(_explosionTexture, position);
            AddGameObject(explosion);
            _explosionList.Add(explosion);
        }

        private void UpdateExplosions(GameTime gameTime)
        {
            foreach (Explosion explosion in _explosionList)
            {
                explosion.Update(gameTime);

                if (explosion.Age > ExplosionActiveLength)
                {
                    explosion.Deactivate();
                }

                if (explosion.Age > MaxExplosionAge)
                {
                    explosion.Destroy();
                }
            }
        }

        private void Shoot(GameTime gameTime)
        {
            if (!_isShootingBullet)
            {
                CreateBullets();
                _isShootingBullet = true;
                _lastBulletShotAt = gameTime.TotalGameTime;

                NotifyEvent(new GamePlayEvents.PlayerShootsBullets());
            }

            if (!_isShootingMissile)
            {
                CreateMissiles();
                _isShootingMissile = true;
                _lastMissleShotAt = gameTime.TotalGameTime;

                NotifyEvent(new GamePlayEvents.PlayerShootsMissile());
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

        private void CreateMissiles()
        {
            Missile missile = new Missile(_missileTexture,
                _exhaustTexture,
                new Vector2(_playerSprite.Position.X + 33, _playerSprite.Position.Y - 25));

            _missileList.Add(missile);
            AddGameObject(missile);
        }

        private List<T> CleanUpObjectList<T>(List<T> projectiles) where T : BaseGameObject
        {
            List<T> activeProjectiles = new List<T>();
            foreach (T projectile in projectiles)
            {
                if (projectile.Position.Y > -50)
                {
                    activeProjectiles.Add(projectile);
                }
                else
                {
                    RemoveGameObject(projectile);
                }
            }

            return activeProjectiles;
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
    }
}
