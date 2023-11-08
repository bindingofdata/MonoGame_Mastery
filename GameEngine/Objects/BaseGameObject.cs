using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects
{
    internal abstract class BaseGameObject
    {
        public virtual void OnNotify(Events eventType) { }

        public int zIndex;

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Vector2.One, Color.White);
        }

        protected Texture2D _texture;
    }
}
