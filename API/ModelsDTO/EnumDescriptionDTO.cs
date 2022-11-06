namespace API.ModelsDTO
{
    public class EnumDescriptionDTO<T>
    {
        public EnumDescriptionDTO(T value, string description)
        {
            Value = value;
            Description = description;
        }

        public T Value { get; set; }
        public string Description { get; set; }
    }
}
