using MvcDotNet_BTH.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcDotNet_BTH.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Student> Students {get; set;}
  }
}