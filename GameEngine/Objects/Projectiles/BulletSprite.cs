using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Engine.Objects;

namespace FlyingShooter.Objects
{
    internal sealed class BulletSprite : BaseGameObject
    {
        public BulletSprite(Texture2D sprite, Vector2 position) : base(sprite, position) { }

        public BulletSprite(Texture2D sprite) : base(sprite) { }

        public void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y - BASE_SPEED);
        }

        private const float BASE_SPEED = 10.0f;
    }
}
