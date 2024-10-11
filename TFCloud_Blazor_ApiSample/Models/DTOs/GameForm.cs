using System.ComponentModel.DataAnnotations;

namespace TFCloud_Blazor_ApiSample.Models.DTOs
{
    public class GameForm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public string Synopsis { get; set; }
    }
}
