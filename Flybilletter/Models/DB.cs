using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class DB : DbContext
    {
        public DB() : base("name=Flybilletter")
        {
            Database.CreateIfNotExists();
        }
        
        //TODO create DBSet

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}