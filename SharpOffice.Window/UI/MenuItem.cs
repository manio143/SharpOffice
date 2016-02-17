using System;
using System.Collections.Generic;

namespace SharpOffice.Window.UI
{
    /// <summary>
    /// The class describing a MenuItem
    /// </summary>
    public class MenuItem
    {
        public MenuItem Parent { get; internal set; }
        public ICollection<MenuItem> Children { get; internal set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <remarks>Use MenuItem.Create to create new object.</remarks>
        protected MenuItem()
        {
            Children = new List<MenuItem>();

            ActionsEnumerable.Add((sender, args) => Checked = !Checked);
        }

        public string Text { get; set; }
        public bool? Checked { get; set; }

        internal List<EventHandler> ActionsEnumerable = new List<EventHandler>();

        /// <summary>
        /// Methods added to this event will be invoked on click.
        /// </summary>
        public event EventHandler Actions
        {
            add { ActionsEnumerable.Add(value); }
            remove { ActionsEnumerable.Remove(value); }
        }
        /// <summary>
        /// Adds a child to the MenuItem.
        /// </summary>
        /// <param name="child"></param>
        /// <returns>Returns 'this' for chaining of methods.</returns>
        /// <example>MenuItem.Create("File").AddChild(MenuItem.Create("Open")).AddChild(MenuItem.Create("Save"))</example>
        public MenuItem AddChild(MenuItem child)
        {
            child.Parent = this;
            Children.Add(child);
            return this;
        }

        /// <summary>
        /// Creates a new instance of MenuItem.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static MenuItem Create(string text, EventHandler action = null)
        {
            var mi = new MenuItem { Text = text };
            if (action != null)
                mi.ActionsEnumerable.Add(action);
            return mi;
        }
    }
}
