using System.ComponentModel.DataAnnotations;

namespace BookLand.Models;

public class Book
{
    public int Id { get; set; }

    public Category Category { get; set; }
    public int CategoryId { get; set; }

    [Required(ErrorMessage ="Title can not be empty")]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    public int Price { get; set; }
    
    [Required(ErrorMessage = "Author can not be empty")]
    [MaxLength(100)]
    public string Author { get; set; } = default!;


    [Required(ErrorMessage = "Year must be a number")]
    public int Year { get; set; } = default;

    [Range(minimum: 1, maximum: 10000, ErrorMessage = "Page must be a number larger than 1")]
    [Required(ErrorMessage = "Page must be a number larger than 1")]
    public int Pages { get; set; } = 1;

    public string Country { get; set; } = default!;

    public string Language { get; set; } = default!;

    public string ImageLink { get; set; } = default!;

    public string Link { get; set; } = default!;

}
