using ActorSystem.DIContainer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ActorSystem.Tests;

[Binding]
public sealed class CreateServiceCollectionTest
{
    IConfiguration? configuration;
    IConfigurationSection? description;
  
    [Given("Определен класс конфигурации")]
    public void Given_Configuration()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, @"{
            ""Dependencies"":
            {
                ""A"": 
                    {
                        ""Implementation"": ""ImplA"",
                        ""LifeTime"" : ""Transient"",
                    },
                ""B"": 
                    {
                        ""Implementation"": ""ImplB"",
                        ""LifeTime"" : ""Singleton"",
                    },
            }
        }"); 
        configuration = new ConfigurationBuilder().AddJsonFile(tempFile).Build();
    }

    [When("В нем есть список дескрипторов")]
    public void Given_Description(){
       Assert.NotEmpty(configuration!.GetSection("Dependencies").GetChildren());
    }

    [Then("Инициализируется коллекция сервисов")]
    public void Given_Services(){
        System.Console.WriteLine(Type.GetType("A"));
        System.Console.WriteLine(Type.GetType("ImplA"));
        var services = new ServiceProviderBuilder(configuration!).CreateServices();
        var serviceA = services.GetService<A>();
        Assert.NotNull(serviceA);
        Assert.IsAssignableFrom<A>(serviceA);
    }
}
