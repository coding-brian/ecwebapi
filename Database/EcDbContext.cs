using EcWebapi.Database.Table;
using Microsoft.EntityFrameworkCore;
using System;

namespace EcWebapi.Database
{
    public class EcDbContext(DbContextOptions<EcDbContext> options) : DbContext(options)
    {
        public DbSet<Member> Members { get; set; }
    }
}