
using ActorSystem.Communication;
using TechTalk.SpecFlow;

namespace ActorSystem.Test;

[Binding]
public class LocalProcessingMessagesTest
{   
    IRedirectRuleRepository? ruleRepo;
    IMessageSystem? messageSystem;
    IMailBox? MailBox1;
    IMailBox? MailBox2;


    [Given("Система состоящая их почтовых ящиков")]
    public void Address_For_MailBoxes()
    {   
        this.MailBox1 = new MailBox();
        this.MailBox2 = new MailBox();
        var redirectRules = new Dictionary<SenderReceiverKey, IMailBox>();
        redirectRules[new SenderReceiverKey("Актор№1", "Актор№2")] = MailBox2;
        redirectRules[new SenderReceiverKey("Актор№2", "Актор№1")] = MailBox1;
        this.ruleRepo = new RedirectRuleRepository(redirectRules);
        this.messageSystem = new MessageSystem(ruleRepo);
    }

    [When("Из ящика№1 отправляется сообщение в ящик№2")]
    public void Send_To_Mail2()
    {   
        IMessage msg = new Message();
        msg.Sender = "Актор№1";
        msg.Receiver = "Актор№2";
        messageSystem!.requestMessage(msg);
    }
    [Then("В ящике№2 появляется сообщение")]
    public void Mail2_Contains_messages(){
        Assert.NotNull(MailBox2!.GetMessage());
    }
}