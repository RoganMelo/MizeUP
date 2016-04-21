using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class LevelMap : ClassMap<LevelModel>
    {
        public LevelMap()
        {
            Table("level");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.LevelType).Not.Nullable().Length(1);
            References(x => x.Institution).Not.Nullable().Not.LazyLoad();
            HasMany(x => x.Students);
        }
    }
}