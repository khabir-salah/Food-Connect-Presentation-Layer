namespace Final_project_PresentationLayer.Models
{
    public class RegistrationLoginViewModel
    {
        public record LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
