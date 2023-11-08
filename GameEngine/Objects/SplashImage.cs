using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    internal class SplashImage : BaseGameObject
    {
        public SplashImage(Texture2D image)
        {
            _texture = image;
        }
    }
}
