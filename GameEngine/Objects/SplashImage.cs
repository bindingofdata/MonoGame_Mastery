using Engine.Objects;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Objects
{
    internal sealed class SplashImage : BaseGameObject
    {
        public SplashImage(Texture2D image)
        {
            _texture = image;
        }
    }
}
