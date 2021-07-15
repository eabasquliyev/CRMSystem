﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CrmSystem.Domain;
using CrmSystem.Domain.Models;
using CrmSystem.Domain.Repositories;
using CrmSystem.EntityFramework.Repositories;

namespace CrmSystem.EntityFramework
{
    public class UnitOfWork:IUnitOfWork
    {
        private CrmSystemContext _context;

        public UnitOfWork(CrmSystemContext context)
        {
            _context = context;

            Contacts = new ContactRepository(_context);
            Employees = new EmployeeRepository(_context);
            Contacts = new ContactRepository(_context);
            Products = new Repository<Product>(_context);
            Tasks = new Repository<Task>(_context);
            LeadSources = new Repository<LeadSource>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IContactRepository Contacts { get; }
        public IEmployeeRepository Employees { get; }
        public IContractRepository Contracts { get; }
        public IRepository<Product> Products { get;}
        public IRepository<Task> Tasks { get; }
        public IRepository<LeadSource> LeadSources { get; }

        //public void ExplicitLoading<TEntity2>(Expression<Func<TEntity2, bool>> predicate) where TEntity2 : class
        //{
        //    _context.Set<TEntity2>().Where(predicate).Load();
        //}
        
        public void Save()
        {
            foreach (var item in _context.ChangeTracker.Entries<Contact>())
            {
                Console.WriteLine(item.State);
            }
            _context.SaveChanges();
        }
    }
}