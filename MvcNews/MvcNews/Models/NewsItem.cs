using System.ComponentModel.DataAnnotations;

namespace MvcNews.Models
{
    public class NewsItem
    {
        public int Id { get; set; }

        [DataType(dataType: DataType.Date)]
        public DateTime TimeStamp { get; set; }

        [Required]
        [StringLength(maximumLength: 140, MinimumLength = 5)]
        public string Text { get; set; } = string.Empty;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
