using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class InstitutionMap : ClassMap<InstitutionModel>
    {
        public InstitutionMap()
        {
            Table("institution");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            HasMany(x => x.Levels);
        }
    }
}