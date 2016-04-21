using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class AccountModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "InvalidEmail")]
        [Display(ResourceType = typeof(Languages), Name = "Email")]
        public virtual string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "InvalidPassword")]
        [StringLength(30, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 8)]
        [Display(ResourceType = typeof(Languages), Name = "Password")]
        public virtual string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "InvalidPassword")]
        [StringLength(30, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 8)]
        [Compare("Password", ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "InvalidConfirmPassword")]
        [Display(ResourceType = typeof(Languages), Name = "ConfirmPassword")]
        public virtual string ConfirmPassword { get; set; }
    }
}