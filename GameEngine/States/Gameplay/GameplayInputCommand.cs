using Engine.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States.GamePlay
{
    public class GamePlayInputCommand : BaseInputCommand
    {
        public sealed class GameExit : GamePlayInputCommand { }
        public sealed class PlayerMoveLeft : GamePlayInputCommand { }
        public sealed class PlayerMoveRight : GamePlayInputCommand { }
        public sealed class PlayerShoots : GamePlayInputCommand { }
    }
}
