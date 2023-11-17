using Engine.Particles;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Particles
{
    public sealed class ExhaustEmitter : Emitter
    {
        public ExhaustEmitter(Texture2D texture, Vector2 position) : this(new EmitterOptions()
        {
            Texture = texture,
            Position = position,
            ParticleState = new ExhaustParticleState(),
            EmitterType = new ConeEmitterType(Direction, spread),
            ParticlesPerUpdate = particlesPerUpdate,
            MaxParticleCount = maxParticles,
        }) { }

        private ExhaustEmitter(EmitterOptions emitterOptions) : base(emitterOptions) { }

        private const int particlesPerUpdate = 10;
        private const int maxParticles = 1_000;
        private const float spread = 1.5f;
        private static Vector2 Direction = new Vector2(0.0f, 0.1f);
    }
}
