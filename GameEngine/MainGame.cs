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

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

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

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private const int BASE_WIDTH = 640;
        private const int BASE_HEIGHT = 480;
        private const float BASE_ASPECT_RATIO = (float)BASE_WIDTH / (float)BASE_HEIGHT;
    }
}