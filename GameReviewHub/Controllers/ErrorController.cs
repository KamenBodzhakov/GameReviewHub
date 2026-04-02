using Microsoft.AspNetCore.Mvc;

namespace GameReviewHub.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return statusCode switch
            {
                400 => View("BadRequest"),
                404 => View("NotFound"),
                500 => View("ServerError"),
                _ => View("Error")
            };
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View("ServerError");
        }
    }
}