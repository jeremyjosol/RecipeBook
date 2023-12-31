using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Controllers
{
  public class RoleController : Controller  
  {  
    private readonly RoleManager<IdentityRole> _roleManager;  
    private readonly UserManager<ApplicationUser> _userManager;  
    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)  
    {  
      _roleManager = roleManager;  
      _userManager = userManager;  
    }

    private void Errors(IdentityResult result)
    {
      foreach (IdentityError error in result.Errors)
      ModelState.AddModelError("", error.Description);
    }

    [Authorize(Roles = "Admin")]
    public ViewResult Index()
    {
      List<IdentityRole> roles = _roleManager.Roles.ToList();
      Dictionary<IdentityRole, List<ApplicationUser>> usersWithRole = new Dictionary<IdentityRole, List<ApplicationUser>>();

      foreach (IdentityRole role in roles)
      {
        List<ApplicationUser> usersInRole = _userManager.GetUsersInRoleAsync(role.Name).Result.ToList();
        usersWithRole.Add(role, usersInRole);
      }
      return View(usersWithRole);
    }

    public IActionResult Create() => View();
      
    [HttpPost]
    public async Task<IActionResult> Create([Required] string name)
    {
      if (ModelState.IsValid)
      {
        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
        return RedirectToAction("Index");
        else
        Errors(result);
      }
      return View(name);
    }
        
    [HttpGet]
    public IActionResult Assign()
    {
      List<IdentityRole> roles = _roleManager.Roles.ToList();
      List<ApplicationUser> users = _userManager.Users.ToList();
      ViewBag.RolesList = new SelectList(roles, "Name", "Name");
      ViewBag.UsersList = new SelectList(users, "Id", "UserName");
      return View();
    }
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Assign(string UserId, string RoleName)
    {
      try
      {
        if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(RoleName))
        {
          ApplicationUser user = await _userManager.FindByIdAsync(UserId);
          if (user != null)
          {
            // interesting case of a type cast conversion error, cannot do List but IList
            IList<string> currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Count > 0)
            {
              IdentityResult removeRole = await _userManager.RemoveFromRolesAsync(user, currentRoles);
              if (!removeRole.Succeeded)
              {
                Errors(removeRole);
                return View();
              }
            }
            if (await _roleManager.RoleExistsAsync(RoleName))
            {
              IdentityResult addRole = await _userManager.AddToRoleAsync(user, RoleName);
              if (addRole.Succeeded)
              {
                return RedirectToAction("Index");
              }
              else
              {
                Errors(addRole);
                return View();
              }
            }
          }
        }
        return View();
      }
      catch
      {
        return View();
      }
    }

    [HttpGet]
    public IActionResult Delete()
    {
      List<IdentityRole> roles = _roleManager.Roles.ToList();
      List<ApplicationUser> users = _userManager.Users.ToList();
      ViewBag.RolesList = new SelectList(roles, "Name", "Name");
      ViewBag.UsersList = new SelectList(users, "Id", "UserName");
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(string UserId, string RoleName)
    {
      try
      {
        if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(RoleName))
        {
          ApplicationUser user = await _userManager.FindByIdAsync(UserId);
          if (user != null)
          {
            if (await _userManager.IsInRoleAsync(user, RoleName))
            {
              IdentityResult removeResult = await _userManager.RemoveFromRoleAsync(user, RoleName);
              if (removeResult.Succeeded)
              {
                return RedirectToAction("Index");
              }
              else
              {
                Errors(removeResult);
                return View();
              }
            }
            else
            {
              ModelState.AddModelError("", "User does not belong to specified role.");
              return View();
            }
          }
        }
        return View();
      }
      catch
      {
        return View();
      }
    }
  }
}