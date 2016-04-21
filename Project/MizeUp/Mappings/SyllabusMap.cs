using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class SyllabusMap : ClassMap<SyllabusModel>
    {
        public SyllabusMap()
        {
            Table("syllabus");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.StartTime).Not.Nullable();
            Map(x => x.Availability).Not.Nullable();
            Map(x => x.Objective).Not.Nullable().Length(1);
            References(x => x.Student);
        }
    }
}