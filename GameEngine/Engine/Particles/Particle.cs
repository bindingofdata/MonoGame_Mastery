using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Particles
{
    public sealed class Particle
    {
        public Vector2 Position { get; private set; }
        public float Scale { get; private set; }
        public float Opacity { get; private set; }

        private int _lifespan;
        private int _age;
        private Vector2 _direction;
        private Vector2 _gravity;
        private float _velocity;
        private float _acceleration;
        private float _rotation;
        private float _opacityFadeRate;

        public Particle() { }

        public void Activate(ParticleOptions particleOptions)
        {
            _lifespan = particleOptions.Lifespan;
            _direction = particleOptions.Direction;
            _gravity = particleOptions.Gravity;
            _velocity = particleOptions.Velocity;
            _acceleration = particleOptions.Acceleration;
            _rotation = particleOptions.Rotation;
            _opacityFadeRate = particleOptions.OpacityFadeRate;
            _age = 0;

            Position = particleOptions.Position;
            Opacity = particleOptions.Opacity;
            Scale = particleOptions.Scale;
        }

        /// <summary>
        /// Updates the particle's attributes and disables old particles.
        /// </summary>
        /// <param name="gameTime">Current GameTime.</param>
        /// <returns>True if the particle is still active.</returns>
        public bool Update(GameTime gameTime)
        {
            _age++;
            if (_age > _lifespan)
                return false;

            _velocity *= _acceleration;
            _direction += _gravity;
            Position += _direction * _velocity;
            Opacity *= _opacityFadeRate;

            return true;
        }
    }

    public struct ParticleOptions
    {
        public int Lifespan { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Gravity { get; set; }
        public float Velocity { get; set; }
        public float Acceleration { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public float Opacity { get; set; }
        public float OpacityFadeRate { get; set; }
    }
}
