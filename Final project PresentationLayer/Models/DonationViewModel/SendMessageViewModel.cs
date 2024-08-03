namespace Final_project_PresentationLayer.Models.DonationViewModel
{
    public class SendMessageViewModel
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string Content { get; set; }

    }

    public class SendMessageRequest
    {
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public Guid DonationId { get; set; }
        public string Content { get; set; }
    }
}
