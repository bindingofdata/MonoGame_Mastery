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
    }
}
