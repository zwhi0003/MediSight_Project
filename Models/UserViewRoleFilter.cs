using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSight_Project.Models
{
    [NotMapped]
    public class UserViewRoleFilter
    {
        public List<ApplicationUser> Users { get; set; }
        public SelectList Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}