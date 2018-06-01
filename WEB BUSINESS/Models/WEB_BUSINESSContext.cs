using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using business;

namespace WEB_BUSINESS.Models
{
    public class WEB_BUSINESSContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WEB_BUSINESSContext() : base("DefaultConnection")
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
		}

		public System.Data.Entity.DbSet<Subject> Subjects { get; set; }
		public System.Data.Entity.DbSet<ActivitiesClass> Activites { get; set; }
	}

}
