using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    public class BuggyController:BaseController
    {
        [HttpGet("auth")]
        public IActionResult Getauth()
        {
            return Unauthorized();
        }
        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server Error");
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadrequest()
        {
            return BadRequest("This is a bad request");
        }
        //[HttpGet("auth")]
        //public IActionResult Getauth()
        //{
        //    return Unauthorized();
        //}
    }
}
