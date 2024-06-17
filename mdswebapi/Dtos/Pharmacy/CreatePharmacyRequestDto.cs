namespace mdswebapi.Dtos.Pharmacy
{
    public class CreatePharmacyRequestDto
    {

        public string? PharName { get; set; }

        public string? PharPhone { get; set; }

        public string? PharEmail { get; set; }

        public string? PharAddress { get; set; }
        public string? CustomerId { get; set; }
    }
}
