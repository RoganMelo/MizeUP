using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class SyllabusModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "StartTime")]
        public virtual DateTime StartTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Time, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Availability")]
        public virtual DateTime Availability { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Objective")]
        public virtual char Objective { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}