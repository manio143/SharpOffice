using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpOffice.Core.Configuration;

namespace SharpOffice.Common.Tests.Configuration
{
    [TestFixture]
    public class PropertyBasedConfigurationTest
    {
        private IConfiguration config;
        readonly bool[] arr = { true, false, true };

        [Test]
        public void CreationTest()
        {
            config = new TestPropertyBasedConfiguration();
        }

        [Test]
        public void SetTest()
        {
            CreationTest();
            config.SetProperty("Integer", 8);
            config.SetProperty("Text", "Hello!");
            config.SetProperty("Bits", arr);
        }

        [Test]
        public void GetTest()
        {
            SetTest();
            Assert.AreEqual(8, config.GetProperty<int>("Integer"));
            Assert.AreEqual("Hello!", config.GetProperty<string>("Text"));
            int i = 0;
            foreach (var el in config.GetProperty<IEnumerable<bool>>("Bits"))
            {
                Assert.AreEqual(arr[i++], el);
            }
        }

        [Test]
        public void InvalidGetNullCastTest()
        {
            CreationTest();
            Assert.Throws<InvalidCastException>(delegate { config.GetProperty<int>("Text"); });
        }

        [Test]
        public void InvalidGetCastTest()
        {
            CreationTest();
            SetTest();
            Assert.Throws<InvalidCastException>(delegate { config.GetProperty<int>("Text"); });
        }

        [Test]
        public void InvalidSetCastTest()
        {
            CreationTest();
            Assert.Throws<InvalidCastException>(delegate { config.SetProperty("Text", 5); });
        }

        [Test]
        public void GetAllPropertiesTest()
        {
            CreationTest();
            SetTest();
            var dict = config.GetAllProperties().ToList();
            Assert.AreEqual("Hello!", dict.First(kvp => kvp.Key == "Text").Value);
            Assert.AreEqual(8, dict.First(kvp => kvp.Key == "Integer").Value);
            int i = 0;
            foreach (var el in (IEnumerable<bool>)dict.First(kvp => kvp.Key == "Bits").Value)
            {
                Assert.AreEqual(arr[i++], el);
            }
        }
    }
}
