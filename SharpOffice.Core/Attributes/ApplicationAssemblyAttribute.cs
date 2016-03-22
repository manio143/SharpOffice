using System;

namespace SharpOffice.Core.Attributes
{
    /// <summary>
    /// Marks a SharpOffice application.
    /// The Container will AutoRegister assemblies marked with this attribute.
    /// But only if it is the currently ran application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ApplicationAssemblyAttribute : Attribute
    {
    }
}