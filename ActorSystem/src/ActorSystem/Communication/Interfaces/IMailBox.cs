namespace ActorSystem.Communication;

public interface IMailBox
{
    Task SendMessage(IMessage message);
    IMessage? GetMessage();
}
