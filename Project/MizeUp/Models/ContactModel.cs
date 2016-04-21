using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class ContactModel
    {
        public virtual int Id { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "InvalidEmail")]
        [Display(ResourceType = typeof(Languages), Name = "Email")]
        public virtual string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(5000, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Message")]
        public virtual string Message { get; set; }
    }
}