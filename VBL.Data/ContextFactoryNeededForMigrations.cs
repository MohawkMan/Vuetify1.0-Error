using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VBL.Data
{
    public class ContextFactoryNeededForMigrations : IDesignTimeDbContextFactory<VBLDbContext>
    {
        //private readonly IHttpContextAccessor _contextAccessor;
        //public ContextFactoryNeededForMigrations(IHttpContextAccessor contextAccessor)
        //{
        //    _contextAccessor = contextAccessor;
        //}

        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=VBL_db;Trusted_Connection=True;MultipleActiveResultSets=true";
        public VBLDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VBLDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly(typeof(VBLDbContext).GetTypeInfo().Assembly.GetName().Name));

            return new VBLDbContext(optionsBuilder.Options);
        }

    }
}
