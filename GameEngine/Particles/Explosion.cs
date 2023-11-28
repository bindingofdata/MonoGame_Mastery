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
    public sealed class Explosion : Emitter
    {
        public Explosion(Texture2D texture, Vector2 position) : this(new EmitterOptions()
        {
            Texture = texture,
            Position = position,
            ParticleState = new ExplosionParticleState(),
            EmitterType = new CircleEmitterType(radius),
            ParticlesPerUpdate = particlesPerUpdate,
            MaxParticleCount = maxParticles,
        }) { }

        public Explosion(EmitterOptions emitterOptions) : base(emitterOptions) { }

        private const int particlesPerUpdate = 2;
        private const int maxParticles = 200;
        private const float radius = 50f;
    }

    public sealed class ExplosionParticleState : EmitterParticleState
    {
        public override int MinLifespan => 180;

        public override int MaxLifespan => 240;

        public override float Velocity => 2.0f;

        public override float VelocityDeviation => 0.0f;

        public override float Acceleration => 0.999f;

        public override Vector2 Gravity => new Vector2(0,1);

        public override float Opacity => 0.4f;

        public override float OpacityDeviation => 0.1f;

        public override float OpacityFadeRate => 0.92f;

        public override float Rotation => 0.0f;

        public override float RotationDeviation => 0.0f;

        public override float Scale => 0.5f;

        public override float ScaleDeviation => 0.1f;
    }
}
