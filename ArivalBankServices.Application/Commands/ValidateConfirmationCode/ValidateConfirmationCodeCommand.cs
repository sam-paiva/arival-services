using MediatR;
using OperationResult;

namespace ArivalBankServices.Application.Commands.ValidateConfirmationCode
{
    public record ValidateConfirmationCodeCommand : IRequest<Result>, ICommand
    {
        public string? PhoneNumber { get; set; }
        public string? CountryCode { get; set; }
        public string? ConfirmationCode { get; set; }
    }
}
