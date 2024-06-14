namespace mdswebapi.Dtos.Promotion
{
    public class PromotionDto
    {
        public int PromoId { get; set; }

        public string? PromoName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? DiscountPercent { get; set; }
    }
}
