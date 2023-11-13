using Engine.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Engine
{
    public sealed class MainGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;
        private BaseGameState _currentGameState;
        private BaseGameState _startingGameState;
        private int _gameWidth;
        private int _gameHeight;
        private float _gameRatio;

        public MainGame(int width, int height, BaseGameState initialGameState)
        {
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _gameWidth = width;
            _gameHeight = height;
            _gameRatio = (float)width / height;

            _startingGameState = initialGameState;
        }

        #region Initialization/Loading
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _gameWidth;
            _graphics.PreferredBackBufferHeight = _gameHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
                _gameWidth, _gameHeight, false, SurfaceFormat.Color,
                DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            float variance = 0.5f;
            float actualAspectRatio = (float)Window.ClientBounds.Width / Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= _gameRatio)
            {
                int presentHeight = (int)(Window.ClientBounds.Width / _gameRatio + variance);
                int barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                int presentWidth = (int)(Window.ClientBounds.Height * _gameRatio + variance);
                int barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SwitchGameState(_startingGameState);
        }
        #endregion

        #region Update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentGameState.HandleInput(gameTime);
            _currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (_currentGameState != null)
            {
                _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
                _currentGameState.UnloadContent();
            }

            _currentGameState = gameState;
            _currentGameState.Initialize(Content, _graphics.GraphicsDevice.Viewport.Height, _graphics.GraphicsDevice.Viewport.Width);
            _currentGameState.LoadContent();
            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void _currentGameState_OnEventNotification(object sender, Events gameEvent)
        {
            switch (gameEvent)
            {
                case Events.GAME_QUIT:
                    Exit();
                    break;
            }
        }
        #endregion

        protected override void Draw(GameTime gameTime)
        {
            // render to the render target
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _currentGameState.Render(_spriteBatch);
            _spriteBatch.End();

            // now render the scaled content
            _graphics.GraphicsDevice.SetRenderTarget(null);
            _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}