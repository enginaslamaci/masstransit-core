using MassTransit;
using Shared.Messages;

namespace Consumer.API.Consumers
{
    public class CompleteProcessEventConsumer : IConsumer<CompleteProcessEvent>
    {
        private readonly ILogger<CompleteProcessEvent> _logger;

        public CompleteProcessEventConsumer(ILogger<CompleteProcessEvent> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CompleteProcessEvent> context)
        {
            //throw new Exception("test error");
            _logger.LogInformation($"Process Info - Id : {context.Message.ProcessId} name : {context.Message.ProcessName}");
        }
    }
}
