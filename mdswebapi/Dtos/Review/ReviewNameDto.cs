namespace mdswebapi.Dtos.Review
{
    public class ReviewNameDto
    {
        public int ReviewId { get; set; }

        public string? CustomerId { get; set; }

        public int? MedId { get; set; }

        public string? ReviewContent { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }
}
