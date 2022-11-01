using Microsoft.AspNetCore.Mvc;

namespace BookLand.Web.ViewComponents;

[ViewComponent]
public class ImageBox : ViewComponent
{
    public ImageBoxViewModel ViewModel { get; set; }
    public async Task<IViewComponentResult> InvokeAsync(byte[] content,string style,string alt)
    {
        ViewModel=new ImageBoxViewModel();

        ViewModel.ImageBase64=Convert.ToBase64String(content);
        ViewModel.Style = style;
        ViewModel.Alt = alt;
        return View("Default",ViewModel); 
    }
}

public class ImageBoxViewModel
{
    public string ImageBase64 { get; set; }
    public string Style { get; set; }
    public string Alt { get; set; }
}