using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpOffice.Common.Configuration;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;

namespace SharpOffice.Common.Tests.Configuration
{
    [TestClass]
    public class ConfigurationFormatTest
    {
        public IConfiguration GetMockConfiguration(IEnumerable<KeyValuePair<string, object>> testData)
        {
            var mock = new Mock<IConfiguration>();
            var data = testData as IList<KeyValuePair<string, object>> ?? testData.ToList();
            mock.Setup(cfg => cfg.GetAllProperties()).Returns(data);
            return mock.Object;
        }

        public void ReadWriteInMemoryStreamTest<T>(IConfiguration configuration)
            where T : IConfigurationFormat, new()
        {
            var stream = new MemoryStream();
            var configFormat = new T();
            configFormat.WriteConfiguration(configuration, stream);

            stream.Position = 0;
            var config = configFormat.ReadConfiguration<TestConfiguration>(stream);
            foreach (var pair in configuration.GetAllProperties())
                Assert.AreEqual(pair.Value, config.GetProperty<object>(pair.Key));
        }

        [TestMethod]
        public void BinaryConfigurationFormatTest()
        {
            ReadWriteInMemoryStreamTest<BinaryConfigurationFormat>(
                GetMockConfiguration(
                    new Dictionary<string, object> { { "int", 16 }, { "float", 3.2f } }));
        }

        [TestMethod]
        public void YamlConfigurationFormatTest()
        {
            var config = new TestConfiguration();
            config.SetProperty("int", 16);
            config.SetProperty("float", 3.2f);
            //config.SetProperty("lista", new[] { true, false });
            //Collections are deserialized as List<string>
            ReadWriteInMemoryStreamTest<YamlConfigurationFormat>(config);
        }
    }
}
