namespace mdswebapi.Dtos.Review
{
    public class CreateReviewDto
    {
        public string? CustomerId { get; set; }

        public int? MedId { get; set; }

        public string? ReviewContent { get; set; }
    }
}
