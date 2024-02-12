using FlyingShooter.Objects;
using FlyingShooter.Particles;
using FlyingShooter.States.Dev;

using Microsoft.Xna.Framework;

using Engine.Particles;
using Engine.Input;
using Engine.State;
using Engine.Objects;
using System.Collections.Generic;
using System.Reflection;

namespace FlyingShooter.States
{
    public sealed class DevState : BaseGameState
    {
        private Exhaust _exhaustEmitter;
        private IList<Missile> _missiles = new List<Missile>();
        private PlayerSprite _playerSprite;

        public override void LoadContent()
        {
            Vector2 exhaustPosition = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _exhaustEmitter = new Exhaust(LoadTexture(TextureMap.ExhaustTexture), exhaustPosition);
            AddGameObject(_exhaustEmitter);

            _playerSprite = new PlayerSprite(LoadTexture(TextureMap.PlayerFighterTexture));
            _playerSprite.Position = new Vector2(500, 500);
            AddGameObject(_playerSprite);
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }

                if (cmd is DevInputCommand.DevShoot)
                {
                    Missile missile = new Missile(
                        LoadTexture(TextureMap.MissileTexture),
                        LoadTexture(TextureMap.ExhaustTexture));
                    missile.Position = new Vector2(_playerSprite.Position.X, _playerSprite.Position.Y - 25);

                    _missiles.Add(missile);
                    AddGameObject(missile);
                }
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
            _exhaustEmitter.Update(gameTime);

            if (_exhaustEmitter.Position.Y < -200)
            {
                RemoveGameObject(_exhaustEmitter);
            }

            List<Missile> deadMissiles = new List<Missile>();
            foreach (Missile missile in _missiles)
            {
                missile.Update(gameTime);

                if (missile.Position.Y < -100)
                {
                    RemoveGameObject(missile);
                    deadMissiles.Add(missile);
                }
            }

            foreach (Missile missile in deadMissiles)
            {
                _missiles.Remove(missile);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }
    }
}
