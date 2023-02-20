using System;
using System.Collections.Generic;
using IdentityWithExistingDb.Core.Models;
using IdentityWithExistingDb.Core.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityWithExistingDb.Ef.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Dependant> Dependants { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<WorkOn> WorkOns { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomConfiguration());

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
                                                                                  
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
                                                                                  
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
                                                                                  
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Dependant>(entity =>
            {
                entity.HasIndex(e => e.EmpId, "IX_Dependants_Emp_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.EmpId).HasColumnName("Emp_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Dependants)
                    .HasForeignKey(d => d.EmpId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.DeptId, "IX_Employees_dept_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.DeptId).HasColumnName("dept_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DeptId);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasIndex(e => e.Departmentid, "IX_Projects_departmentid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Departmentid).HasColumnName("departmentid");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Departmentid);
            });

            modelBuilder.Entity<WorkOn>(entity =>
            {
                entity.HasKey(e => new { e.Pid, e.Eid });

                entity.ToTable("WorkOn");

                entity.HasIndex(e => e.Eid, "IX_WorkOn_Eid");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.HasOne(d => d.EidNavigation)
                    .WithMany(p => p.WorkOns)
                    .HasForeignKey(d => d.Eid)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.WorkOns)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
