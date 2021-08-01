namespace zbw.Auftragsverwaltung.Core.Common.Configurations
{
    public class JwtBearerSettings
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}
