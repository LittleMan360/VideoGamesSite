using System.ComponentModel.DataAnnotations;

namespace VideoGamesSite.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = "";

        [Required, StringLength(60)]
        public string Genre { get; set; } = "";

        [Range(1970, 2100)]
        public int ReleaseYear { get; set; }

        [Range(0, 10000)]
        public int HoursPlayed { get; set; }

        // FK -> Platform
        public int PlatformId { get; set; }
        public Platform? Platform { get; set; }
    }
}