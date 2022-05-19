using BlazorServerApp20220519.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.Services
{
    public class EntityFrameworkService : DbContext
    {
        private readonly string conStr;

        public EntityFrameworkService()
        {
            conStr = StaticConfigService.Configuration.GetConnectionString("TestDbStr");
        }

        public DbSet<StudentModel> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conStr);
        }
    }
}
