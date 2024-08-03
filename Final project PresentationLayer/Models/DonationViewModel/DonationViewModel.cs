namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class DonationViewModel
    {
        public string FoodDetails { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DonationStatus Status { get; set; }
        public DateTime PickUpTime { get; set; }
        public string PickUpLocation { get; set; } = default!;
        public ICollection<string> DonationImages { get; set; } = null!;
        public string PrimaryImageUrl { get; set; } = null!;
    }

    public enum DonationStatus
    {
        pending,
        Approve,
        Disapprove,
        Available,
        Claim,
        Received
    }

}
