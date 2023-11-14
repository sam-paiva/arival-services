namespace ArivalBankServices.Application.Exceptions
{
    public class CodeNotFoundException : Exception
    {
        public CodeNotFoundException() : base("No code found for the requested phone number")
        {
        }
    }
}
