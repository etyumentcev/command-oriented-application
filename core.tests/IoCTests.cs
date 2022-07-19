using System.Reflection;

namespace HWdTech
{
    public class IoCTests
    {
        [Fact]
        public void BasedVersionOfIoCShouldAllowToChangestrategy()
        {
            IoC.Resolve<ICommand>("HWdTech.IoC.Setup", (Func<string, object[], object>)((key, args) =>
            {
                if ((string)key == "dependency")
                {
                    return 1;
                }

                throw new Exception("Dependncy was not found.");
            })).Execute();

            Assert.Equal(1, IoC.Resolve<int>("dependency"));
            Assert.Throws<Exception>(() => IoC.Resolve<Object>("UnknownDepenendncy"));
        }
    }
}