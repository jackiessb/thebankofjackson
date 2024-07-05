namespace thebankofjackson_api.Credentials {
    public interface IPlaidCredentials {
        public string? Secret { get; set; }
        public string? ClientID { get; set; }
        public string[]? Products { get; set; }
        public string? Language { get; set; }
        public string[]? CountryCodes { get; set; }
        public string? LinkToken { get; set; }
        public string? PublicToken { get; set; }
    }
    public class PlaidCredentials : IPlaidCredentials {
        public string? Secret { get; set; }
        public string? ClientID { get; set; }
        public string[]? Products { get; set; }
        public string? Language { get; set; }
        public string[]? CountryCodes { get; set; }
        public string? LinkToken { get; set; }
        public string? PublicToken { get; set; }
    }
}
