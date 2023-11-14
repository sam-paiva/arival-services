using ArivalBankServices.API.Validation;
using ArivalBankServices.Application.Commands.SendConfirmationCode;
using ArivalBankServices.Application.Commands.ValidateConfirmationCode;
using ArivalBankServices.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArivalBankServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationCodesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerificationCodesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-code")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrorDetails))]
        public async Task<IActionResult> SendConfirmationCode(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Exception.Message);

            return CreatedAtAction(nameof(SendConfirmationCode), result.Value);
        }

        [HttpPatch("verify-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrorDetails))]
        public async Task<IActionResult> VerifyConfirmationCode(ValidateConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsSuccess) return Ok();
            if(result.Exception is CodeNotFoundException)
                return NotFound(result.Exception.Message);

            return BadRequest(result.Exception.Message);
        }
    }
}
