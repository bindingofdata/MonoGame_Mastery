using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Particles
{
    public sealed class CircleEmitterType : IEmitterType
    {
        public float Radius { get; private set; }

        private Random random = new Random();

        public CircleEmitterType(float radius)
        {
            Radius = radius;
        }

        public Vector2 GetParticleDirection()
        {
            return Vector2.Zero;
        }

        public Vector2 GetParticlePosition(Vector2 emitterPosition)
        {
            float newAngle = random.NextFloat(0, 2 * MathHelper.Pi);
            Vector2 positionVector = new Vector2(
                (float)Math.Cos(newAngle),
                (float)Math.Sin(newAngle));
            positionVector.Normalize();

            float disatance = random.NextFloat(0, Radius);
            Vector2 position = positionVector * disatance;

            return new Vector2(
                emitterPosition.X + position.X,
                emitterPosition.Y + position.Y);
        }
    }
}
