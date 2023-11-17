using Engine.Particles;

using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Particles
{
    public sealed class ExhaustParticleState : EmitterParticleState
    {
        public override int MinLifespan => 1; // in seconds

        public override int MaxLifespan => 3;

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
