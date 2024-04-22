namespace ActorSystem.Communication;

public class MessageSystem(IRedirectRuleRepository redirectRuleRepository, IDictionary<string, IMailBox> mailBoxes) : IMessageSystem
{   
    public async Task requestMessage(IMessage message)
    {
        var endAddres = redirectRuleRepository.GetAdressReceiver(message.Receiver);
        await mailBoxes[endAddres].SendMessage(message);
    }
}