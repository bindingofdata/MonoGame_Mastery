using Engine.State;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States.Gameplay
{
    public class GameplayEvents : BaseGameStateEvent
    {
        public sealed class PlayerShoots : GameplayEvents { }
    }
}
