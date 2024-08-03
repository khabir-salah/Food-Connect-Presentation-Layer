namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class DonationWithMessagesViewModel
    {
        public DonationResponseCommandModel Donation { get; set; }
        public MessageCommandModel Messages { get; set; }
    }
    

    public class DonationResponseCommandModel
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

public class MessageCommandModel
    {
        public Guid DonorId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
