using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpOffice.Window.Components;
using SharpOffice.Window.UI;

namespace SharpOffice.Window
{
    public abstract class WindowDefinition
    {
        public ICollection<ComponentBase> Components { get; protected set; }
        public ICollection<MenuItem> Menu { get; protected set; } 
    }
}
