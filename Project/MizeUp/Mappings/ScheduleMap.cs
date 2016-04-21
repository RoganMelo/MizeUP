using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class ScheduleMap : ClassMap<ScheduleModel>
    {
        public ScheduleMap()
        {
            Table("schedule");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Day).Not.Nullable().Length(1);
            Map(x => x.StartTime).Not.Nullable();
            Map(x => x.EndTime).Not.Nullable();
            References(x => x.Subject);
        }
    }
}