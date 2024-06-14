using mdswebapi.Dtos.Medicine;

namespace mdswebapi.Dtos.Pharmacy
{
    public class PharmacyDto
    {
        public int PharId { get; set; }

        public string? PharLogin { get; set; }

        public string? PharPass { get; set; }

        public string? PharName { get; set; }

        public string? PharPhone { get; set; }

        public string? PharEmail { get; set; }

        public string? PharAddress { get; set; }
        public List<MedicineDto> Medicines { get; set; }
    }
}
