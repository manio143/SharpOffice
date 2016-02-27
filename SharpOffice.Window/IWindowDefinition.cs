using System.Collections.Generic;
using SharpOffice.Window.Components;

namespace SharpOffice.Window
{
    public interface IWindowDefinition
    {
        ICollection<IComponent> Components { get; }
    }
}
