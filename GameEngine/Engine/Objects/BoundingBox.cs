using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Objects
{
    public class BoundingBox
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }

        public BoundingBox(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(BoundingBox otherBox)
        {
            if (Position.X < otherBox.Position.X + otherBox.Width &&
                Position.X + Width > otherBox.Position.X &&
                Position.Y < otherBox.Position.Y + otherBox.Height &&
                Position.Y + Height > otherBox.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
