using System.Collections.Generic;
using SharpOffice.Common.Configuration;

namespace SharpOffice.Common.Tests.Configuration
{
    public class TestPropertyBasedConfiguration : PropertyBasedConfiguration
    {
        public int Integer { get; set; }
        public string Text { get; set; }
        public IEnumerable<bool> Bits { get; set; }
    }
}