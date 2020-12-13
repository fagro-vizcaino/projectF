namespace ProjectF.WebUI.Services
{
    public class AuthReponseDto
    {
        public bool IsAuthSuccessful { get; init; }
        public string ErrorMessage { get; init; }
        public string Token { get; set; }
    }
}