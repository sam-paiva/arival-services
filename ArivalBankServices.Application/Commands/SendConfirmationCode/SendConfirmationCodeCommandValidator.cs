using ArivalBankServices.Application.ValidationExtensions;
using FluentValidation;

namespace ArivalBankServices.Application.Commands.SendConfirmationCode
{
    public class SendConfirmationCodeCommandValidator : AbstractValidator<SendConfirmationCodeCommand>
    {
        public SendConfirmationCodeCommandValidator()
        {
            RuleFor(c => c.CountryCode).NotEmpty().WithMessage("{PropertyName} can't be empty or null")
                .Must(c => c!.StartsWith("+"))
                .WithMessage("{PropertyName} must starts with +");

            RuleFor(c => c.Phone).NotEmpty().WithMessage("{PropertyName} can't be empty or null")!.PhoneNumber();

        }
    }
}
