namespace SharpOffice.Core.Data
{
    public interface IValue
    {
        object Value { get; set; }
        T Cast<T>();
    }
}