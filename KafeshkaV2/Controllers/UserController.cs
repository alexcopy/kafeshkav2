using KafeshkaV2.BL.interfaces;
using KafeshkaV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeshkaV2.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserBL _userBl;

    public UserController(ILogger<UserController> logger, IUserBL userBl)
    {
        _logger = logger;
        _userBl = userBl;
    }

    [HttpGet]
    public IActionResult User()
    {
        return View();
    }

    [HttpPost]
    public IActionResult User(LoginModelView model)
    {
        int? authenticate = _userBl.Authenticate(model.email, model.password);
        if (authenticate != null)
        {
            return View(_userBl.GetUserById(authenticate.Value));
        }

        return View();
    }
}