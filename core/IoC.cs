using System;

using static HWdTech.IoC;

using IoCStrategy = System.Func<string, object[], object>;

namespace HWdTech
{
    public class IoC
    {
        private class SetupCommand : ICommand
        {
            private IoCStrategy newStrategy;
            public SetupCommand(IoCStrategy newStrategy)
            {
                this.newStrategy = newStrategy;
            }

            public void Execute()
            {
                currentStrategy = newStrategy;
            }
        }

        private static IoCStrategy currentStrategy = (dependency, args) =>
        {
            if (dependency == "HWdTech.IoC.Setup")
            {
                return new SetupCommand((IoCStrategy)args[0]);
            }
            else
            {
                throw new ArgumentException(string.Format("IoC Dependency {0} was not found.", dependency));
            }
        };

        public static T Resolve<T>(string dependency, params object[] args)
        {
            return (T)currentStrategy(dependency, args);
        }
    }
}
