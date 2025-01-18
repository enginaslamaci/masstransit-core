using MassTransit;
using Shared.Constants;

namespace Consumer.API.Consumers.Definitions
{
    public class UpdateProcessConsumerDefinition : ConsumerDefinition<UpdateProcessCommandConsumer>
    {
        public UpdateProcessConsumerDefinition()
        {
            // override the default endpoint name
            EndpointName = RabbitMQConstants.UpdateProcessQuene;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<UpdateProcessCommandConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRateLimit(1, TimeSpan.FromSeconds(10)); // 1 request in 10 seconds
            endpointConfigurator.UseMessageRetry(r => r.Immediate(3)); // try 3 times if error
            endpointConfigurator.UseCircuitBreaker(configurator =>
            {
                configurator.TrackingPeriod = TimeSpan.FromMinutes(1); //tracking after error
                configurator.TripThreshold = 15; //received error rate
                configurator.ActiveThreshold = 10; //repeated errors
                configurator.ResetInterval = TimeSpan.FromMinutes(5); //waiting when error
            });
        }
    }
}
