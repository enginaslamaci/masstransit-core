using MassTransit;
using Shared.Constants;

namespace Consumer.API.Consumers.Definitions
{
    public class CompleteProcessConsumerDefinition : ConsumerDefinition<CompleteProcessEventConsumer>
    {
        public CompleteProcessConsumerDefinition()
        {
            // override the default endpoint name
            //EndpointName = RabbitMQConstants.CompleteProcessQuene + "-1";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CompleteProcessEventConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseRateLimit(3, TimeSpan.FromSeconds(10)); // 3 request in 10 seconds
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
