using FluentNHibernate.Mapping;
using MizeUP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeUP.Mappings
{
    public class EvaluationMap : SubclassMap<EvaluationModel>
    {
        public EvaluationMap()
        {
            Table("evaluation");
        }
    }
}
