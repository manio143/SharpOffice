using SharpOffice.Core.Attributes;
using SharpOffice.Window;

namespace SharpOffice.SharpNote
{
    [Window(WindowType.MainWindow)]
    public class MainWindowDefinition : IWindowDefinition
    {
        private readonly MainWindow _mainWindow;
        public Xwt.Window Window { get { return _mainWindow; } }

        public MainWindowDefinition()
        {
            _mainWindow = new MainWindow();
        }

        public void Configure(Core.Configuration.IConfigurationProvider configurationProvider)
        {
            //TODO: Setup _mainWindow
        }
    }
}