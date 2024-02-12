using Engine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Objects
{
    internal sealed class PlayerSprite : BaseGameObject
    {
        // Bounding Box Defaults
        private const int BoundingBox01X = 29;
        private const int BoundingBox01Y = 2;
        private const int BoundingBox01Width = 57;
        private const int BoundingBox01Height = 147;

        private const int BoundingBox02X = 2;
        private const int BoundingBox02Y = 77;
        private const int BoundingBox02Width = 111;
        private const int BoundingBox02Height = 37;

        public PlayerSprite(Texture2D sprite) : base(sprite)
        {
            AddBoundingBox(new Engine.Objects.BoundingBox(
                new Vector2(BoundingBox01X, BoundingBox01Y),
                BoundingBox01Width,
                BoundingBox01Height));

            AddBoundingBox(new Engine.Objects.BoundingBox(
                new Vector2(BoundingBox02X, BoundingBox02Y),
                BoundingBox02Width,
                BoundingBox02Height));
        }

        public void MoveLeft()
        {
            Position = new Vector2(Position.X - BASE_SPEED, Position.Y);
        }

        public void MoveRight()
        {
            Position = new Vector2(Position.X + BASE_SPEED, Position.Y);
        }

        private const float BASE_SPEED = 10.0f;
    }
}
