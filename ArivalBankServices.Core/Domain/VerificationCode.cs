using ArivalBankServices.Core.Base;
using System.Text.RegularExpressions;

namespace ArivalBankServices.Core.Domain
{
    public partial class VerificationCode : Entity
    {
        public VerificationCode(string phoneNumber, string countryCode, int codeExpiryTime)
        {
            PhoneNumber = phoneNumber.Trim();
            CountryCode = countryCode.Trim();
            CodeStatus = CodeStatus.Pending;

            if(!CountryCode.StartsWith("+"))
                throw new ArgumentException("Invalid Country Code");

            if (!IsPhoneNumberValid())
                throw new ArgumentException("Invalid Phone Number");

           GenerateConfirmationCode(codeExpiryTime);
        }

        public string CountryCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ConfirmationCode { get; private set; } = string.Empty;
        public DateTime? CodeExpiryDate { get; private set; }
        public bool IsCodeExpired { get => DateTime.UtcNow > CodeExpiryDate; }
        public CodeStatus CodeStatus { get; private set; }

        private bool IsPhoneNumberValid()
        {
            return PhoneNumberRegex().Match(PhoneNumber).Success;
        }

        private void GenerateConfirmationCode(int minutes)
        {
            Random r = new();
            CodeExpiryDate = DateTime.UtcNow.AddMinutes(minutes);
            ConfirmationCode = r.Next(0, 1000000).ToString("000000");
        }

        public void UpdateStatus(CodeStatus status)
        {
            CodeStatus = status;
            UpdateDate = DateTime.UtcNow;
        }

        [GeneratedRegex("^\\d{1,}$")]
        public static partial Regex PhoneNumberRegex();
    }
}
