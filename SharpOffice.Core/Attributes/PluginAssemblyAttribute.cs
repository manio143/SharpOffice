using System;

namespace SharpOffice.Core.Attributes
{
    /// <summary>
    /// Marks the assembly that contains plugins/extensions for SharpOffice.
    /// The Container will AutoRegister assemblies marked with this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class PluginAssemblyAttribute : Attribute
    {
    }
}