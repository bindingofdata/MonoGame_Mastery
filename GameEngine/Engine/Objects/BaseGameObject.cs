using Engine.State;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects
{
    public abstract class BaseGameObject
    {
        protected Vector2 _position;
        public virtual Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public int ZIndex { get; set; }
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        public BaseGameObject(Texture2D sprite, Vector2 position) : this(sprite)
        {
            Position = position;
        }

        public BaseGameObject(Texture2D sprite)
        {
            _texture = sprite;
        }

        public virtual void OnNotify(BaseGameStateEvent eventType) { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        protected Texture2D _texture;
    }
}
