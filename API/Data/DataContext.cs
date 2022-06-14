using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, 
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Andon> Andon { get; set; }
        public DbSet<NodeList> NodeList { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }

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