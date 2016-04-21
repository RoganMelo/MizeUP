using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MizeUP.Models
{
    public class LevelModel
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual char LevelType { get; set; }

        public virtual InstitutionModel Institution { get; set; }

        public virtual IList<StudentModel> Students { get; set; }
    }
}