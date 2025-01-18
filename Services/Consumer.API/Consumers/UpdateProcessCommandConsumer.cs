using MassTransit;
using Shared.Messages;

namespace Consumer.API.Consumers
{
    public class UpdateProcessCommandConsumer : IConsumer<UpdateProcessCommand>
    {
        private readonly ILogger<UpdateProcessCommand> _logger;

        public UpdateProcessCommandConsumer(ILogger<UpdateProcessCommand> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UpdateProcessCommand> context)
        {
            _logger.LogInformation($"Process Info - Id : {context.Message.ProcessId} code : {context.Message.ProcessCode} name : {context.Message.ProcessName}");
        }
    }
}
