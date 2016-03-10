using System;
using System.Linq;

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
        public Type Application { get; private set; }

        public ApplicationAttribute(Type application)
        {
            if(application.GetInterfaces().All(x => x != typeof (IApplication)))
                throw new ApplicationException("ApplicationAttribute may only be used with types that implement IApplication interface.");
            Application = application;
        }
    }
}
