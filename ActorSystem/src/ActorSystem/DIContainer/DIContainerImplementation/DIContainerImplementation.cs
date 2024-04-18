namespace ActorSystem.DIContainer;

public sealed class DIContainer(IServiceProvider services) : DIContainerInterface
{
    public IServiceProvider Services{get => services; set { services = value;}}
}