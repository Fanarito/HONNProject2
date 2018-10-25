using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Repositories;

namespace VideotapeGalore.Tests.DataFakers
{
    public static class BogusContext
    {
        public static ApplicationContext SetupDb()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(connection)
                .Options;
            var context = new ApplicationContext(options);
            context.Database.EnsureCreated();
            for (var i = 0; i < 10; i++)
            {
                context.Add(BogusDataGenerators.GetFriend());
                context.Add(BogusDataGenerators.GetTape());
            }
            context.SaveChanges();
            
            return context;
        }
    }
}