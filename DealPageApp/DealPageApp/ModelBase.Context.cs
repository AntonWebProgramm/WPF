﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DealPageApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BD_SecuritesEntities : DbContext
    {
        private static BD_SecuritesEntities _context;
        public BD_SecuritesEntities()
            : base("name=BD_SecuritesEntities")
        {
        }

        public static BD_SecuritesEntities GetContext()
        {
            if (_context == null)
                _context = new BD_SecuritesEntities();

            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountPlan> AccountPlans { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<DealKind> DealKinds { get; set; }
        public virtual DbSet<DealPlace> DealPlaces { get; set; }
        public virtual DbSet<DealType> DealTypes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<SubAccount> SubAccounts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tiker> Tikers { get; set; }
    }
}
