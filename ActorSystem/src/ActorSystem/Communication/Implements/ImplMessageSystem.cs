namespace ActorSystem.Communication;

public class MessageSystem(IRedirectRuleRepository redirectRuleRepository, IDictionary<string, IMailBox> mailBoxes) : IMessageSystem
{   
    public async Task requestMessage(IMessage message)
    {
        var endAddres = redirectRuleRepository.GetAdressReceiver(new SenderReceiverKey(message.Sender, message.Receiver));
        await endAddres.SendMessage(message);
    }
}