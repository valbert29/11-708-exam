using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exam_1.Models
{
    public class DbCnxt:DbContext
    {
        public DbSet<FileU> Files { get; set; }
        public DbCnxt() { }
        public DbCnxt(DbContextOptions<DbCnxt> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
