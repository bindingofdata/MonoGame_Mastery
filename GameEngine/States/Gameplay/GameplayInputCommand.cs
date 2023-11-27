using Engine.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States.GamePlay
{
    public class GameplayInputCommand : BaseInputCommand
    {
        public sealed class GameExit : GameplayInputCommand { }
        public sealed class PlayerMoveLeft : GameplayInputCommand { }
        public sealed class PlayerMoveRight : GameplayInputCommand { }
        public sealed class PlayerShoots : GameplayInputCommand { }
    }
}
