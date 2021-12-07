using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(x =>
            {
                x.ToTable("addresses");
            });

            modelBuilder.Entity<Person>(x =>
            {
                x.ToTable("persons");
            });
            
            modelBuilder.Entity<Patient>(x =>
            {
                x.ToTable("patients");
                x.HasIndex(y => y.PersonId).IsUnique();
            });

            modelBuilder.Entity<Employee>(x =>
            {
                x.ToTable("employees");
                x.HasIndex(y => y.PersonId).IsUnique();
            });

            modelBuilder.Entity<EmployeePermits>(x =>
            {
                x.ToTable("employee_permits");
            });

            modelBuilder.Entity<EmployeeSalary>(x =>
            {
                x.ToTable("employee_salaries");
                x.HasIndex(y => y.EmployeeId).IsUnique();
            });

            modelBuilder.Entity<Doctor>(x =>
            {
                x.ToTable("doctors");
                x.HasIndex(y => y.EmployeeId).IsUnique();
            });

            modelBuilder.Entity<Secretary>(x =>
            {
                x.ToTable("secretaries");
                x.HasIndex(y => y.EmployeeId).IsUnique();
            });

            modelBuilder.Entity<Housekeeper>(x =>
            {
                x.ToTable("housekeepers");
                x.HasIndex(y => y.EmployeeId).IsUnique();
            });
            
            modelBuilder.Entity<SecretaryDoctor>(x =>
            {
                x.ToTable("secretary_doctors");
            });

            modelBuilder.Entity<HousekeeperResponsibleFloor>(x =>
            {
                x.ToTable("housekeeper_responsible_floors");
            });

            modelBuilder.Entity<Reservation>(x =>
            {
                x.ToTable("reservations");
            });

            modelBuilder.Entity<PhysicalExamination>(x =>
            {
                x.ToTable("physical_examinations");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePermits> EmployeePermits { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Secretary> Secretaries { get; set; }
        public DbSet<Housekeeper> Housekeepers { get; set; }
        public DbSet<SecretaryDoctor> SecretaryDoctors { get; set; }
        public DbSet<HousekeeperResponsibleFloor> HousekeeperResponsibleFloors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PhysicalExamination> PhysicalExaminations { get; set; }
    }
}