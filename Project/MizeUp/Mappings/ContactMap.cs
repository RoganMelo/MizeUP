using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class ContactMap : ClassMap<ContactModel>
    {
        public ContactMap()
        {
            Table("contact");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Message).Not.Nullable().Length(5000);
        }
    }
}