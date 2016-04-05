using System.Collections.Generic;

namespace SharpOffice.Core.Window
{
    public class Menu
    {
        public List<IMenuElement> Items { get; protected set; }

        public Menu()
        {
            Items = new List<IMenuElement>();
        }

        public void AddMultiple(IEnumerable<IMenuElement> items)
        {
            foreach (var item in items)
                Items.Add(item);
        }
    }
}