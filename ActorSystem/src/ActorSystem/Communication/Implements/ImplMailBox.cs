using System.Collections.Concurrent;

namespace ActorSystem.Communication;

public class MailBox(IMessageSystem messageSystem) : IMailBox
{
    ConcurrentQueue<IMessage> _messages = new();

    public async Task SendMessage(IMessage message){
        await messageSystem.requestMessage(message);
    }

    public IMessage? GetMessage()
    {
        IMessage? message;
        return _messages.TryDequeue(out message)? message : null;
    }

}