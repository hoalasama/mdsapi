namespace mdswebapi.Dtos.Cart
{
    public class AddCartDetailDto
    {
        public string CustomerId { get; set; }
        public int MedId { get; set; }
        public int Quantity { get; set; }
    }

    public class GetCartDto
    {
        public string CustomerId { get; set; }
    }

    public class EditCartDetailDto
    {
        public string CustomerId { get; set; }
        public int CartDetailId { get; set; }
        public int Quantity { get; set; }
    }

    public class DeleteCartDetailDto
    {
        public string CustomerId { get; set; }
        public int CartDetailId { get; set; }
    }

}
