using MassTransit;
using Shared.Messages;

namespace Consumer2.API.Consumers.Faults
{
    public class CompleteProcessFaultConsumer2 : IConsumer<Fault<CompleteProcessEvent>>
    {
        private readonly ILogger<CompleteProcessFaultConsumer2> _logger;

        public CompleteProcessFaultConsumer2(ILogger<CompleteProcessFaultConsumer2> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<CompleteProcessEvent>> context)
        {
            _logger.LogError($"Id: {context.Message.Message.ProcessId} - ErrorMessage: {context.Message.Exceptions?.FirstOrDefault()?.Message}");
        }
    }
}
