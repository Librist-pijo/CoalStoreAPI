namespace API.ModelsDTO
{
    public class ResultData
    {
        public ResultData(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; set; }
        public string Error { get; set; }
        public string Info { get; set; }
        public object Data { get; set; }
    }
}
