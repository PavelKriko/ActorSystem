using ActorSystem.Communication;
using ActorSystem.Actors;
using TechTalk.SpecFlow;

[Binding]
public class ValidatorTests
{
    IMailBox? validatorMailBox;
    IMailBox? trueValidationMailBox;
    IMailBox? falseValidationMailBox;
    IRedirectRuleRepository? ruleRepo;
    IMessageSystem? messageSystem;
    Validator<int>? validator;

    [Given("Система состоящая из 3 почтовых ящиков")]
    public void threeBoxesSystem()
    {
        validatorMailBox = new MailBox();
        trueValidationMailBox = new MailBox();
        falseValidationMailBox = new MailBox();

        var redirectRules = new Dictionary<SenderReceiverKey, IMailBox>();
        redirectRules[new SenderReceiverKey("Validator", "ValidationTrue")] = trueValidationMailBox;
        redirectRules[new SenderReceiverKey("Validator", "ValidationFalse")] = falseValidationMailBox;

        ruleRepo = new RedirectRuleRepository(redirectRules);
        messageSystem = new MessageSystem(ruleRepo);

        validator = new Validator<int>(messageSystem,validatorMailBox, "Validator", "age", 18);

    }

    [When("В валидотор приходит сообщение со значением, которое нужно проверить, оно не проходит валидацию")]
    public async Task ValidatorGetFalseValidationMessage()
    {
        var msg = new Message();
        msg.Context["age"] = 14;
        validatorMailBox!.PutMessage(msg);
        await validator!.HandleMessage();
    }

    [Then("В ящике FalseValidation появляется сообщение")]
    public async Task falseValidationMailBoxGetMessage()
    {
        Assert.NotNull(await falseValidationMailBox!.GetMessage());
    }
}