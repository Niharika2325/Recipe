using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookingRecipe.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; } // Note: Password should be hashed in production

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }
    }
    public class UserFieldUpdate
    {
        public string Field { get; set; }
        public string Value { get; set; }
    }
}