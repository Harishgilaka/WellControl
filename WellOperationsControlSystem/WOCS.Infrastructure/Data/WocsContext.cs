using Microsoft.EntityFrameworkCore;

namespace WOCS.Infrastructure.Data;

public partial class WocsContext : DbContext
{
    public WocsContext()
    {
    }

    public WocsContext(DbContextOptions<WocsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }

    public virtual DbSet<ExproJob> ExproJobs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ExproCirrusDB-2.0;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;App=EntityFramework");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExceptionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exceptio__3214EC0757D65704");

            entity.HasIndex(e => e.IsReviewed, "IX_ExceptionLogs_IsReviewed").HasFilter("([IsReviewed]=(0))");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplicationName).HasDefaultValue("WOCS");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.IsRecoverable).HasDefaultValue(true);
            entity.Property(e => e.LogLevel).HasDefaultValue("Error");
            entity.Property(e => e.LoggedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OccurredAt).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<ExproJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproJobs");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
