namespace zbw.Auftragsverwaltung.Domain.Users
{
    public class AuthenticateResponse
    {
        public object AuthenticationToken { get; set; }

        public object RefreshToken { get; set; }
    }
}
