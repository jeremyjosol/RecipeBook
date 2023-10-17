﻿using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBook.Controllers
{
    public class HomeController : Controller
    {
      private readonly RecipeBoxContext _db;
      private readonly UserManager<ApplicationUser> _userManager;

      public HomeController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
      {
        _userManager = userManager;
        _db = db;
      }
    
    public ActionResult Index()
    {
      return View();
    }
    }
}