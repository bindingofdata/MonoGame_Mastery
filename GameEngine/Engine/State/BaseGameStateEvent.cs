using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.State
{
    public abstract class BaseGameStateEvent
    {
        public sealed class GameQuit : BaseGameStateEvent { }
    }
}
