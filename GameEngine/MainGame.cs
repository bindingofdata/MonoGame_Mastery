using GameEngine.State;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace GameEngine
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;
        private Rectangle _renderScaleRectangle;
        private BaseGameState _currentGameState;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region Initialization/Loading
        protected override void Initialize()
        {
            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
                BASE_WIDTH, BASE_HEIGHT,false, SurfaceFormat.Color,
                DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();

            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            float variance = 0.5f;
            float actualAspectRatio = (float)Window.ClientBounds.Width / Window.ClientBounds.Height;

            Rectangle scaleRectangle;

            if (actualAspectRatio <= BASE_ASPECT_RATIO)
            {
                int presentHeight = (int)((Window.ClientBounds.Width / BASE_ASPECT_RATIO) + variance);
                int barHeight = (Window.ClientBounds.Height - presentHeight) / 2;
                scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
            }
            else
            {
                int presentWidth = (int)((Window.ClientBounds.Height * BASE_ASPECT_RATIO) + variance);
                int barWidth = (Window.ClientBounds.Width - presentWidth) / 2;
                scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
            }

            return scaleRectangle;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        #endregion

        #region Update
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            _currentGameState?.UnloadContent(Content);

            _currentGameState = gameState;
            _currentGameState.LoadContent(Content);
            _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            throw new NotImplementedException();
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

        private const int BASE_WIDTH = 640;
        private const int BASE_HEIGHT = 480;
        private const float BASE_ASPECT_RATIO = (float)BASE_WIDTH / (float)BASE_HEIGHT;
    }
}