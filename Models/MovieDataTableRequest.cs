namespace RedFlickMVC.Models
{
    public class MovieDataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}
