using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MizeUP.Models
{
    public class TaskModel : ActivityModel
    {
        [Required(ErrorMessageResourceType = typeof(Languages) , ErrorMessageResourceName = "Required")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages), Name = "Deadline")]
        public virtual DateTime DeadLine { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages) , ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(Languages) , Name = "Status")]
        public virtual char Status { get; set; }
    }
}