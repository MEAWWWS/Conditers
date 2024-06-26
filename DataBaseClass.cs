using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectioneryFactoryDll;

namespace Conditer
{
    public class DataBaseClass : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserJob> UserJobs { get; set; } = null!;
        public DbSet<Garbage> Garbages { get; set; } = null!;
        private static DataBaseClass db;
        private DataBaseClass(){}
        public static DataBaseClass GetDatabase()
        {
            if (db == null)
            {
                db = new DataBaseClass();
            }
            return db;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ConditerDB.db;");
        }
    }
}
