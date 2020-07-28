namespace GitsLibary.Models
{
    public class ApiResponeModel
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int TotalRow { get; set; }

    }
}
