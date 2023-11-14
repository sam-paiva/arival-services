namespace ArivalBankServices.Application.Exceptions
{
    public class CodeNotFoundException : Exception
    {
        public CodeNotFoundException() : base("Code not found")
        {
        }
    }
}
