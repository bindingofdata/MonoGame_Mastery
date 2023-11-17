using Engine.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Particles
{
    public abstract class Emitter : BaseGameObject
    {
        private LinkedList<Particle> _activeParticles = new LinkedList<Particle>();
        private LinkedList<Particle> _inactiveParticles = new LinkedList<Particle>();
        private EmitterParticleState _emitterParticleState;
        private IEmitterType _emitterType;
        private int _particlesEmittedPerUpdate;
        private int _maxParticleCount;

        protected Emitter(EmitterOptions emitterOptions)
        {
            _emitterParticleState = emitterOptions.ParticleState;
            _emitterType = emitterOptions.EmitterType;
            _texture = emitterOptions.Texture;
            _particlesEmittedPerUpdate = emitterOptions.ParticlesPerUpdate;
            _maxParticleCount = emitterOptions.MaxParticleCount;
            Position = emitterOptions.Position;
        }

        public void Update(GameTime gameTime)
        {
            EmitParticles();

            LinkedListNode<Particle> currentNode = _activeParticles.First;
            while (currentNode != null)
            {
                if (!currentNode.Value.Update(gameTime))
                {
                    _activeParticles.Remove(currentNode);
                    _inactiveParticles.AddLast(currentNode.Value);
                }
                currentNode = currentNode.Next;
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);
            foreach (Particle particle in _activeParticles)
            {
                spriteBatch.Draw(
                    texture: _texture,
                    position: particle.Position,
                    sourceRectangle: sourceRectangle,
                    color: Color.White * particle.Opacity,
                    rotation: 0.0f,
                    origin: Vector2.Zero,
                    scale: particle.Scale,
                    effects: SpriteEffects.None,
                    layerDepth: ZIndex);
            }
        }

        private void EmitParticles()
        {
            if (_activeParticles.Count >= _maxParticleCount)
            {
                return;
            }

            int availableParticleCount = _maxParticleCount - _activeParticles.Count;
            int neededParticles = Math.Min(availableParticleCount, _particlesEmittedPerUpdate);
            int particlesToRecycle = Math.Min(_inactiveParticles.Count, neededParticles);
            int particlesToCreate = neededParticles - particlesToRecycle;

            for (int i = 0; i < particlesToRecycle; i++)
            {
                LinkedListNode<Particle> particleNode = _inactiveParticles.First;
                EmitNewParticle(particleNode.Value);
                _inactiveParticles.Remove(particleNode);
            }

            for (int i = 0; i < particlesToCreate; i++)
            {
                EmitNewParticle(new Particle());
            }
        }

        private void EmitNewParticle(Particle particle)
        {
            particle.Activate(new ParticleOptions()
            {
                Lifespan = _emitterParticleState.GenerateLifespan(),
                Position = _emitterType.GetParticlePosition(_position),
                Scale = _emitterParticleState.GenerateScale(),
                Rotation = _emitterParticleState.GenerateRotation(),
                Direction = _emitterType.GetParticleDirection(),
                Velocity = _emitterParticleState.GenerateVelocity(),
                Acceleration = _emitterParticleState.Acceleration,
                Gravity = _emitterParticleState.Gravity,
                Opacity = _emitterParticleState.Opacity,
                OpacityFadeRate = _emitterParticleState.OpacityFadeRate,
            });

            _activeParticles.AddLast(particle);
        }
    }

    public struct EmitterOptions
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public EmitterParticleState ParticleState { get; set; }
        public IEmitterType EmitterType { get; set; }
        public int ParticlesPerUpdate { get; set; }
        public int MaxParticleCount { get; set; }
    }
}
