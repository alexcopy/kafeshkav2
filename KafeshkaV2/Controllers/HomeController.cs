using System.Diagnostics;
using KafeshkaV2.Areas.Identity;
using Microsoft.AspNetCore.Mvc;
using KafeshkaV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KafeshkaV2.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<KafeshkaAppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<KafeshkaAppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }


    public IActionResult Index()
    {
        ViewData["UserID"] = _userManager.GetUserId(User);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}