using System;
using API.Data;
using API.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyControlller : BaseApiController
    {

        public readonly DataContext _context;
      public  BuggyControlller(DataContext context)
        {
            _context = context;
        }
         [HttpGet("bad-request")]
        public ActionResult<string> getSecret1()
        {
            return BadRequest("This is bad Request");
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "This is Secret Message";
        }
         [HttpGet("not-found")]
        public ActionResult<Appuser> GetNotFound()
        {
            var thing=_context.Users.Find(-1);
            if(thing==null)    return NotFound();
            return Ok(thing);
        }
         [HttpGet("server-error")]
        public ActionResult<string> getSecret()
        {
            try
            {
                var thing = _context.Users.Find(-1);
                var thingToReturn = thing.ToString();
                return thingToReturn;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Not Exist");
            }

        }
        
        
    }
}