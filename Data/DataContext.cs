using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBShared.Types;
using SBShared.Types.Data;
using System;

namespace SBCore.Configurator.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<AdminData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ParkingData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AisLogData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<AisLogData>().HasIndex(p => p.OperationNumber);
            modelBuilder.Entity<AisLogData>().HasIndex(p => p.Card);
            modelBuilder.Entity<AisLogData>().HasIndex(p => p.SessionId);
            modelBuilder.Entity<AisLogData>().HasIndex(p => p.StartTime);
            modelBuilder.Entity<AisReplacementData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuAisSessionDeliveryStatusData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuIssArchiveImageData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConfigData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConfigData>().Property(p => p.Parameter).HasConversion(v => ConfigParameterConverter(v), v => ConfigParameterConverter(v));
            modelBuilder.Entity<CmiuDeviceData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuCashStatusData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuEventData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuImageData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuLevelsData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuLprData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuMovementData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuParkingLevelData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuPaymentData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuPlacesData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuPlacesUrlData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CmiuPlacesUrlData>().Property(p => p.Url).HasConversion(new UriToStringConverter()!);
            modelBuilder.Entity<OAJobData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MetroExemptionData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MetroParkingData>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OAProviderData>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<CameraData<CmiuDeviceData>>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CameraData<CmiuDeviceData>>().Property(p => p.Cameratype).HasConversion<string>();
            modelBuilder.Entity<CameraData<CmiuDeviceData>>().Property(p => p.CameraUri).HasConversion(new UriToStringConverter()!);
            modelBuilder.Entity<CameraData<CmiuDeviceData>>().Property(p => p.Delay).HasConversion<string>();
            modelBuilder.Entity<CameraData<CmiuDeviceData>>().Property(p => p.Timeout).HasConversion<string>();

            modelBuilder.Entity<DeviceCmiuCameraData<CmiuDeviceData>>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DeviceCmiuCameraData<CmiuDeviceData>>().HasOne(p => p.Camera).WithMany(p => p.DeviceCmiuCameras).IsRequired(false).HasForeignKey(p => p.CameraId);
            modelBuilder.Entity<DeviceCmiuCameraData<CmiuDeviceData>>().HasOne(p => p.Device).WithMany(p => p.DeviceCmiuCameras).IsRequired(true).HasForeignKey(p => p.DeviceId);

            modelBuilder.Entity<EventCameraData<CmiuDeviceData>>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<EventCameraData<CmiuDeviceData>>().HasOne(p => p.DeviceCmiuCamera).WithMany(p => p.EventCameras).HasForeignKey(p => p.DeviceCmiuCameraId);

            modelBuilder.Entity<LprIssEventData<CmiuDeviceData>>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LprIssEventData<CmiuDeviceData>>().HasOne(p => p.DeviceCmiuCamera).WithMany(p => p.LprIssEvents).HasForeignKey(p => p.DeviceCmiuCameraId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AdminData> AdminDataSet { get; set; }
        public DbSet<ParkingData> ParkingDataSet { get; set; }
        public DbSet<AisLogData> AisLogDataSet { get; set; }
        public DbSet<AisReplacementData> AisReplacementDataSet { get; set; }
        public DbSet<CmiuAisSessionDeliveryStatusData> CmiuAisSessionDeliveryStatusDataSet { get; set; }
        public DbSet<CmiuIssArchiveImageData> CmiuIssArchiveImageDataSet { get; set; }
        public DbSet<ConfigData> ConfigDataSet { get; set; }
        public DbSet<CmiuDeviceData> CmiuDeviceDataSet { get; set; }
        public DbSet<CmiuCashStatusData> CmiuCashStatusDataSet { get; set; }
        public DbSet<CmiuEventData> CmiuEventDataSet { get; set; }
        public DbSet<CmiuImageData> CmiuImageDataSet { get; set; }
        public DbSet<CmiuLevelsData> CmiuLevelsDataSet { get; set; }
        public DbSet<CmiuLprData> CmiuLprDataSet { get; set; }
        public DbSet<CmiuMovementData> CmiuMovementDataSet { get; set; }
        public DbSet<CmiuParkingLevelData> CmiuParkingLevelDataSet { get; set; }
        public DbSet<CmiuPaymentData> CmiuPaymentDataSet { get; set; }
        public DbSet<CmiuPlacesData> CmiuPlacesDataSet { get; set; }
        public DbSet<CmiuPlacesUrlData> CmiuPlacesUrlDataSet { get; set; }
        public DbSet<OAJobData> OAJobDataSet { get; set; }
        public DbSet<MetroExemptionData> MetroExemptionDataSet { get; set; }
        public DbSet<MetroParkingData> MetroParkingDataSet { get; set; }
        public DbSet<OAProviderData> OAProviderDataSet { get; set; }

        public DbSet<CameraData<CmiuDeviceData>> CameraDataSet { get; set; }
        public DbSet<DeviceCmiuCameraData<CmiuDeviceData>> DeviceCmiuCameraDataSet { get; set; }
        public DbSet<EventCameraData<CmiuDeviceData>> EventCameraDataSet { get; set; }
        public DbSet<LprIssEventData<CmiuDeviceData>> LprIssEventDataSet { get; set; }

        private static ConfigParameter ConfigParameterConverter(string value)
        {
            if (value is null)
                return null;
            if (ConfigParameter.TryFromValue(value, out var result))
                return result;
            return null;
        }

        private static string ConfigParameterConverter(ConfigParameter value)
        {
            if (value is null)
                return null;
            return value.Value;
        }
    }
}