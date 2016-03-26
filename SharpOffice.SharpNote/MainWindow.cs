using SharpOffice.Window;
using Xwt;

namespace SharpOffice.SharpNote
{
    public class MainWindow : Xwt.Window
    {
        private Menu _menu;

        public MainWindow()
        {
            //DEMO
            Title = "SharpNote (alpha)";
            Width = 500;
            Height = 400;
            Closed += (sender, args) => Xwt.Application.Exit();

            InitializeMenu();
        }

        private void InitializeMenu()
        {
            _menu = new Menu();

            var menuFile = MenuHelper.CreateMenuItem("_File", subMenu: new[]
            {
             MenuHelper.CreateMenuItem("_New", subMenu: new[]
             {
                 MenuHelper.CreateMenuItem("_Source file", command: null),
                 MenuHelper.CreateMenuItem("_Note", command: null)
             }),
             MenuHelper.CreateMenuItem("_Open", command: null),
             MenuHelper.CreateMenuItem("E_xit", command: (sender, args) => this.Close())
            });

            _menu.Items.Add(menuFile);

            this.MainMenu = _menu;
        }
    }
}
