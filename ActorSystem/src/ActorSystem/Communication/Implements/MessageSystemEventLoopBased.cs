using System.Collections.Concurrent;

namespace ActorSystem.Communication;

//В дальнейшем очередь сообщений будет представлять собой отдельный класс
public class MessageSystemEventLoopBased(BlockingCollection<IMessage> messages) : IMessageSystem
{
    public void requestMessage(IMessage message)
    {
        messages.Add(message);
    }
}