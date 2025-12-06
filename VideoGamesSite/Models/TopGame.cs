using System.ComponentModel.DataAnnotations;

namespace VideoGamesSite.Models
{
    public class TopGame
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = "";

        [StringLength(40)]
        public string? Tag { get; set; }

        // 1–10 rank in the list
        public int Rank { get; set; }
    }
}