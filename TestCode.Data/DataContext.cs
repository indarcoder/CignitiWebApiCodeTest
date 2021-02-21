using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestCode.Domain.Posts;

namespace TestCode.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {

        }
        public DbSet<Post> posts { get; set; }
    }
}
