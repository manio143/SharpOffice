using System.Windows.Controls;
using DryIoc;
using SharpOffice.Core;
using SharpOffice.Core.Container;
using SharpOffice.Core.Window;
using SharpOffice.Window.Runtime.WPF.Utilities;
using WPFMenu = System.Windows.Controls.Menu;

namespace SharpOffice.Window.Runtime.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private MenuBuilder _menuBuilder;

        public MainWindow()
        {
            InitializeComponent();
            InitializeMenus(ContainerWrapper.GetContainer().Resolve<IMenuProvider>());
        }

        public void InitializeMenus(IMenuProvider menuProvider)
        {
            _menuBuilder = new MenuBuilder(menuProvider);
            RenderMenu();
        }

        public void RenderMenu()
        {
            _menuBuilder.BuildMenu();

            var rootGrid = (Grid) Content;
            if (!rootGrid.Children.Contains(_menuBuilder.RootMenu))
                rootGrid.Children.Add(_menuBuilder.RootMenu);
        }
    }
}
