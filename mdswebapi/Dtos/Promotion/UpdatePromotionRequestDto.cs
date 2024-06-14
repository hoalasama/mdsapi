namespace mdswebapi.Dtos.Promotion
{
    public class UpdatePromotionRequestDto
    {

        public string? PromoName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DiscountPercent { get; set; }
    }
}
