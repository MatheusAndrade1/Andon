using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppAndon> Andon { get; set; }
        public DbSet<AppNodeList> NodeList { get; set; }
        public DbSet<AppUser> User { get; set; }

        // Adding a stored procedure in the context
        // public ObjectResult<AppAndon> GetAndonByID(Nullable<int> andonID)
        // {
        //     var andonParameterId = andonID.HasValue ?
        //         new ObjectParameter("AndonId", andonID) :
        //         new ObjectParameter("AndonId", typeof(int));

        //     return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AppAndon>("GetAndonByID", andonParameterId);
        // }
    }
}