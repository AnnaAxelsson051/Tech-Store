

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
        return NotFound();
        }
          [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
        return BadRequest("This is a bad request");
        }
          [HttpGet("unauthorized")]
        public ActionResult GetUnauthorized()
        {
        return Unauthorized();
        }
          [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
        ModelState.AddModelError("Problem1", "This is the first error");
        ModelState.AddModelError("Problem2", "This is the first error");
        return ValidationProblem();
        }
          [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
        throw new Exception("This is a server error");
        }
    }
}