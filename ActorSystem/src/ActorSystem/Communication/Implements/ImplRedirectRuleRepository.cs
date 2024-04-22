namespace ActorSystem.Communication;

public class RedirectRuleRepository(IDictionary<SenderReceiverKey, IMailBox> RedirectRule) : IRedirectRuleRepository
{
    public IMailBox GetAdressReceiver(SenderReceiverKey key)
    {
        try
        {
            return RedirectRule[key];
        }
        catch(KeyNotFoundException)
        {
            throw new KeyNotFoundException(string.Format("Не удалось найти получателя сообщения {0}, отправленного от {1}", key.Receiver, key.Sender));
        }
    }
}