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
    internal sealed class Missile : BaseGameObject, IDamageDealer
    {
        private const float StartSpeed = 0.5f;
        private const float Acceleration = 0.15f;
        private float _speed = StartSpeed;

        // track scaled size
        private int _missileHeight;
        private int _missileWidth;
        private const int BaseMissileWidth = 50;

        // Bounding Box Defaults
        private const int BoundingBoxX = 352;
        private const int BoundingBoxY = 7;
        private const int BoundingBoxWidth = 150;
        private const int BoundingBoxHeight = 500;

        private Exhaust _exhaustEmitter = new Exhaust(null, Vector2.Zero);

        public int Damage => 25;

        public Missile(Texture2D missileSprite, Texture2D emitterTexture) : base(missileSprite)
        {
            _exhaustEmitter = LoadEmitter(emitterTexture);

            float ratio = (float)_texture.Height / _texture.Width;
            _missileWidth = BaseMissileWidth;
            _missileHeight = (int)(_missileWidth * ratio);

            ratio = (float)_missileWidth / _texture.Width;
            AddBoundingBox(new Engine.Objects.BoundingBox(
                new Vector2(BoundingBoxX * ratio, BoundingBoxY * ratio),
                BoundingBoxWidth * ratio, BoundingBoxHeight * ratio));
        }

        private Missile(Texture2D sprite) : base(sprite) { }

        public override Vector2 Position
        {
            set
            {
                _exhaustEmitter.Position = GetEmitterPosition();
                base.Position = value;
            }
        }

        public override void Update(GameTime gameTime)
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

        private Exhaust LoadEmitter(Texture2D exhaustTexture)
        {
            return new Exhaust(exhaustTexture, GetEmitterPosition());
        }

        private Vector2 GetEmitterPosition()
        {
            return new Vector2(_position.X + 18, _position.Y + (_missileHeight - 10));
        }
    }
}
