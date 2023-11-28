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
    public sealed class Exhaust : Emitter
    {
        public Exhaust(Texture2D texture, Vector2 position) : this(new EmitterOptions()
        {
            Texture = texture,
            Position = position,
            ParticleState = new ExhaustParticleState(),
            EmitterType = new ConeEmitterType(Direction, spread),
            ParticlesPerUpdate = particlesPerUpdate,
            MaxParticleCount = maxParticles,
        }) { }

        private Exhaust(EmitterOptions emitterOptions) : base(emitterOptions) { }

        private const int particlesPerUpdate = 10;
        private const int maxParticles = 1_000;
        private const float spread = 1.5f;
        private static Vector2 Direction = new Vector2(0.0f, 0.1f);
    }

    public sealed class ExhaustParticleState : EmitterParticleState
    {
        public override int MinLifespan => 60; // in seconds

        public override int MaxLifespan => 90;

        public override float Velocity => 4.0f;

        public override float VelocityDeviation => 1.0f;

        public override float Acceleration => 0.8f;

        public override Vector2 Gravity => Vector2.Zero;

        public override float Opacity => 0.4f;

        public override float OpacityDeviation => 0.1f;

        public override float OpacityFadeRate => 0.5f;

        public override float Rotation => 0.0f;

        public override float RotationDeviation => 0.0f;

        public override float Scale => 0.1f;

        public override float ScaleDeviation => 0.05f;
    }
}
