using System.Linq;
using Eto.Forms;
using SharpOffice.Core.Window;

namespace SharpOffice.Runtime.Eto.Utilities
{
    public class MenuBuilder
    {
        private IMenuProvider _menuProvider;

        public MenuBuilder(IMenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
        }

        public MenuBar BuildMenu()
        {
            var menu = new MenuBar();
            foreach (var topLevelMenu in _menuProvider.TopLevelMenus)
                menu.Items.Add(CreateMenuItem(topLevelMenu));
            return menu;
        }

        public MenuItem CreateMenuItem(IMenuElement menuElement)
        {
            MenuItem menuItem;
            if (menuElement.Checked != null)
                menuItem = new CheckMenuItem {Checked = menuElement.Checked.Value};
            else if (menuElement is MenuSeparator)
                return new SeparatorMenuItem();
            else
                menuItem = new ButtonMenuItem();

            menuItem.Text = menuElement.Label;
            menuItem.Enabled = menuElement.Enabled;
            
            if(menuElement.SubMenu != null)
                (menuItem as ButtonMenuItem).Items.AddRange(menuElement.SubMenu.Items.Select(CreateMenuItem));

            menuItem.Click += menuElement.Command;

            return menuItem;
        }
 
    }
}