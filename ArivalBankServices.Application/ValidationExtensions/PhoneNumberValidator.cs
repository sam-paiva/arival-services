using ArivalBankServices.Core.Domain;
using FluentValidation;

namespace ArivalBankServices.Application.ValidationExtensions
{
    internal static class PhoneNumberValidator
    {
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Matches(VerificationCode.PhoneNumberRegex()).WithMessage("Invalid Phone Number");
        }
    }
}
