#language: ru-RU
Функция: Акторы могут обмениваться сообщениями с помощью EventLoop
Сценарий: Из почтового ящика №1 отправлено сообщение в ящик №2
    Дано Система состоящая их почтовых ящиков и EventLoop
    Когда Из ящика№1 отправляется сообщение в ящик№2 с EventLoopBased
    Тогда В ящике№2 появляется сообщение и EventLoop завершается