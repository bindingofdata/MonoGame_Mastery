using Engine.Objects;
using Engine.State;

using FlyingShooter.States.GamePlay;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Objects.Pathing;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Objects
{
    internal sealed class ChopperSprite : BaseGameObject
    {
        // Chopper sprite sheet info
        private const int ChopperStartX = 0;
        private const int ChopperStartY = 0;
        private const int ChopperWidth = 44;
        private const int ChopperHeight = 98;

        // Chopper blade sprite sheet info
        private const int BladeStartX = 133;
        private const int BladeStartY = 0;
        private const int BladeWidth = 94;
        private const int BladeHeight = 94;

        // Chopper
        private const float BaseSpeed = 4.0f;
        private float _speed = BaseSpeed;
        private Vector2 _direction = Vector2.Zero;
        private int _framesSurvived = 0;
        private List<PathNode> _path;
        private int _life = 40;

        // Chopper flash when hit
        private int _hitAt = 0;

        // Blade rotation
        private const float BladeCenterX = 47f;
        private const float BladeCenterY = 47f;
        private const float BladeSpeed = 0.2f;
        private float _bladeAngle = 0.0f;

        // Blade positioning
        private const int ChopperBladeX = ChopperWidth / 2;
        private const int ChopperBladeY = 34;

        public ChopperSprite(Texture2D sprite, ChopperColor chopperColor, List<PathNode> path) : base(sprite)
        {
            _path = path;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (PathNode node in _path)
            {
                if (_framesSurvived >= node.StartingFrameNumber)
                {
                    _direction = node.Direction;
                }

                Position += _direction * _speed;
            }

            _framesSurvived++;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            // chopper
            Rectangle chopperRect = GetChopperSprite(ChopperColor.Yellow);
            Rectangle destChopperRect = new Rectangle(
                _position.ToPoint(), new Point(ChopperWidth, ChopperHeight));

            spriteBatch.Draw(_texture,
                destChopperRect,
                chopperRect,
                Color.White,
                MathHelper.Pi,
                new Vector2(ChopperBladeX, ChopperBladeY),
                SpriteEffects.None,
                0f);

            // blades
            Rectangle bladeRect = GetChopperBladeSprite();
            Rectangle destBladeRect = new Rectangle(
                _position.ToPoint(), new Point(BladeWidth, BladeHeight));

            spriteBatch.Draw(_texture,
                destBladeRect,
                bladeRect,
                Color.White,
                _bladeAngle,
                new Vector2(BladeCenterY, BladeCenterX),
                SpriteEffects.None,
                0f);
            _bladeAngle += BladeSpeed;
        }

        public override void OnNotify(BaseGameStateEvent eventType)
        {
            switch (eventType)
            {
                case GamePlayEvents.EnemyHitBy m:
                    JustHit(m.HitBy);
                    SendEvent(new GamePlayEvents.EnemyLostLife(_life));
                    break;
            }
        }

        private Rectangle GetChopperSprite(ChopperColor color)
        {
            switch (color)
            {
                case ChopperColor.Yellow:
                    return new Rectangle(
                        ChopperStartX,
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case ChopperColor.Red:
                    return new Rectangle(
                        ChopperStartX + ChopperWidth,
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case ChopperColor.Blue:
                    return new Rectangle(
                        ChopperStartX + (ChopperWidth * 2),
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case ChopperColor.Green:
                    return new Rectangle(
                        ChopperStartX,
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                case ChopperColor.Pink:
                    return new Rectangle(
                        ChopperStartX + ChopperWidth,
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                case ChopperColor.Purple:
                    return new Rectangle(
                        ChopperStartX + (ChopperWidth * 2),
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                default:
                    throw new InvalidEnumArgumentException($"Invalid enum value: \"{color}\"");
            }
        }

        public Rectangle GetChopperBladeSprite()
        {
            // Static blades for non-moving choppers
            if (_speed == 0)
            {
                return new Rectangle(
                        BladeStartX,
                        BladeStartY,
                        BladeWidth,
                        BladeHeight);
            }
            // Blurred blades for moving choppers
            else
            {
                return new Rectangle(
                        BladeStartX,
                        BladeStartY - BladeHeight,
                        BladeWidth,
                        BladeHeight);
            }
        }

        private void JustHit(IDamageDealer damageDealer)
        {
            _hitAt = 0;
            _life -= damageDealer.Damage;
        }
    }

    public enum ChopperColor
    {
        Yellow = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Pink = 4,
        Purple = 5,
    }
}
