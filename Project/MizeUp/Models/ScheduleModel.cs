using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class ScheduleModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Day")]
        public virtual char Day { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "StartTime")]
        public virtual DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "EndTime")]
        public virtual DateTime EndTime { get; set; }

        public virtual SubjectModel Subject { get; set; }
    }
}