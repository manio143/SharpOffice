namespace SharpOffice.Core
{
    /// <summary>
    /// Describes a class that provides a registration entry for the application.
    /// </summary>
    /// <remarks>
    /// Every application should have exactly one implementation of this interface.
    /// </remarks>
    public interface IApplication
    {
        string Name { get; }
        void RegisterCustomTypes(DryIoc.Container container);
    }
}