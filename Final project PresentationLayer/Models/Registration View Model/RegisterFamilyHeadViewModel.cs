namespace Final_project_PresentationLayer.Models
{
    public class RegisterFamilyHeadViewModel
    {
        public record FamilyHeadViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int FamilyCount { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string NIN { get; set; }
            public string Password { get; set; }
        }
        
    }
}
