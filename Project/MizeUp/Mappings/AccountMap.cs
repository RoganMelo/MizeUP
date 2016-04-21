using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MizeUP.Mappings
{
    public class AccountMap : ClassMap<AccountModel>
    {
        public AccountMap()
        {
            Table("account");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Email).Unique().Not.Nullable();
            Map(x => x.Password).Not.Nullable();
        }
    }
}