using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class SubjectMap : ClassMap<SubjectModel>
    {
        public SubjectMap()
        {
            Table("subject");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.Status).Not.Nullable().Length(1);
            References(x => x.Student);
            HasMany(x => x.Schedules);
            HasMany(x => x.Activities);
        }
    }
}