using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ImageHost.Models.Data
{
    public class ImgHostContext : DbContext
    {
        public ImgHostContext() : base("SQLCONNSTR_ImgHostDB")
        {
        }

        public DbSet<HostedImage> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new HostedImageConfiguration());
        }
    }
}