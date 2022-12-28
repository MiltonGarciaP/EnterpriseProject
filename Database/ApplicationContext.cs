using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacantion> Vacantions { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region table
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Vacantion>().ToTable("Vacations");
            modelBuilder.Entity<Payroll>().ToTable("Payrolls");
            #endregion

            #region primary keys
            modelBuilder.Entity<Employee>().HasKey(employee => employee.Id);
            modelBuilder.Entity<Vacantion>().HasKey(vacantion => vacantion.Id);
            modelBuilder.Entity<Payroll>().HasKey(payroll => payroll.Id);
            modelBuilder.Entity<Position>().HasKey(position => position.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Payroll>()
                .HasMany<Employee>(payroll => payroll.Employees)
                .WithOne(employee => employee.Payroll)
                .HasForeignKey(employee => employee.PayrollId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vacantion>()
                .HasMany<Employee>(vacantion => vacantion.Employees)
                .WithOne(employee => employee.Vacantion)
                .HasForeignKey(employee => employee.VacantionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Position>()
                .HasMany<Employee>(position => position.Employees)
                .WithOne(employee => employee.Position)
                .HasForeignKey(employee => employee.PositionId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region property configurations

            #region employee
                modelBuilder.Entity<Employee>().Property(employee => employee.EmployeeName).IsRequired().HasMaxLength(70);
                modelBuilder.Entity<Employee>().Property(employee => employee.Email).IsRequired().HasMaxLength(50);
                modelBuilder.Entity<Employee>().Property(employee => employee.PhoneNumber).IsRequired().HasMaxLength(10);
                modelBuilder.Entity<Employee>().Property(employee => employee.DOB).IsRequired();
                modelBuilder.Entity<Employee>().Property(employee => employee.DOA).IsRequired();
                modelBuilder.Entity<Employee>().Property(employee => employee.IdCard).IsRequired();
            #endregion

            #region payroll
                modelBuilder.Entity<Payroll>().Property(payroll => payroll.Earnings).IsRequired();
            #endregion

            #region vacantion
            modelBuilder.Entity<Vacantion>().Property(vacantion => vacantion.VacationName).IsRequired().HasMaxLength(55);
            modelBuilder.Entity<Vacantion>().Property(vacantion => vacantion.StartingDate).IsRequired();
            modelBuilder.Entity<Vacantion>().Property(vacantion => vacantion.EndingDate).IsRequired();
            #endregion

            #region position
            modelBuilder.Entity<Position>().Property(position => position.PositionName).IsRequired().HasMaxLength(55);
            #endregion

            #endregion
        }
    }
}
