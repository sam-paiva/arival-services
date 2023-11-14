using FluentValidation.Results;

namespace ArivalBankServices.API.Validation
{
    public record ValidationErrorDetails
    {
        public ValidationErrorDetails(string? title, IEnumerable<ValidationFailure>? errors)
        {
            Title = title;
            Errors = errors;
        }

        public string? Title { get; }
        public IEnumerable<ValidationFailure>? Errors { get; }
    }
}
