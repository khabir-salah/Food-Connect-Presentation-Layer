namespace Final_project_PresentationLayer.Models.Profile_Update_View_Model
{
    public class IndividualUpdateViewModel
    {
        public string? City { get; set; }
        public string? LOcalGovernment { get; set; }
        public string? PostalCode { get; set; }
        public string? ProfileImage { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public string? Address { get; set; }
    }
}
