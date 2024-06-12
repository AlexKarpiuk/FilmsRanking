using FilmsRanking.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmsRanking.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
