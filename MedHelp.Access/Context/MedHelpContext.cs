using MedHelp.Access.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access.Context
{
    public class MedHelpContext : DbContext
    {
        public MedHelpContext()
        { }

        public MedHelpContext(DbContextOptions<MedHelpContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Sex> Sexes { get; set; } = null!;
        public DbSet<Tolon> Tolons { get; set; } = null!;
        public DbSet<Disease> Diseases { get; set; } = null!;
        public DbSet<Reception> Receptions { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MedHelp;Integrated Security=True");
        }
    }
}
