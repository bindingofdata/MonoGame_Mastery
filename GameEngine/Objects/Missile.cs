using Engine.Objects;

using FlyingShooter.Particles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Objects
{
    internal sealed class Missile : BaseGameObject
    {
        private const float StartSpeed = 0.5f;
        private const float Acceleration = 0.15f;
        private float _speed = StartSpeed;

        // track scaled size
        private int _missileHeight;
        private int _missileWidth;

        private ExhaustEmitter _exhaustEmitter = new ExhaustEmitter(null, Vector2.Zero);

        public Missile(Texture2D missileSprite, Texture2D emitterTexture, Vector2 position) : base(missileSprite, position)
        {
            float ratio = (float)_texture.Height / _texture.Width;
            _missileWidth = 50;
            _missileHeight = (int)(_missileWidth * ratio);

            _exhaustEmitter = LoadEmitter(emitterTexture);
        }

        private Missile(Texture2D sprite) : base(sprite) { }

        public override Vector2 Position
        {
            set
            {
                _position = value;
                _exhaustEmitter.Position = GetEmitterPosition();
            }
        }

        public void Update(GameTime gameTime)
        {
            _exhaustEmitter.Update(gameTime);
            Position = new Vector2(Position.X, Position.Y - _speed);
            _speed += Acceleration;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            Rectangle canvas = new Rectangle((int)Position.X, (int)Position.Y, _missileWidth, _missileHeight);
            spriteBatch.Draw(_texture, canvas, Color.White);

            _exhaustEmitter.Render(spriteBatch);
        }

        private ExhaustEmitter LoadEmitter(Texture2D exhaustTexture)
        {
            return new ExhaustEmitter(exhaustTexture, GetEmitterPosition());
        }

        private Vector2 GetEmitterPosition()
        {
            return new Vector2(_position.X + 18, _position.Y + (_missileHeight - 10));
        }
    }
}
