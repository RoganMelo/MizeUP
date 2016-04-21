using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class StudentModel : AccountModel
    {
        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "LastName")]
        public virtual string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Level")]
        public virtual LevelModel Level { get; set; }

        public virtual SyllabusModel Syllabus { get; set; }

        public virtual IList<SubjectModel> Subjects { get; set; }
    }
}