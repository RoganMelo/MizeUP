using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MizeUP.Models
{
    public class ActivityModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        [Required (ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(1000, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Description")]
        public virtual string Description { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Stage")]
        public virtual char Stage { get; set; }

        public virtual double Weight { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Range(0, 10, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "NumberValidate")]
        [Display(ResourceType = typeof(Languages), Name = "Grade")]
        public virtual double Grade { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages) , ErrorMessageResourceName = "Required" )]
        [Display(ResourceType= typeof(Languages), Name= "Subjects")]
        public virtual SubjectModel Subject { get; set; }
    }
}