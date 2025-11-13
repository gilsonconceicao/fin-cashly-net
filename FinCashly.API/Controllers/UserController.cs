using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FinCashly.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController()
        {
            
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }
    }
}