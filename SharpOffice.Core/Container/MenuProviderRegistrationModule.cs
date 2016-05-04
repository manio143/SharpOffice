using DryIoc;
using SharpOffice.Common.Window.Menu;
using SharpOffice.Core.Window;

namespace SharpOffice.Core.Container
{
    public class MenuProviderRegistrationModule : IRegistrationModule
    {
        public void Register(DryIoc.Container container)
        {
            container.Register<IMenuProvider, MenuProvider>(Reuse.Singleton);
        }
    }
}