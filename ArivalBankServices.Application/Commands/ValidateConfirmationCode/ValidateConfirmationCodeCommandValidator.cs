using ArivalBankServices.Application.ValidationExtensions;
using FluentValidation;

namespace ArivalBankServices.Application.Commands.ValidateConfirmationCode
{
    public class ValidateConfirmationCodeCommandValidator : AbstractValidator<ValidateConfirmationCodeCommand>
    {
        public ValidateConfirmationCodeCommandValidator()
        {
            RuleFor(c => c.CountryCode).NotEmpty().WithMessage("{PropertyName} can't be empty or null");
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("{PropertyName} can't be empty or null")!
                .PhoneNumber();
            RuleFor(c => c.ConfirmationCode).NotEmpty().Must(code => code!.Length == 6).WithMessage("{PropertyName} must be 6 digits long");
        }
    }
}
