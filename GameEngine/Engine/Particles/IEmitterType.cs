using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Particles
{
    public interface IEmitterType
    {
        Vector2 GetParticleDirection();
        Vector2 GetParticlePosition(Vector2 emitterPosition);
    }
}
