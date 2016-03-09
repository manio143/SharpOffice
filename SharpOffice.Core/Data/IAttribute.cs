namespace SharpOffice.Core.Data
{
    public interface IAttribute
    {
        string Name { get; set; }
        IValue Value { get; set; }
    }
}