using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cst247_Minesweeper.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(15)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(15)]
        public string LastName { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(15)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(5)]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string State { get; set; }
    }
}