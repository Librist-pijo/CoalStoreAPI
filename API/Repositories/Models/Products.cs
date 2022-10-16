namespace API.Repositories.Models
{
    public class Products
    {
        private int Id { get; set; }    
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
    }
}
