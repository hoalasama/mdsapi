namespace mdswebapi.Dtos.Promotion
{
    public class CreatePromotionRequestDto
    {

        public string? PromoName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DiscountPercent { get; set; }
    }
}
