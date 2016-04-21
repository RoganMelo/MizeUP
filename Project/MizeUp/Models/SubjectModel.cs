using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class SubjectModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Status")]
        public virtual char Status { get; set; }

        public virtual StudentModel Student { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Schedules")]
        public virtual IList<ScheduleModel> Schedules { get; set; }

        public virtual IList<ActivityModel> Activities { get; set; }
    }
}