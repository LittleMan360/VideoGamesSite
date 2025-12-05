using System.ComponentModel.DataAnnotations;

namespace VideoGamesSite.Models
{
    public class NowPlayingItem
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Title { get; set; } = "";

        [Required, StringLength(40)]
        public string Platform { get; set; } = "";

        // What you're focusing on: story, builds, co-op, etc.
        [StringLength(200)]
        public string? Focus { get; set; }

        // 0–100 percent
        [Range(0, 100)]
        public int Progress { get; set; }

        // Used to control ordering in the list (for move up/down)
        public int DisplayOrder { get; set; }
    }
}
