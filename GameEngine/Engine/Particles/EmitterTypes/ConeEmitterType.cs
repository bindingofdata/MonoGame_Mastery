using Engine;
using Engine.Particles;
using Microsoft.Xna.Framework;

using System;

namespace Engine.Particles
{
    public sealed class ConeEmitterType : IEmitterType
    {
        public Vector2 Direction { get; private set; }
        public float Spread { get; private set; }

        private Random _random = new Random();

        public ConeEmitterType(Vector2 direction, float spread)
        {
            Direction = direction;
            Spread = spread;
        }

        public Vector2 GetParticleDirection()
        {
            float angle = (float)Math.Atan2(Direction.Y, Direction.X);
            float halfSpread = Spread / 2.0f;
            float newAngle = _random.NextFloat(angle - halfSpread, angle + halfSpread);
            Vector2 particleDirection = new Vector2(
                (float)Math.Cos(newAngle),
                (float)Math.Sin(newAngle));
            particleDirection.Normalize();
            return particleDirection;
        }

        public Vector2 GetParticlePosition(Vector2 emitterPosition)
        {
            return new Vector2(emitterPosition.X, emitterPosition.Y);
        }
    }
}
