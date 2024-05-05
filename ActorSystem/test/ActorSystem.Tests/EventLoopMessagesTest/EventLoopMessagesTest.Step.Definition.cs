namespace ActorSystem.Tests;
using TechTalk.SpecFlow;
using ActorSystem.Communication;
using System.Collections.Concurrent;

[Binding]
class EventLoopTest
{

    IRedirectRuleRepository? ruleRepo;
    IMessageSystem? messageSystem;
    IMailBox? MailBox1;
    IMailBox? MailBox2;

    BlockingCollection<IMessage>? messages;

    EventLoop? eventLoop;


    [Given("Система состоящая их почтовых ящиков и EventLoop")]
    public void Address_For_MailBoxes()
    {   
        //Почтовые ящики
        this.MailBox1 = new MailBox();
        this.MailBox2 = new MailBox();

        //Правила перессылки сообщений
        var redirectRules = new Dictionary<SenderReceiverKey, IMailBox>();
        redirectRules[new SenderReceiverKey("Актор№1", "Актор№2")] = MailBox2;
        redirectRules[new SenderReceiverKey("Актор№2", "Актор№1")] = MailBox1;
        this.ruleRepo = new RedirectRuleRepository(redirectRules);
        
        //Система обмена сообщениями
        this.messages = new();
        this.messageSystem = new MessageSystemEventLoopBased(messages);

        this.eventLoop = new EventLoop(this.ruleRepo, this.messages);
    }

    [When("Из ящика№1 отправляется сообщение в ящик№2 с EventLoopBased")]
    public void Send_To_Mail2()
    {   
        IMessage msg = new Message();
        msg.Sender = "Актор№1";
        msg.Receiver = "Актор№2";
        messageSystem!.requestMessage(msg);
    }
    [Then("В ящике№2 появляется сообщение и EventLoop завершается")]
    public async void Mail2_Contains_messages(){
        var stop_msg = new Message();
        stop_msg.Context["STOP_EVENT_LOOP"] = true;
        messages!.Add(stop_msg);
        eventLoop!.Wait();
        Assert.NotNull(await MailBox2!.GetMessage());
    }

}