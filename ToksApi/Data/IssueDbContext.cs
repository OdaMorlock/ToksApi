using Microsoft.EntityFrameworkCore;
using ToksApi.Models;

namespace ToksApi.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<IssueModel> Issues { get; set; }
 
    }
}
