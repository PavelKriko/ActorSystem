namespace ActorSystem.Communication;

public interface IMessageSystem
{
    Task requestMessage(IMessage message);
}