using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBlog.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace SimpleBlog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}