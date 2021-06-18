namespace ProjectF.WebUI.Pages.Auth
{
    public class UserResetPasswordDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public UserResetPasswordDto() { }
        public UserResetPasswordDto(string password, string confirm)
         => (Password, ConfirmPassword) = (password, confirm);
    }
}
