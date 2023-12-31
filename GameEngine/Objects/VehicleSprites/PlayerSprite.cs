﻿using Engine.Objects;
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
        public PlayerSprite(Texture2D sprite) : base(sprite) { }

        public void MoveLeft()
        {
            _position.X -= BASE_SPEED;
        }

        public void MoveRight()
        {
            _position.X += BASE_SPEED;
        }

        private const float BASE_SPEED = 10.0f;
    }
}
