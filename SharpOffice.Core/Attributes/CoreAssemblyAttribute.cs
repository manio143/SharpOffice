using System;

namespace SharpOffice.Core.Attributes
{
    /// <summary>
    /// Defines the assembly as part of the core.
    /// The Container won't AutoRegister assemblies marked with this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class CoreAssemblyAttribute : Attribute
    {
    }
}