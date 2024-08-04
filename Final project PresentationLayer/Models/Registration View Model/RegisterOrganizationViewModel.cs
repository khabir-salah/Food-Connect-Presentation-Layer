namespace Final_project_PresentationLayer.Models
{
    public class RegisterOrganizationViewModel
    {
        public string OganisationName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string CacNumber { get; set; } = default!;
        public int? Capacity { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
