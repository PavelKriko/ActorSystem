namespace ActorSystem.Communication;

//Нужно по заданному получателю, получить адрес почтового ящика
public interface IRedirectRuleRepository
{
    string GetAdressReceiver(string receiver);
}