using System;
using System.Collections.Generic;
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

    public virtual DbSet<ExproClient> ExproClients { get; set; }

    public virtual DbSet<ExproFluidType> ExproFluidTypes { get; set; }

    public virtual DbSet<ExproJob> ExproJobs { get; set; }

    public virtual DbSet<ExproWell> ExproWells { get; set; }

    public virtual DbSet<ExproWellType> ExproWellTypes { get; set; }

    public virtual DbSet<LynxAssembly> LynxAssemblies { get; set; }

    public virtual DbSet<LynxAssemblyDevice> LynxAssemblyDevices { get; set; }

    public virtual DbSet<LynxAssemblySceduleActionType> LynxAssemblySceduleActionTypes { get; set; }

    public virtual DbSet<LynxAssemblyScheduleAction> LynxAssemblyScheduleActions { get; set; }

    public virtual DbSet<LynxAssemblyScheduleActionBlock> LynxAssemblyScheduleActionBlocks { get; set; }

    public virtual DbSet<LynxChirpFrequencyRange> LynxChirpFrequencyRanges { get; set; }

    public virtual DbSet<LynxConfigurationDocument> LynxConfigurationDocuments { get; set; }

    public virtual DbSet<LynxDevice> LynxDevices { get; set; }

    public virtual DbSet<LynxDeviceDataReading> LynxDeviceDataReadings { get; set; }

    public virtual DbSet<LynxDeviceDataRecovery> LynxDeviceDataRecoveries { get; set; }

    public virtual DbSet<LynxDeviceDataRecoveryType> LynxDeviceDataRecoveryTypes { get; set; }

    public virtual DbSet<LynxDeviceGroup> LynxDeviceGroups { get; set; }

    public virtual DbSet<LynxDeviceGroupDevice> LynxDeviceGroupDevices { get; set; }

    public virtual DbSet<LynxDeviceType> LynxDeviceTypes { get; set; }

    public virtual DbSet<LynxFrequencyBand> LynxFrequencyBands { get; set; }

    public virtual DbSet<LynxLink> LynxLinks { get; set; }

    public virtual DbSet<LynxLinkPowerLevel> LynxLinkPowerLevels { get; set; }

    public virtual DbSet<LynxLrcmTransmissionSpeed> LynxLrcmTransmissionSpeeds { get; set; }

    public virtual DbSet<LynxOperation> LynxOperations { get; set; }

    public virtual DbSet<LynxOperationTask> LynxOperationTasks { get; set; }

    public virtual DbSet<LynxOperationTaskStatus> LynxOperationTaskStatuses { get; set; }

    public virtual DbSet<LynxOperationTaskType> LynxOperationTaskTypes { get; set; }

    public virtual DbSet<LynxOperationType> LynxOperationTypes { get; set; }

    public virtual DbSet<LynxOperationVersion> LynxOperationVersions { get; set; }

    public virtual DbSet<LynxRouteSegment> LynxRouteSegments { get; set; }

    public virtual DbSet<LynxStation> LynxStations { get; set; }

    public virtual DbSet<LynxTimingMetric> LynxTimingMetrics { get; set; }

    public virtual DbSet<LynxTool> LynxTools { get; set; }

    public virtual DbSet<__MigrationHistory> __MigrationHistories { get; set; }

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

        modelBuilder.Entity<ExproClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproClients");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ExproFluidType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproFluidTypes");
        });

        modelBuilder.Entity<ExproJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproJobs");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.ExproJobs).HasConstraintName("FK_dbo.ExproJobs_dbo.ExproClients_ClientId");

            entity.HasMany(d => d.ExproWells).WithMany(p => p.ExproJobs)
                .UsingEntity<Dictionary<string, object>>(
                    "ExproWellJob",
                    r => r.HasOne<ExproWell>().WithMany()
                        .HasForeignKey("ExproWell_Id")
                        .HasConstraintName("FK_dbo.ExproWellJobs_dbo.ExproWells_ExproWell_Id"),
                    l => l.HasOne<ExproJob>().WithMany()
                        .HasForeignKey("ExproJob_Id")
                        .HasConstraintName("FK_dbo.ExproWellJobs_dbo.ExproJobs_ExproJob_Id"),
                    j =>
                    {
                        j.HasKey("ExproJob_Id", "ExproWell_Id").HasName("PK_dbo.ExproWellJobs");
                        j.ToTable("ExproWellJobs");
                        j.HasIndex(new[] { "ExproJob_Id" }, "IX_ExproJob_Id");
                        j.HasIndex(new[] { "ExproWell_Id" }, "IX_ExproWell_Id");
                    });
        });

        modelBuilder.Entity<ExproWell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproWells");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.FluidType).WithMany(p => p.ExproWells).HasConstraintName("FK_dbo.ExproWells_dbo.ExproFluidTypes_FluidTypeId");

            entity.HasOne(d => d.WellType).WithMany(p => p.ExproWells).HasConstraintName("FK_dbo.ExproWells_dbo.ExproWellTypes_WellTypeId");
        });

        modelBuilder.Entity<ExproWellType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ExproWellTypes");
        });

        modelBuilder.Entity<LynxAssembly>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxAssemblies");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Station).WithMany(p => p.LynxAssemblies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblies_dbo.LynxStations_StationId");
        });

        modelBuilder.Entity<LynxAssemblyDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxAssemblyDevices");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirmwareVersion).IsFixedLength();

            entity.HasOne(d => d.Assembly).WithMany(p => p.LynxAssemblyDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblyDevices_dbo.LynxAssemblies_AssemblyId");

            entity.HasOne(d => d.LynxChirpFrequencyRange).WithMany(p => p.LynxAssemblyDevices).HasConstraintName("FK_dbo.LynxAssemblyDevices_dbo.LynxChirpFrequencyRanges_LynxChirpFrequencyRangeId");

            entity.HasOne(d => d.LynxDevice).WithMany(p => p.LynxAssemblyDevices).HasConstraintName("FK_dbo.LynxAssemblyDevices_dbo.LynxDevices_LynxDeviceId");

            entity.HasOne(d => d.LynxDeviceType).WithMany(p => p.LynxAssemblyDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblyDevices_dbo.LynxDeviceTypes_LynxDeviceTypeId");
        });

        modelBuilder.Entity<LynxAssemblySceduleActionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxAssemblySceduleActionTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxAssemblyScheduleAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxAssemblyScheduleActions");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LynxAssemblySceduleActionType).WithMany(p => p.LynxAssemblyScheduleActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblyScheduleActions_dbo.LynxAssemblySceduleActionTypes_LynxAssemblySceduleActionTypeId");

            entity.HasOne(d => d.LynxAssemblyScheduleActionBlock).WithMany(p => p.LynxAssemblyScheduleActions).HasConstraintName("FK_dbo.LynxAssemblyScheduleActions_dbo.LynxAssemblyScheduleActionBlocks_LynxAssemblyScheduleActionBlockId");

            entity.HasOne(d => d.TargetAssembly).WithMany(p => p.LynxAssemblyScheduleActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblyScheduleActions_dbo.LynxAssemblies_TargetAssemblyId");

            entity.HasOne(d => d.TargetDevice).WithMany(p => p.LynxAssemblyScheduleActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxAssemblyScheduleActions_dbo.LynxAssemblyDevices_TargetDeviceId");
        });

        modelBuilder.Entity<LynxAssemblyScheduleActionBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxAssemblyScheduleActionBlocks");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AssemblyDeviceReference).WithMany(p => p.LynxAssemblyScheduleActionBlocks).HasConstraintName("FK_dbo.LynxAssemblyScheduleActionBlocks_dbo.LynxAssemblyDevices_AssemblyDeviceReferenceId");

            entity.HasOne(d => d.Assembly).WithMany(p => p.LynxAssemblyScheduleActionBlocks).HasConstraintName("FK_dbo.LynxAssemblyScheduleActionBlocks_dbo.LynxAssemblies_AssemblyId");
        });

        modelBuilder.Entity<LynxChirpFrequencyRange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxChirpFrequencyRanges");
        });

        modelBuilder.Entity<LynxConfigurationDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxConfigurationDocuments");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDevices");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirmwareVersion).IsFixedLength();
            entity.Property(e => e.LynxUID).IsFixedLength();

            entity.HasOne(d => d.Type).WithMany(p => p.LynxDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDevices_dbo.LynxDeviceTypes_TypeId");
        });

        modelBuilder.Entity<LynxDeviceDataReading>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceDataReadings");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LynxDeviceDataRecovery).WithMany(p => p.LynxDeviceDataReadings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceDataReadings_dbo.LynxDeviceDataRecoveries_LynxDeviceDataRecoveryId");
        });

        modelBuilder.Entity<LynxDeviceDataRecovery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceDataRecoveries");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AssemblyDevice).WithMany(p => p.LynxDeviceDataRecoveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceDataRecoveries_dbo.LynxAssemblyDevices_AssemblyDeviceId");

            entity.HasOne(d => d.LynxDeviceDataRecoveryType).WithMany(p => p.LynxDeviceDataRecoveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceDataRecoveries_dbo.LynxDeviceDataRecoveryTypes_LynxDeviceDataRecoveryTypeId");

            entity.HasOne(d => d.OperationVersion).WithMany(p => p.LynxDeviceDataRecoveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceDataRecoveries_dbo.LynxOperationVersions_OperationVersionId");
        });

        modelBuilder.Entity<LynxDeviceDataRecoveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceDataRecoveryTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxDeviceGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceGroups");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxDeviceGroupDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceGroupDevices");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.LynxDeviceGroupDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceGroupDevices_dbo.LynxDeviceTypes_DeviceTypeId");

            entity.HasOne(d => d.Group).WithMany(p => p.LynxDeviceGroupDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxDeviceGroupDevices_dbo.LynxDeviceGroups_GroupId");
        });

        modelBuilder.Entity<LynxDeviceType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxDeviceTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxFrequencyBand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxFrequencyBands");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxLinks");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.FirstAssembly).WithMany(p => p.LynxLinkFirstAssemblies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxLinks_dbo.LynxAssemblies_FirstAssemblyId");

            entity.HasOne(d => d.LynxLrcmTransmissionSpeed).WithMany(p => p.LynxLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxLinks_dbo.LynxLrcmTransmissionSpeeds_LynxLrcmTransmissionSpeedId");

            entity.HasOne(d => d.LynxOperationVersion).WithMany(p => p.LynxLinks).HasConstraintName("FK_dbo.LynxLinks_dbo.LynxOperationVersions_LynxOperationVersionId");

            entity.HasOne(d => d.SecondAssembly).WithMany(p => p.LynxLinkSecondAssemblies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxLinks_dbo.LynxAssemblies_SecondAssemblyId");
        });

        modelBuilder.Entity<LynxLinkPowerLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxLinkPowerLevels");

            entity.HasOne(d => d.LynxFrequencyBand).WithMany(p => p.LynxLinkPowerLevels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxLinkPowerLevels_dbo.LynxFrequencyBands_LynxFrequencyBandId");
        });

        modelBuilder.Entity<LynxLrcmTransmissionSpeed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxLrcmTransmissionSpeeds");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperations");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.JobConfiguration).WithMany(p => p.LynxOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperations_dbo.ExproJobs_JobConfigurationId");

            entity.HasOne(d => d.OperationType).WithMany(p => p.LynxOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperations_dbo.LynxOperationTypes_OperationTypeId");

            entity.HasOne(d => d.Well).WithMany(p => p.LynxOperations).HasConstraintName("FK_dbo.LynxOperations_dbo.ExproWells_WellId");
        });

        modelBuilder.Entity<LynxOperationTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperationTasks");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LynxDeviceDataRecovery).WithMany(p => p.LynxOperationTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationTasks_dbo.LynxDeviceDataRecoveries_LynxDeviceDataRecoveryId");

            entity.HasOne(d => d.OperationVersion).WithMany(p => p.LynxOperationTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationTasks_dbo.LynxOperationVersions_OperationVersionId");

            entity.HasOne(d => d.TargetDevice).WithMany(p => p.LynxOperationTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationTasks_dbo.LynxAssemblyDevices_TargetDeviceId");

            entity.HasOne(d => d.TaskStatus).WithMany(p => p.LynxOperationTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationTasks_dbo.LynxOperationTaskStatuses_TaskStatusId");

            entity.HasOne(d => d.TaskType).WithMany(p => p.LynxOperationTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationTasks_dbo.LynxOperationTaskTypes_TaskTypeId");
        });

        modelBuilder.Entity<LynxOperationTaskStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperationTaskStatuses");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxOperationTaskType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperationTaskTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxOperationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperationTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxOperationVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxOperationVersions");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.LynxFrequencyBand).WithMany(p => p.LynxOperationVersions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationVersions_dbo.LynxFrequencyBands_LynxFrequencyBandId");

            entity.HasOne(d => d.LynxLrcmTransmissionSpeed).WithMany(p => p.LynxOperationVersions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationVersions_dbo.LynxLrcmTransmissionSpeeds_LynxLrcmTransmissionSpeedId");

            entity.HasOne(d => d.Operation).WithMany(p => p.LynxOperationVersions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxOperationVersions_dbo.LynxOperations_OperationId");
        });

        modelBuilder.Entity<LynxRouteSegment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxRouteSegments");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.OperationVersion).WithMany(p => p.LynxRouteSegments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxRouteSegments_dbo.LynxOperationVersions_OperationVersionId");

            entity.HasOne(d => d.StartAssembly).WithMany(p => p.LynxRouteSegmentStartAssemblies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxRouteSegments_dbo.LynxAssemblies_StartAssemblyId");

            entity.HasOne(d => d.TargetAssembly).WithMany(p => p.LynxRouteSegmentTargetAssemblies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxRouteSegments_dbo.LynxAssemblies_TargetAssemblyId");
        });

        modelBuilder.Entity<LynxStation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxStations");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.OperationVersion).WithMany(p => p.LynxStations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dbo.LynxStations_dbo.LynxOperationVersions_OperationVersionId");
        });

        modelBuilder.Entity<LynxTimingMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxTimingMetrics");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LynxTool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.LynxTools");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<__MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
