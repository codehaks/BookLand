using BookLand.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLand.Web.Controllers
{

    [ApiController]
    public class BookImageController : ControllerBase
    {
        private readonly BookLandDbContext _db;

        public BookImageController(BookLandDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("api/book/image/{bookid}")]
        public IActionResult Get(int bookId)
        {
            var book = _db.Books.Find(bookId);
            return File(book.ImageData,"image/jpeg",book.Title+".jpg",true);
        }
    }
}
