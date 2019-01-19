using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ServiceCollection.RegisterOption;

namespace Tests
{
    public class OptionRegistratorTest
    {
        private IConfiguration _configuration;
        private IServiceCollection _serviceCollection;
        private IConfigurationSection _configurationSection;
        private class FakeOption
        {
            public string PropertyOne { get; set; }
            public string PropertySecond { get; set; }
        }
        
        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _serviceCollection = Substitute.For<IServiceCollection>();
            _configurationSection = Substitute.For<IConfigurationSection>();
        }

        [Test]
        public void RegisterOptionsSimpleConfigurationTest()
        {
            _configuration.GetSection(Arg.Any<string>()).Returns(_configurationSection);
            _serviceCollection.RegisterOptions<FakeOption>(_configuration);
        }
    }
}