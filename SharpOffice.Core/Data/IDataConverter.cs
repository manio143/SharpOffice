using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace SharpOffice.Core.Data
{
    public interface IDataConverter
    {
        ICollection<Type> SupportedTypes { get; }
        TTo Convert<TFrom, TTo>(TFrom data)
            where TFrom : IData
            where TTo : IData;
    }
}