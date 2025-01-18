using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Producer.API.Models;
using Shared.Constants;
using Shared.Messages;

namespace Producer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {

        private readonly ILogger<ProcessController> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProcessController(ILogger<ProcessController> logger, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompleteProcessInfo model)
        {
            CompleteProcessEvent newEvent = new CompleteProcessEvent()
            {
                ProcessId = model.Id,
                ProcessName = model.Name
            };

            await _publishEndpoint.Publish<CompleteProcessEvent>(newEvent);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProcess model)
        {
            var bus = await _sendEndpointProvider.GetSendEndpoint(new Uri($"{RabbitMQConstants.Uri}/{RabbitMQConstants.UpdateProcessQuene}"));

            UpdateProcessCommand command = new UpdateProcessCommand()
            {
                ProcessId = model.Id,
                ProcessCode = model.Code,
                ProcessName = model.Name,
                ProcessDescripiton = model.Description
            };

            await bus.Send(command); 

            return Ok();
        }

    }
}
