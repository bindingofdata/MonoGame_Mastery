using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

using Microsoft.Xna.Framework;

namespace Engine.Particles
{
    public abstract class EmitterParticleState
    {
        private readonly Random _random = new Random();

        public abstract int MinLifespan { get; }
        public abstract int MaxLifespan { get; }

        public abstract float Velocity { get; }
        public abstract float VelocityDeviation { get; }
        public abstract float Acceleration { get; }
        public abstract Vector2 Gravity { get; }

        public abstract float Opacity { get; }
        public abstract float OpacityDeviation { get; }
        public abstract float OpacityFadeRate { get; }

        public abstract float Rotation { get; }
        public abstract float RotationDeviation { get; }

        public abstract float Scale { get; }
        public abstract float ScaleDeviation { get; }

        public int GenerateLifespan()
        {
            return _random.Next(MinLifespan, MaxLifespan);
        }

        public float GenerateVelocity()
        {
            return GenerateDeviatedFloat(Velocity, VelocityDeviation);
        }

        public float GenerateOpacity()
        {
            return GenerateDeviatedFloat(Opacity, OpacityDeviation);
        }

        public float GenerateRotation()
        {
            return GenerateDeviatedFloat(Rotation, RotationDeviation);
        }

        public float GenerateScale()
        {
            return GenerateDeviatedFloat(Scale, ScaleDeviation);
        }

        protected float GenerateDeviatedFloat(float value, float deviation)
        {
            float halfDeviation = deviation / 2.0f;
            return _random.NextFloat(value - halfDeviation, value + halfDeviation);
        }
    }
}
