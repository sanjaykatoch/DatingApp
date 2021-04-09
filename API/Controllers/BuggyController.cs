using System;
using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        public DataContext _context ;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret Text is Here";
        }
        // [Authorize]
        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var thing=_context.Users.Find(-1);
            if(thing==null) return NotFound();

            return Ok(thing);
        }
       // [Authorize]
        [HttpGet("server-error")]
        public ActionResult<Appuser> GetServerError()
        {
            try
            {
                var thing = _context.Users.Find(-1);
                var thingToReturn = thing.ToString();
                return Ok(thingToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
         //[Authorize]
        [HttpGet("bad-request")]
        public ActionResult<string> getBadRequest()
        {
          return BadRequest("This was not good Request");
        }
    }
}