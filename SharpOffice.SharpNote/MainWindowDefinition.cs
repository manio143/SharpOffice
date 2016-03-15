using SharpOffice.Core.Attributes;
using SharpOffice.Window;

namespace SharpOffice.SharpNote
{
    [Window(WindowType.MainWindow)]
    public class MainWindowDefinition : IWindowDefinition
    {

        public Xwt.Window Window
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Configure(Core.Configuration.IConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}