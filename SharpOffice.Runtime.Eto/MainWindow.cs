using Eto.Drawing;
using Eto.Forms;
using SharpOffice.Core;
using SharpOffice.Core.Window;
using SharpOffice.Runtime.Eto.Utilities;

namespace SharpOffice.Runtime.Eto
{
    public class MainWindow : Form
    {
        private readonly MenuBuilder _menuBuilder;

        public MainWindow(IApplication application, IMenuProvider menuProvider)
        {
            Title = application.Name + " - SharpOffice.Runtime.Eto (" + GetType().Assembly.GetName().Version + ")";
            
            _menuBuilder = new MenuBuilder(menuProvider);
        }

        public new void Initialize()
        {
            ClientSize = new Size(460, 200);

            Menu = _menuBuilder.BuildMenu();
        }
    }
}