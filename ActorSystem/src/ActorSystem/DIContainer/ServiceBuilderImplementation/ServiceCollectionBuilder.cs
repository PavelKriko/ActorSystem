using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActorSystem.DIContainer;

public class ServiceProviderBuilder(IConfiguration configuration) : IServiceProviderBuilder
{
    public IServiceProvider CreateServices(){
        var services = new ServiceCollection();
        IConfigurationSection dependencies = configuration.GetSection("Dependencies");

        //Каждый ребенок представляет собой назвиние(тип) зависимости children.key,
        //класс реализующий ее и время жизни
        foreach(var children in dependencies.GetChildren()){
            var dependencyName = children.Key;
            var lifeTime = children["LifeTime"];
            var Implementation = children["Implementation"];

            Type depType = Type.GetType(dependencyName);
            Type ImplType = Type.GetType(Implementation);
            switch (lifeTime){
                case "Transient":
                services.AddTransient(depType,ImplType);
                break;

                case "Singleton":
                services.AddSingleton(depType, ImplType);
                break;

                default:
                break;
            }

        }
        return services.BuildServiceProvider();
    }
}

public interface A{};
public class ImplA : A{};

public interface B{};
public class ImplB : B{};