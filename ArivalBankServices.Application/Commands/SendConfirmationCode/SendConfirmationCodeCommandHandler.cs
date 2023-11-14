using ArivalBankServices.Application.Config;
using ArivalBankServices.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OperationResult;

namespace ArivalBankServices.Application.Commands.SendConfirmationCode
{
    public class SendConfirmationCodeCommandHandler : IRequestHandler<SendConfirmationCodeCommand, Result<string>>
    {
        private readonly IVerificationCodesRepository _verificationCodesRepository;
        private readonly ILogger<SendConfirmationCodeCommandHandler> _logger;
        private readonly CodeConfiguration _codeConfiguration;

        public SendConfirmationCodeCommandHandler(IVerificationCodesRepository verificationCodesRepository,
            ILogger<SendConfirmationCodeCommandHandler> logger,
            IOptions<CodeConfiguration> codeConfiguration)
        {
            _verificationCodesRepository = verificationCodesRepository;
            _logger = logger;
            _codeConfiguration = codeConfiguration.Value;
        }

        public async Task<Result<string>> Handle(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
        {
            var allCodes = await _verificationCodesRepository.GetAllAsync(c => c.CountryCode.Equals(request.CountryCode) && c.PhoneNumber.Equals(request.Phone)
                && c.CodeStatus == CodeStatus.Pending);

            if (allCodes.Count() >= _codeConfiguration.CodesPerPhone)
            {
                return Result.Error<string>(new Exception("Maximum number of codes per phones already exceeded"));
            }

            VerificationCode newCode = new(request.Phone!, request.CountryCode!, _codeConfiguration.ExpirationTime);
            await _verificationCodesRepository.CreateAsync(newCode, cancellationToken);

            //Send New Code Here
            _logger.LogInformation($"Confirmation code: {newCode.ConfirmationCode}");

            return Result.Success(newCode.ConfirmationCode);
        }
    }
}
