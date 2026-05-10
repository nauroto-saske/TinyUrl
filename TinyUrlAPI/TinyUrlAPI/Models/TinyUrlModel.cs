namespace TinyUrlAPI.Models
{
    public class TinyUrlModel
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string ShortCode { get; set; }   
        public bool IsPrivate { get; set; }
        public int Clicks { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
