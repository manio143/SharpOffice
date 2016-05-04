using System;
using System.ComponentModel;

namespace SharpOffice.Core.Window
{
    public class SubMenuItem : IMenuElement
    {
        protected SubMenuItem(string label)
        {
            _label = label;
        }

        private readonly string _label;
        public string Label
        {
            get { return _label; }
        }

        public bool Enabled { get { return true; } }
        public bool? Checked { get { return null; } }

        private readonly Menu _subMenu = new Menu();
        public Menu SubMenu
        {
            get { return _subMenu; }
        }

        public void Command(object sender, EventArgs args)
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}