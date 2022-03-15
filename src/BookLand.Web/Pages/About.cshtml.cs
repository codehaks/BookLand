using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLand.Web.Pages
{
    public class AboutModel : PageModel
    {

        public string TimeOfDay { get; set; }
        public void OnGet()
        {
            ViewData["message"] = "Hello from codehaks.com";
            TimeOfDay = DateTime.Now.TimeOfDay.ToString();
        }
    }
}
