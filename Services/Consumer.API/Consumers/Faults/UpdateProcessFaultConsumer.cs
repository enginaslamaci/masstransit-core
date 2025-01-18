using MassTransit;
using Shared.Messages;

namespace Consumer.API.Consumers.Faults
{
    public class UpdateProcessFaultConsumer : IConsumer<Fault<UpdateProcessCommand>>
    {
        private readonly ILogger<UpdateProcessCommand> _logger;

        public UpdateProcessFaultConsumer(ILogger<UpdateProcessCommand> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<UpdateProcessCommand>> context)
        {
            _logger.LogError($"Id: {context.Message.Message.ProcessId} - ErrorMessage: {context.Message.Exceptions?.FirstOrDefault()?.Message}");
        }
    }
}
