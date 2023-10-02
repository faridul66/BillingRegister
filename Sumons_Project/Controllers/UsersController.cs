using BillingRegister.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;

namespace BillingRegister.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, 
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Add the Group Manager (NOTE: only access through the public
        // Property, not by the instance variable!)
        private ApplicationGroupManager _groupManager;
        public ApplicationGroupManager ApplicationGroupManager
        {
            get
            {
                return _groupManager ?? new ApplicationGroupManager();
            }
            private set
            {
                _groupManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext()
                    .Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }


        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            string picfile;
            if (user.PictureFile == null || user.PictureFile == "")
            {
                picfile = "Blank-Profile-Pic.PNG";
            }
            else
            {
                picfile = user.PictureFile;
            }
            user.PictureFile = picfile;
            // Show the groups the user belongs to:
            var userGroups = await this.ApplicationGroupManager.GetUserGroupsAsync(id);
            ViewBag.GroupNames = userGroups.Select(u => u.Name).ToList();
            var context = new ApplicationDbContext();
            var ROLES = from r in context.ApplicationUserRoles join u in context.Users on r.UserId equals u.Id join rn in context.Roles on r.RoleId equals rn.Id where r.UserId == id orderby rn.Name select rn.Name;
            ViewBag.RoleNames = ROLES;
            ViewBag.RoleCounts = ROLES.Count();
           // ViewBag.BranchList = context.AssignBranches.Where(x => x.Username == user.UserName).Select(x=>x.BranchName).ToList();
            return View(user);
        }

                  
        public ActionResult Create()
        {
           
            // Show a list of available groups:
            ViewBag.GroupsList = 
                new SelectList(this.ApplicationGroupManager.Groups, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, 
            params string[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = userViewModel.UserName, 
                    Email = userViewModel.Email 
                };
                var adminresult = await UserManager
                    .CreateAsync(user, userViewModel.Password);
                

                //Add User to the selected Groups 
                if (adminresult.Succeeded)
                {
                    if (selectedGroups != null)
                    {
                        selectedGroups = selectedGroups ?? new string[] { };
                        await this.ApplicationGroupManager
                            .SetUserGroupsAsync(user.Id, selectedGroups);
                    }
                    
                    return RedirectToAction("Index");
                }
          
                AddErrors(adminresult);
            }
            ViewBag.GroupsList = new SelectList(
                await this.ApplicationGroupManager.Groups.ToListAsync(), "Id", "Name");
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #region
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            string picfile;
            if(user.PictureFile==null || user.PictureFile=="")
            {
                picfile = "Blank-Profile-Pic.PNG";
            }
            else
            {
                picfile =user.PictureFile;
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            var model = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName,
                PhoneNumber=user.PhoneNumber,
                File= picfile
            };
            
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber")] EditUserViewModel editUser, HttpPostedFileBase postedFile)
        {
           
                if (ModelState.IsValid)
                {
                    if (postedFile!=null)
                    {
                        string _FileName = Path.GetFileName(postedFile.FileName);
                        var fileExt = Path.GetExtension(postedFile.FileName).Substring(1);
                        string _path = Path.Combine(Server.MapPath("~/Images/Identity/Users/Profiles"), editUser.Id + "." + fileExt);
                        postedFile.SaveAs(_path);
                    }

                        var user = await UserManager.FindByIdAsync(editUser.Id);
                        if (user == null)
                        {
                            return HttpNotFound();
                        }

                        // Update the User:
                        user.Email = editUser.Email;
                        user.FirstName = editUser.FirstName;
                        user.LastName = editUser.LastName;
                        user.PhoneNumber = editUser.PhoneNumber;
                    if (postedFile != null)
                    {
                        var fileExt = Path.GetExtension(postedFile.FileName).Substring(1);
                        user.PictureFile = editUser.Id + "." + fileExt;
                    }
               
                        await this.UserManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                }
            
           

            ModelState.AddModelError("", "Something went wrong");
            return View();
        }
        #endregion // Edit User
        //Assign Group for User
        #region 
        public async Task<ActionResult> ff0147utsfesrogsrcoaukpn(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Display a list of available Groups:
            var allGroups = this.ApplicationGroupManager.Groups;
            var userGroups = await this.ApplicationGroupManager.GetUserGroupsAsync(id);
            var accallGroup = allGroups.OrderBy(x => x.Name);
            var model = new GroupViewfromUserList()
            {
                Id = user.Id,
                UserName=user.UserName
            };

            foreach (var group in accallGroup)
            {
                var listItem = new SelectListItem()
                {
                    Text = group.Name,
                    Value = group.Id,
                    Selected = userGroups.Any(g => g.Id == group.Id)
                };
                model.GroupsList.Add(listItem);
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ff0147utsfesrogsrcoaukpn(
            [Bind(Include = "Id")] GroupViewfromUserList gfromuserlist,
            params string[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(gfromuserlist.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                selectedGroups = selectedGroups ?? new string[] { };
                await this.ApplicationGroupManager.SetUserGroupsAsync(user.Id, selectedGroups);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        #endregion //Assign Group for User


        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                // Remove all the User Group references:
                await this.ApplicationGroupManager.ClearUserGroupsAsync(id);

                // Then Delete the User:
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
      
    }
}
