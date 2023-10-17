using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeBook.Models;
using System.Threading.Tasks;
using RecipeBook.ViewModels;

namespace RecipeBook.Controllers
{
  public class AccountController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
  }
}
