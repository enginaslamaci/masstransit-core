using MassTransit;
using Shared.Messages;

namespace Consumer.API.Consumers.Faults
{
    public class CompleteProcessFaultConsumer : IConsumer<Fault<CompleteProcessEvent>>
    {
        private readonly ILogger<CompleteProcessEvent> _logger;

        public CompleteProcessFaultConsumer(ILogger<CompleteProcessEvent> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<CompleteProcessEvent>> context)
        {
            _logger.LogError($"Id: {context.Message.Message.ProcessId} - ErrorMessage: {context.Message.Exceptions?.FirstOrDefault()?.Message}");
        }
    }
}
