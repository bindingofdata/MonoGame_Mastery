using Engine.State;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Engine.Objects
{
    public abstract class BaseGameObject
    {
        // Sprite
        protected Texture2D _texture;
        protected Texture2D _boundingBoxTexture;
        public int Width => _texture.Width;
        public int Height => _texture.Height;

        // Location
        protected Vector2 _position = Vector2.One;
        public virtual Vector2 Position
        {
            get { return _position; }
            set
            {
                float deltaX = value.X - _position.X;
                float deltaY = value.Y - _position.Y;
                _position = value;

                foreach (BoundingBox box in _boundingBoxes)
                {
                    box.Position = new Vector2(box.Position.X + deltaX, box.Position.Y + deltaY);
                }
            }
        }
        public int ZIndex { get; set; }

        // Bounding Boxes
        protected List<BoundingBox> _boundingBoxes = new List<BoundingBox>();
        public List<BoundingBox> BoundingBoxes { get { return _boundingBoxes; } }

        public event EventHandler<BaseGameStateEvent> OnObjectChanged;

        public bool Destroyed { get; private set; }

        public BaseGameObject(Texture2D sprite, Vector2 position) : this(sprite)
        {
            _position = position;
        }

        public BaseGameObject(Texture2D sprite)
        {
            _texture = sprite;
        }

        public virtual void OnNotify(BaseGameStateEvent eventType) { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void Destroy()
        {
            Destroyed = true;
        }

        public void SendEvent(BaseGameStateEvent e)
        {
            OnObjectChanged?.Invoke(this, e);
        }

        public void AddBoundingBox(BoundingBox box)
        {
            _boundingBoxes.Add(box);
        }

        public void RenderBoundingBoxes(SpriteBatch spriteBatch)
        {
            if (_boundingBoxTexture == null)
            {
                CreateBoundingBoxTexture(spriteBatch.GraphicsDevice);
            }

            foreach (BoundingBox box in _boundingBoxes)
            {
                spriteBatch.Draw(_boundingBoxTexture, box.Rectangle, Color.Red);
            }
        }

        private void CreateBoundingBoxTexture(GraphicsDevice graphicsDevice)
        {
            _boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1);
            _boundingBoxTexture.SetData<Color>(new Color[] { Color.Red });
        }
    }
}
