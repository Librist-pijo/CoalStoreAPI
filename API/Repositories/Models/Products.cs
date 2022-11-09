namespace API.Repositories.Models
{
    public class Products
    {
        public int? Id { get; set; }    
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
    }
}
