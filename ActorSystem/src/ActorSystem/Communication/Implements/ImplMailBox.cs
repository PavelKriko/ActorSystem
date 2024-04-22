using System.Collections.Concurrent;

namespace ActorSystem.Communication;

public class MailBox : IMailBox
{
    ConcurrentQueue<IMessage> _messages = new();

   public void PutMessage(IMessage message)
   {
        _messages.Enqueue(message);
   }

    public IMessage? GetMessage()
    {
        IMessage? message;
        return _messages.TryDequeue(out message)? message : null;
    }

}