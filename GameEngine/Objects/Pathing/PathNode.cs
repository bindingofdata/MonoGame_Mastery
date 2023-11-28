using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects.Pathing
{
    internal sealed class PathNode
    {
        public int StartingFrameNumber { get; }
        public Vector2 Direction { get; }

        public PathNode(int startingFrameNumber, Vector2 direction)
        {
            StartingFrameNumber = startingFrameNumber;
            Direction = direction;
        }
    }
}
