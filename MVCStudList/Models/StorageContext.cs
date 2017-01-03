namespace MVCStudList.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public partial class StorageContext : DbContext
    {
        public StorageContext()
            : base("name=StorageContext")
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
