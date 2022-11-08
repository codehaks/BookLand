using BookLand.Application.Books.Commands;
using BookLand.Data;
using BookLand.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookLand.Web.Areas.Admin.Pages.Books;

public class CreateModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public CreateModel(IMediator mediator, IWebHostEnvironment webHostEnvironment)
    {
        _mediator = mediator;
 
        BookInputModel = new BookInputModel
        {
            Year = 0,
            Pages = 1
        };
        _webHostEnvironment = webHostEnvironment;
    }

    [BindProperty]
    public BookInputModel BookInputModel { get; set; }

    [BindProperty]
    public IFormFile? ImageFile { get; set; }
    public async Task<IActionResult> OnPost()
    {
        #region File and validation
        var fileName = BookInputModel.Title.ToLower().Replace(" ", " - ") + ".jpg";
        if (ImageFile != null)
        {
            var path = _webHostEnvironment.WebRootPath + @"\images\" + fileName;

            using var stream = System.IO.File.Create(path);
            stream.Position = 0;
            await ImageFile.CopyToAsync(stream);
        }



        ModelState.Remove("ImageLink");

        if (ModelState.IsValid == false)
        {
            return Page();
        }
        if (BookInputModel is null)
        {
            return Page();
        }

        var imageLink = "images/" + fileName; 
        #endregion

        await _mediator.Send(new CreateBook.Command {
            Title = BookInputModel.Title,
            Year = BookInputModel.Year,
            Pages = BookInputModel.Pages,
            Author = BookInputModel.Author,
            Country = BookInputModel.Country,
            ImageLink = imageLink,
            Language = BookInputModel.Language,
            Link = BookInputModel.Link
        });

        return RedirectToPage("./index");
    }
}


public class BookInputModel
{
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "Title can not be empty")]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "Price can not be empty")]
    [Range(minimum:0,maximum:1000)]
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
    public string Link { get; set; } = default!;

}
