using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using SharpOffice.Common.Configuration;
using SharpOffice.Core.Configuration;
using SharpOffice.Core.Formats;

namespace SharpOffice.Common.Tests.Configuration
{
    [TestFixture]
    public class ConfigurationFormatTest
    {
        public IConfiguration GetMockConfiguration(IEnumerable<KeyValuePair<string, object>> testData)
        {
            var mock = new Mock<IConfiguration>();
            var data = testData as IList<KeyValuePair<string, object>> ?? testData.ToList();
            mock.Setup(cfg => cfg.GetAllProperties()).Returns(data);
            return mock.Object;
        }

        public void ReadWriteInMemoryStreamTest<TFormat, TConfig>(IConfiguration configuration)
            where TFormat : IConfigurationFormat, new()
            where TConfig : IConfiguration, new()
        {
            var stream = new MemoryStream();
            var configFormat = new TFormat();
            configFormat.WriteConfiguration(configuration, stream);

            stream.Position = 0;
            var config = configFormat.ReadConfiguration<TConfig>(stream);
            foreach (var pair in configuration.GetAllProperties())
                if (!(pair.Value is IEnumerable) || pair.Value is string)
                    Assert.AreEqual(pair.Value, config.GetProperty<object>(pair.Key));
                else
                    foreach (var obj in config.GetProperty<IEnumerable>(pair.Key))
                        Assert.IsTrue(Has((IEnumerable) pair.Value, obj));
        }

        [Test]
        public void BinaryConfigurationFormatTest_WithMock()
        {
            ReadWriteInMemoryStreamTest<BinaryConfigurationFormat, TestConfiguration>(
                GetMockConfiguration(
                    new Dictionary<string, object> { { "int", 16 }, { "float", 3.2f } }));
        }
        
        [Test]
        public void BinaryConfigurationFormatTest_WithPropertyBasedConfiguration()
        {
            ReadWriteInMemoryStreamTest<BinaryConfigurationFormat, TestPropertyBasedConfiguration>(
                new TestPropertyBasedConfiguration()
                {
                    Integer = 12,
                    Text = "Warszawa",
                    Bits = new[] {true, false}
                });
        }

        [Test]
        public void YamlConfigurationFormatTest()
        {
            var config = new TestPropertyBasedConfiguration();
            config.SetProperty("Integer", 16);
            config.SetProperty("Text", "Ala ma kota.");
            config.SetProperty("Bits", new[] { true, false });
            ReadWriteInMemoryStreamTest<YamlConfigurationFormat, TestPropertyBasedConfiguration>(config);
        }

        private bool Has(IEnumerable enumerable, object obj)
        {
            if (enumerable.Cast<object>().Any(el => obj.Equals(el)))
                return true;
            return false;
        }
    }
}
