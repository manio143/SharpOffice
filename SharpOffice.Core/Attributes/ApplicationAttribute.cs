using System;

namespace SharpOffice.Core.Attributes
{
    /// <summary>
    /// Specifies SharpOffice applications that may load this class or assembly.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Assembly | AttributeTargets.Class,
        AllowMultiple = true, Inherited = true)]
    public class ApplicationAttribute : Attribute
    {
        public string Application { get; private set; }

        public ApplicationAttribute(string application)
        {
            Application = application;
        }
    }
}
