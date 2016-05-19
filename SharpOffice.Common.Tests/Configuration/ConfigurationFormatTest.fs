namespace SharpOffice.Common.Tests

open System
open System.Collections.Generic
open System.Linq
open System.IO
open Moq
open NUnit.Framework
open SharpOffice.Common.Configuration
open SharpOffice.Core.Configuration

[<TestFixture>]
[<TestOf(typeof<BinaryConfigurationFormat>)>]
module BinaryConfigurationTest =
    
    let GetMockConfiguration (testData : IEnumerable<KeyValuePair<string, System.Object>>) = 
        let mock = new Mock<IConfiguration>()
        let data = testData.ToList()
        ignore(mock.Setup(fun cfg -> cfg.GetAllProperties()).Returns(data))
        mock.Object

    type TestConfiguration () =
        let dictionary = new Dictionary<string, System.Object>()

        interface IConfiguration with
            member this.GetProperty<'T> (propertyName : string) =
                let property = dictionary.Item(propertyName)
                if not (property :? 'T) then raise (ArgumentException("type T"))
                else property :?> 'T

            member this.SetProperty<'T>( propertyName, propertyValue : 'T) =
                dictionary.Add(propertyName, propertyValue)

            member this.GetAllProperties() = dictionary :> IEnumerable<KeyValuePair<string, Object>>

    [<Test>]
    let WriteAndReadTest () =
        let data = new Dictionary<string, Object>()
        data.Add ("int", 16)
        data.Add ("float", 3.2f)

        use stream = new MemoryStream()
        let configFormat = new BinaryConfigurationFormat()
        configFormat.WriteConfiguration(GetMockConfiguration data, stream)

        stream.Position <- 0L
        let (config : IConfiguration) = configFormat.ReadConfiguration<TestConfiguration>(stream) :> IConfiguration
        for kvp in data do
            Assert.AreEqual(kvp.Value, config.GetProperty(kvp.Key))

[<TestFixture>]
[<TestOf(typeof<YamlConfigurationFormat>)>]
module YamlConfigurationTest =
    
    type TestConfiguration() =
        inherit PropertyBasedConfiguration()
        member val Int = 0 with get,set
        member val Float = 0.0f with get,set

    [<Test>]
    let WriteAndReadTest () =
        let configuration = new TestConfiguration()
        configuration.Int <- 16
        configuration.Float <- 3.2f

        use stream = new MemoryStream()
        let configFormat = new YamlConfigurationFormat()
        configFormat.WriteConfiguration(configuration, stream)

        stream.Position <- 0L
        let config = configFormat.ReadConfiguration<TestConfiguration>(stream)
        Assert.AreEqual(configuration.Int, config.Int)
        Assert.AreEqual(configuration.Float, config.Float)