using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using NLog;
using SharpOffice.Core.Window;

namespace SharpOffice.Runtime.Eto.Utilities
{
    public class MenuBuilder
    {
        private readonly IMenuProvider _menuProvider;
        private static readonly Logger Logger = LogManager.GetLogger("Runtime.Eto.MenuBuilder");

        public MenuBuilder(IMenuProvider menuProvider, IEnumerable<IMenuComposer> menuComposers)
        {
            _menuProvider = menuProvider;

            Logger.Debug("Composing menu...");
            foreach (var menuComposer in menuComposers)
                menuComposer.Setup(menuProvider);
        }

        public MenuBar BuildMenu()
        {
            Logger.Debug("Building menu...");

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