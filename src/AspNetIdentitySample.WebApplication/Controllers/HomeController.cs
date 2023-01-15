using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentitySample.WebApplication.Controllers
{
  [Route("")]
  public sealed class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
