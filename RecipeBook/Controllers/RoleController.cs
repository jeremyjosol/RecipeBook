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

namespace RecipeBook.Controllers
{
 public class RoleController : Controller  
    {  
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly UserManager<IdentityUser> _userManager;  
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)  
        {  
            _roleManager = roleManager;  
            _userManager = userManager;  
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<ActionResult> Create(string Name)  
        {  
            try  
            {  
                if (!string.IsNullOrEmpty(Name))  
                {  
                    if (!(await _roleManager.RoleExistsAsync(Name)))  
                    {  
                        await _roleManager.CreateAsync(new IdentityRole(Name));  
                        return RedirectToAction("Index");  
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