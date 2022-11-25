using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        public IActionResult Index()
        {
            // Book id should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                // return new BadRequestResult();
                return BadRequest("Book id is not supplied");
            }

            // Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                return BadRequest("Book id can't be null or empty");
            }

            // Book id should be between 1 to 1000
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);

            if (bookId <= 0)
            {
                return BadRequest("Book id can't be less then or equal to zero");
            } 
            
            if (bookId > 1000)
            {
                return NotFound("Book id can't be greater than 1000");
            }

            // isLoggedin should be true
            if (Convert.ToBoolean(Request.Query["isLoggedin"]) == false)
            {
                // return Unauthorized("User must be authenticated");
                return StatusCode(401);
            }

            // return File("/sample.pdf", "application/pdf");

            // 302 - Found
            // return new RedirectToActionResult("Books", "Store", new { });

            // 301 - Moved Permanently
            return new RedirectToActionResult("Books", "Store", new { }, true);
        }
    }
}
