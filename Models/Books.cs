namespace MinimalApi.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; }
        public int  PageCount { get; set; }
        public string CoverImageUrl { get; set; }
    }

    public class UploadImage: Books
    {
        public IFormFile image { get; set; }
    }
}