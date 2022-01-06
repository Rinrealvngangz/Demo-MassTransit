using MassTransit;
using Model;
using System;
using System.Threading.Tasks;
namespace ReceivedService
{
    public class MessageConsumer : IConsumer<Messager>
    {
        public async Task Consume(ConsumeContext<Messager> context)
        {
          
            await Console.Out.WriteLineAsync($"From:{context.Message.Name}\n" +
                                             $"Message:{context.Message.Message}");
        }
    }
}