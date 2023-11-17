using Engine.Input;

namespace FlyingShooter.States.Dev
{
    public class DevInputCommand : BaseInputCommand
    {
        public sealed class DevQuit : DevInputCommand { }
        public sealed class DevShoot : DevInputCommand { }
    }
}
