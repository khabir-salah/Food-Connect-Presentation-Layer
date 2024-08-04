namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class DonationSearchCommand
    {
        public string? Location { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public PaginationFilter Filter { get; set; } = new PaginationFilter();
    }
}
