﻿using System.Windows.Controls;
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
        }

        public void InitializeMenus(IMenuProvider menuProvider)
        {
            _menuBuilder = new MenuBuilder(menuProvider);
            RenderMenu();
        }

        public void RenderMenu()
        {
            _menuBuilder.UpdateMenu();

            var rootGrid = (Grid) Content;
            if (!rootGrid.Children.Contains(_menuBuilder.RootMenu))
                rootGrid.Children.Add(_menuBuilder.RootMenu);
        }
    }
}
