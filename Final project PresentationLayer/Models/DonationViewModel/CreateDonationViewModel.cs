namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class CreateDonationViewModel
    {
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public Guid UserId { get; set; }
        public IList<IFormFile> DonationImages { get; set; } = null!;
        public IFormFile PrimaryImageUrl { get; set; } = null!;
    }
}
