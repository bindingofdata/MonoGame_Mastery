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
    internal sealed class BulletSprite : BaseGameObject, IDamageDealer
    {
        // Bounding Box Defaults
        private const int BoundingBoxX = 9;
        private const int BoundingBoxY = 4;
        private const int BoundingBoxWidth = 10;
        private const int BoundingBoxHeight = 22;

        public int Damage => 10;

        public BulletSprite(Texture2D sprite, Vector2 position) : base(sprite, position)
        {
            InitializeBoundingBox();
        }

        public BulletSprite(Texture2D sprite) : base(sprite)
        {
            InitializeBoundingBox();
        }

        public void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y - BASE_SPEED);
        }

        private void InitializeBoundingBox()
        {
            AddBoundingBox(new Engine.Objects.BoundingBox(
                new Vector2(BoundingBoxX, BoundingBoxY),
                BoundingBoxWidth,
                BoundingBoxHeight));
        }

        private const float BASE_SPEED = 10.0f;
    }
}
