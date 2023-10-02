using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace BillingRegister.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        //public EditUserViewModel()
        //{
        //    this.RolesList = new List<SelectListItem>();
        //    this.GroupsList = new List<SelectListItem>();
        //}
        public string Id { get; set; }
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string File { get; set; }
        public ICollection<SelectListItem> RolesList { get; set; }
        //public ICollection<SelectListItem> GroupsList { get; set; }
    }

    public class GroupViewModel
    {
        public GroupViewModel()
        {
            this.UsersList = new List<SelectListItem>();
            this.RolesList = new List<SelectListItem>();
        }
        //[Required(AllowEmptyStrings = false)]
        public string Id { get; set; }
        //[Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<SelectListItem> UsersList { get; set; }
        public ICollection<SelectListItem> RolesList { get; set; }
    }

    #region 
    public class GroupViewfromUserList
    {
        public GroupViewfromUserList()
        {
            this.RolesList = new List<SelectListItem>();
            this.GroupsList = new List<SelectListItem>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public ICollection<SelectListItem> RolesList { get; set; }
        public ICollection<SelectListItem> GroupsList { get; set; }
    }
    #endregion //Users Group from User Page -- 17-August-2017
    public class AssignBranch
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string BranchName { get; set; }
    }
   public class BranchViewfromUserList
    {
        public BranchViewfromUserList()
        {
            this.BranchList = new List<SelectListItem>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public ICollection<SelectListItem> BranchList { get; set; }
    }
}