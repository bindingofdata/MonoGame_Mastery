using Engine;
using FlyingShooter.States;

namespace FlyingShooter
{
    public static class Program
    {
        private const int BASE_WIDTH = 1280;
        private const int BASE_HEIGHT = 720;

        static void Main()
        {
            using var game = new MainGame(BASE_WIDTH, BASE_HEIGHT, new DevState());
            game.Run();
        }
    }
}