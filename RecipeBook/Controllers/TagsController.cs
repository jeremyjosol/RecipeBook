using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class TagsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public TagsController(RecipeBoxContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Tags.ToList());
    }
  }
}