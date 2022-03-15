using System.ComponentModel.DataAnnotations;

namespace BookLand.Models;

public class Book
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = default!;

    [Required]
    public int Year { get; set; }

    [Required]
    [Range(minimum: 1, maximum: 10000)]
    public int Pages { get; set; }

}
