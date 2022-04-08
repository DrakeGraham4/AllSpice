namespace AllSpice.Models
{
    public class Recipe
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Category { get; set; }

        public string ImgUrl { get; set; }

        public string creatorId { get; set; }

        public Account? Creator { get; set; }
    }
}
