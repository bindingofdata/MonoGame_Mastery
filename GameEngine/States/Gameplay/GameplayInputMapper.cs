using Engine.Input;

using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingShooter.States.GamePlay
{
    public sealed class GamePlayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState keyboardState)
        {
            List<BaseInputCommand> commands = new List<BaseInputCommand>();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                commands.Add(new GamePlayInputCommand.GameExit());
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                commands.Add(new GamePlayInputCommand.PlayerMoveLeft());
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                commands.Add(new GamePlayInputCommand.PlayerMoveRight());
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                commands.Add(new GamePlayInputCommand.PlayerShoots());
            }

            return commands;
        }
    }
}
