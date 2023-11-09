using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    internal class TerrainBackground : BaseGameObject
    {
        public TerrainBackground(Texture2D texture)
        {
            _texture = texture;
            _position = Vector2.Zero;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            Viewport viewport = spriteBatch.GraphicsDevice.Viewport;
            Rectangle sourceRectangle = new Rectangle(0, 0 ,_texture.Width, _texture.Height);

            int rowCount = (viewport.Height / _texture.Height) + 1;
            int columnCount = (viewport.Width / _texture.Width) + 1;
            for (int row = -1; row < rowCount; row++)
            {
                int y = (int)Position.Y + (row * _texture.Height);
                for (int column = 0; column < columnCount; column++)
                {
                    int x = (int)_position.X + (column * _texture.Width);
                    Rectangle destRectangle = new Rectangle(x, y, _texture.Width, _texture.Height);
                    spriteBatch.Draw(_texture, destRectangle, sourceRectangle, Color.White);
                }
            }

            _position.Y = (int)(Position.Y + SCROLLING_SPEED) % _texture.Height;
        }

        private const float SCROLLING_SPEED = 2.0f;
    }
}
