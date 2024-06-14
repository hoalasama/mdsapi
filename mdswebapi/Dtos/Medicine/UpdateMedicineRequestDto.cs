namespace mdswebapi.Dtos.Medicine
{
    public class UpdateMedicineRequestDto
    {
        public string? MedName { get; set; }

        public string? MedDesc { get; set; }

        public int? MedDiscount { get; set; }

        public int? MedRemain { get; set; }

        public decimal? MedPrice { get; set; }

        public string? MedPicture { get; set; }
        public int? CateId { get; set; }
    }
}
