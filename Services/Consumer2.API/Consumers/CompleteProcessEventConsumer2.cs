using MassTransit;
using Shared.Messages;

namespace Consumer2.API.Consumers
{
    public class CompleteProcessEventConsumer2 : IConsumer<CompleteProcessEvent>
    {
        private readonly ILogger<CompleteProcessEvent> _logger;

        public CompleteProcessEventConsumer2(ILogger<CompleteProcessEvent> logger)
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
