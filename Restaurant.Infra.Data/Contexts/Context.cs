﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Domain.Entities;
using Restaurant.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Infra.Data.Contexts
{
    public class Context : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public IDbContextTransaction Transaction { get; private set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            if (Database.GetPendingMigrations().Count() > 0)
                Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public IDbContextTransaction InitTransaction()
        {
            if (Transaction == null) Transaction = this.Database.BeginTransaction();
            return Transaction;
        }
        private void RollBack()
        {
            if (Transaction != null)
                Transaction.Rollback();
        }
        private void Save()
        {
            try
            {
                ChangeTracker.DetectChanges();
                SaveChanges();
            }
            catch (Exception ex)
            {
                RollBack();
                throw new Exception(ex.Message);
            }
        }
        private void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                Transaction.Dispose();
                Transaction = null;
            }
        }
        public void SendChanges()
        {
            Save();
            Commit();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DishMap());
        }
    }
}
