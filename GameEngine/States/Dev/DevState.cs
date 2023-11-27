using FlyingShooter.Particles;
using FlyingShooter.States.Dev;

using Microsoft.Xna.Framework;

using Engine.Particles;
using Engine.Input;
using Engine.State;
using Engine.Objects;
using FlyingShooter.Objects;

namespace FlyingShooter.States
{
    public sealed class DevState : BaseGameState
    {
        private Missile _testObject;

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is DevInputCommand.DevQuit)
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
            });
        }

        public override void LoadContent()
        {
            Vector2 objectPosition = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _testObject = new Missile(LoadTexture(MissileTexture), objectPosition, LoadTexture(ExhaustTexture));
            //_testObject = new ExhaustEmitter(LoadTexture(ExhaustTexture), objectPosition);

            AddGameObject(_testObject);
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _testObject.Position = new Vector2(_testObject.Position.X, _testObject.Position.Y - 3f);
            _testObject.Update(gameTime);

            if (_testObject.Position.Y < -200)
            {
                RemoveGameObject(_testObject);
                LoadContent();
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }

        private const string ExhaustTexture = "Cloud001";
        private const string PlayerFighter = "Fighter";
        private const string BackgroundTexture = "Barren";
        private const string BulletTexture = "bullet";
        private const string MissileTexture = "Missile05";
    }
}
