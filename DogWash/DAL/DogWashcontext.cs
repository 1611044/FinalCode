using DogWash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DogWash.DAL
{
    public class DogWashcontext : DbContext
    {
        public DogWashcontext() : base("DogWashcontext")
        {
        }

        public DbSet<Pet> pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Wash> Washes { get; set; }
        public DbSet<appointment> appointments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        
    }
}