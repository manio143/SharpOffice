using System;
using Xwt;

namespace SharpOffice.Window
{
    public static class MenuHelper
    {
        public static MenuItem CreateMenuItem(string label, EventHandler command = null, MenuItem[] subMenu = null)
        {
            var menuItem = new MenuItem(label);
            if (command != null)
                menuItem.Clicked += command;
            else if (subMenu != null)
                menuItem.SubMenu = CreateMenu(subMenu);
            return menuItem;
        }

        public static Menu CreateMenu(MenuItem[] subMenu)
        {
            var menu = new Menu();
            foreach (var menuItem in subMenu)
            {
                menu.Items.Add(menuItem);
            }
            return menu;
        }
    }
}