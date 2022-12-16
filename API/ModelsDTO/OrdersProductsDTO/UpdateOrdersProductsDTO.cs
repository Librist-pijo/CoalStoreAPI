namespace API.ModelsDTO.OrdersProductsDTO
{
    public class UpdateOrdersProductsDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
