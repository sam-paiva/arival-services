using ArivalBankServices.Application.Exceptions;
using ArivalBankServices.Core.Domain;
using MediatR;
using OperationResult;

namespace ArivalBankServices.Application.Commands.ValidateConfirmationCode
{
    public class ValidateConfirmationCodeCommandHandler : IRequestHandler<ValidateConfirmationCodeCommand, Result>
    {
        private readonly IVerificationCodesRepository _verificationCodesRepository;

        public ValidateConfirmationCodeCommandHandler(IVerificationCodesRepository verificationCodesRepository)
        {
            _verificationCodesRepository = verificationCodesRepository;
        }

        public async Task<Result> Handle(ValidateConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            var code = await _verificationCodesRepository.GetByQueryAsync(c => c.CountryCode.Equals(request.CountryCode!.Trim()) && c.PhoneNumber.Equals(request.PhoneNumber!.Trim())
                && c.CodeStatus == CodeStatus.Pending && c.ConfirmationCode == request.ConfirmationCode);

            if (code is null)
                Result.Error(new CodeNotFoundException());

            if (code!.IsCodeExpired)
            {
                code.UpdateStatus(CodeStatus.Cancelled);
                await _verificationCodesRepository.UpdateAsync(code);
                Result.Error(new Exception("Code expired"));
            }

            code.UpdateStatus(CodeStatus.Approved);
            await _verificationCodesRepository.UpdateAsync(code);

            return Result.Success();
        }
    }
}
