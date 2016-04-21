using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeUP.Mappings
{
    public class TaskMap : SubclassMap<TaskModel>
    {
        public TaskMap()
        {
            Table("task");
            Map(x => x.DeadLine).Not.Nullable();
            Map(x => x.Status).Not.Nullable().Length(1);
        }
    }
}
