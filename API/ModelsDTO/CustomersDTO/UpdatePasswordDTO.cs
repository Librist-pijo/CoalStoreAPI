namespace API.ModelsDTO.CustomersDTO
{
    public class UpdatePasswordDTO
    {
        public int CustomerId { get; set; }
        public string CustomerLogin { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
