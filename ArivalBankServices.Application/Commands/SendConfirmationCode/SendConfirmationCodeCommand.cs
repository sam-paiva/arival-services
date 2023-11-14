using MediatR;
using OperationResult;

namespace ArivalBankServices.Application.Commands.SendConfirmationCode
{
    public record SendConfirmationCodeCommand : IRequest<Result<string>>, ICommand
    {
        public string? Phone { get; set; }
        public string? CountryCode { get; set; }
    }
}
