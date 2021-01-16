using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Domain.Common;
using FamilyTree.Domain.Entities.Content;
using FamilyTree.Domain.Entities.Privacy;
using FamilyTree.Domain.Entities.Tree;
using FamilyTree.Domain.Entities.UserDefinedContent;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IDateTimeService _dateTimeService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                    ICurrentUserService currentUserService,
                                    IDateTimeService dateTimeService) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<Person> People { get; set; }

        public DbSet<FamilyTie> FamilyTies { get; set; }

        public DbSet<FamilyTreeEntity> FamilyTrees { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<DataBlock> DataBlocks { get; set; }

        public DbSet<DataHolder> DataHolders { get; set; }

        public DbSet<CommonDataBlock> CommonDataBlocks { get; set; }

        public DbSet<DataHolderPrivacy> DataHolderPrivacies { get; set; }

        public DbSet<ImagePrivacy> ImagePrivacies { get; set; }

        public DbSet<VideoPrivacy> VideoPrivacies { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Video> Videos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTimeService.Now;
                        break;

                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
