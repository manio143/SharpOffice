using System;
using SharpOffice.Core.Data;

namespace SharpOffice.Common.Data
{
    public class StringValue : IValue
    {
        private string _value;

        public StringValue()
        {
            _value = String.Empty;
        }

        public StringValue(string value)
        {
            _value = value;
        }


        public object Value
        {
            get { return _value; }
            set { _value = (string)value; }
        }

        public T Cast<T>()
        {
            return (T)(object)_value;
        }
    }
}