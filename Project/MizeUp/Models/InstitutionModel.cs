﻿using MizeUP.Internacionalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class InstitutionModel 
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Languages), ErrorMessageResourceName = "LengthMinMax", MinimumLength = 3)]
        [Display(ResourceType = typeof(Languages), Name = "Name")]
        public virtual string Name { get; set; }

        public virtual IList<LevelModel> Levels { get; set; }
    }
}