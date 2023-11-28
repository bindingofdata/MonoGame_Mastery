using FlyingShooter.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Objects.Pathing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlyingShooter.States
{
    internal sealed class ChopperGenerator
    {
        // Location and movement
        private bool _generateLeft = true;
        private Vector2 _leftVector = new Vector2(-1, 0);
        private Vector2 _leftDownVector = new Vector2(-1, 1);
        private Vector2 _rightVector = new Vector2(1, 0);
        private Vector2 _rightDownVector = new Vector2(1, 1);

        // VFX
        private Texture2D _chopperTexture;
        private Timer _timer;
        private Action<ChopperSprite> _chopperHandler;
        private int _maxChoppers;
        private int _choppersGenerated;
        private bool _generating = false;

        public ChopperGenerator(Texture2D texture, int maxChoppers, Action<ChopperSprite> handler)
        {
            _chopperTexture = texture;
            _maxChoppers = maxChoppers;
            _chopperHandler = handler;

            _leftDownVector.Normalize();
            _rightDownVector.Normalize();

            _timer = new Timer(500);
            _timer.Elapsed += _timer_Elapsed;
        }

        public void GenerateChoppers()
        {
            if (_generating)
            {
                return;
            }

            _choppersGenerated = 0;
            _timer.Start();
            _generating = true;
        }

        public void StopGenerating()
        {
            _timer.Stop();
            _generating = false;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<PathNode> path;
            Vector2 position;
            ChopperSprite chopper;
            if (_generateLeft)
            {
                path = new List<PathNode>()
                {
                    new PathNode(0, _rightVector),
                    new PathNode(120, _rightDownVector),
                };

                position = new Vector2(-200, 100);
            }
            else
            {
                path = new List<PathNode>()
                {
                    new PathNode(0, _leftVector),
                    new PathNode(120, _leftDownVector),
                };

                position = new Vector2(1500, 100);
            }

            chopper = new ChopperSprite(_chopperTexture, ChopperColor.Yellow, path);
            chopper.Position = position;
            _chopperHandler(chopper);

            _generateLeft = !_generateLeft;
            _choppersGenerated++;
            if(_choppersGenerated >= _maxChoppers)
            {
                StopGenerating();
            }
        }
    }
}
