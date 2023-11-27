using Engine.Objects;
using Engine.State;

using FlyingShooter.States.GamePlay;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.Objects.VehicleSprites
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

        // Blade rotation
        private const float BladeCenterX = 47f;
        private const float BladeCenterY = 47f;
        private const float BladeSpeed = 0.2f;
        private float _bladeAngle = 0.0f;

        // Blade positioning
        private const int ChopperBladeX = ChopperWidth / 2;
        private const int ChopperBladeY = 34;

        // Chopper life
        private int _age = 0;
        private int _life = 40;

        // flash when hit
        private int _hitAt = 0;

        public ChopperSprite(Texture2D sprite) : base(sprite)
        {
            _texture = sprite;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            // chopper
            Rectangle chopperRect = GetChopperSprite(0);
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
            Rectangle bladeRect = GetChopperBladeSprite(0);
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

        public Rectangle GetChopperSprite(int index)
        {
            switch (index)
            {
                case 0:
                    return new Rectangle(
                        ChopperStartX,
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case 1:
                    return new Rectangle(
                        ChopperStartX + ChopperWidth,
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case 2:
                    return new Rectangle(
                        ChopperStartX + (ChopperWidth * 2),
                        ChopperStartY,
                        ChopperWidth,
                        ChopperHeight);
                case 3:
                    return new Rectangle(
                        ChopperStartX,
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                case 4:
                    return new Rectangle(
                        ChopperStartX + ChopperWidth,
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                case 5:
                    return new Rectangle(
                        ChopperStartX + (ChopperWidth * 2),
                        ChopperStartY - ChopperHeight,
                        ChopperWidth,
                        ChopperHeight);
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), "Value must be between 0 and 5");
            }
        }

        public Rectangle GetChopperBladeSprite(int index)
        {
            switch (index)
            {
                case 0:
                    return new Rectangle(
                        BladeStartX,
                        BladeStartY,
                        BladeWidth,
                        BladeHeight);
                case 1:
                    return new Rectangle(
                        BladeStartX,
                        BladeStartY - BladeHeight,
                        BladeWidth,
                        BladeHeight);
                default:
                    throw new ArgumentOutOfRangeException(nameof(index), "Value must be between 0 or 1");

            }
        }

        private void JustHit(IDamageDealer damageDealer)
        {
            _hitAt = 0;
            _life -= damageDealer.Damage;
        }
    }
}
