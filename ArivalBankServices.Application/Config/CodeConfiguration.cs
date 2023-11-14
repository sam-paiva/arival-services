namespace ArivalBankServices.Application.Config
{
    public record CodeConfiguration
    {
        public int ExpirationTime { get; set; }
        public int CodesPerPhone { get; set; }
    }
}
