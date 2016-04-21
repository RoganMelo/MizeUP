using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class StudentMap : SubclassMap<StudentModel>
    {
        public StudentMap()
        {
            Table("student");
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.LastName).Not.Nullable().Length(100);
            References(x => x.Level).Not.Nullable().Not.LazyLoad();
            References(x => x.Syllabus);
            HasMany(x => x.Subjects);
        }
    }
}