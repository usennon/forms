﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IW5.API.DAL.Factories
{
    public class FormsDbContextFactory : IDesignTimeDbContextFactory<FormsDbContext>
    {
        public FormsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<FormsDbContextFactory>(optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FormsDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IW5"));
            return new FormsDbContext(optionsBuilder.Options);
        }
    }
}
