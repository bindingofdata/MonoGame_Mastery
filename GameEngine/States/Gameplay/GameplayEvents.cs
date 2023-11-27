using Engine.Objects;
using Engine.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States.GamePlay
{
    public class GamePlayEvents : BaseGameStateEvent
    {
        public sealed class PlayerShootsBullets : GamePlayEvents { }
        public sealed class PlayerShootsMissile : GamePlayEvents { }
        public sealed class EnemyHitBy : GamePlayEvents
        {
            public IDamageDealer HitBy { get; private set; }
            public EnemyHitBy(IDamageDealer gameObject)
            {
                HitBy = gameObject;
            }
        }
        public sealed class EnemyLostLife : GamePlayEvents
        {
            public int CurrentLife { get; private set; }
            public EnemyLostLife(int currentLife)
            {
                CurrentLife = currentLife;
            }
        }
    }
}
