namespace DomainRegistrarWebApp.Models.Auth
{
    public class LoginModel
    {
        public string S { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(S);
    }
}