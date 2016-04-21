using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class ActivityMap : ClassMap<ActivityModel>
    {
        public ActivityMap()
        {
            Table("activity");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.Description).Not.Nullable().Length(1000);
            Map(x => x.Stage).Not.Nullable().Length(100);
            Map(x => x.Weight).Not.Nullable();
            Map(x => x.Grade).Nullable();
            References(x => x.Subject).Not.LazyLoad();
        }
    }
}