using FlyingShooter.Particles;
using FlyingShooter.States.Dev;

using Microsoft.Xna.Framework;

using Engine.Particles;
using Engine.Input;
using Engine.State;

namespace FlyingShooter.States
{
    public sealed class DevState : BaseGameState
    {
        private ExhaustEmitter _exhaustEmitter;

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
            Vector2 exhaustPosition = new Vector2(_viewportWidth / 2, _viewportHeight / 2);
            _exhaustEmitter = new ExhaustEmitter(
                LoadTexture(ExhaustTexture),
                exhaustPosition);

            AddGameObject(_exhaustEmitter);
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
            _exhaustEmitter.Update(gameTime);

            if (_exhaustEmitter.Position.Y < -200)
            {
                //RemoveGameObject(_exhaustEmitter);
                _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _viewportHeight / 2);
            }
        }

        protected override void SetInputManager()
        {
            InputManager = new InputManager(new DevInputMapper());
        }

        private const string ExhaustTexture = "Cloud001";
    }
}
