namespace ActorSystem.DIContainer;
using Microsoft.Extensions.DependencyInjection;

public interface IServiceProviderBuilder
{
    IServiceProvider CreateServices();
}