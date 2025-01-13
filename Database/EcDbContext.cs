﻿using EcWebapi.Database.Table;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcWebapi.Database
{
    public class EcDbContext(DbContextOptions<EcDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberCaptcha> MemberCaptchas { get; set; }
    }
}