
using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Management.Instrumentation;
using Microsoft.Win32;
using ROOT.CIMV2.Win32;
using System.Net;



namespace KTone.RFIDGlobal.SystemInfo
{
    public class BiosInfo
    {
        public String SerialNumber;
        public String Manufacturer;
        public String IdentificationCode;
        public String Version;
        public DateTime InstallDate;

        public string ToString(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            if (getAllDetails)
            {
                sb.Append("\tSerialNumber : ");
                sb.Append(SerialNumber);
                sb.Append("\r\n");
                sb.Append("\tManufacturer : ");
                sb.Append(Manufacturer);
                sb.Append("\r\n");
                sb.Append("\tIdentificationCode : ");
                sb.Append(IdentificationCode);
                sb.Append("\r\n");
                sb.Append("\tVersion : ");
                sb.Append(Version);
                sb.Append("\r\n");
                sb.Append("\tInstallDate : ");
                sb.Append(InstallDate.ToString());
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class BusInfo
    {
        public UInt32 BusNum;
        public UInt32 BusType;
        public String DeviceId;

        public string ToString(bool getAllDetails, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", index)); 
            sb.Append("\tBusNum : ");
            sb.Append(BusNum);
            sb.Append("\r\n");
            sb.Append("\tBusType : ");
            sb.Append(BusType);
            sb.Append("\r\n");
            sb.Append("\tDeviceId : ");
            sb.Append(DeviceId);
            sb.Append("\r\n");
            sb.Append("\r\n");
            return sb.ToString();
        }
    }

    public class DiskDriveInfo
    {
        public String Manufacturer;
        public String Model;
        public String Name;
        public String InterfaceName;
        public String InterfaceType;
        public String MediaType;
        public UInt32 BytesPerSector;
        public UInt64 TotalCylinders;
        public UInt32 TotalHeads;
        public UInt64 TotalSectors;
        public UInt64 TotalTracks;
        public UInt32 Partitions;

        public string ToString(bool getAllDetails, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", index)); 
            sb.Append("\tManufacturer : ");
            sb.Append(Manufacturer);
            sb.Append("\r\n");
            sb.Append("\tModel : ");
            sb.Append(Model);
            sb.Append("\r\n");
            sb.Append("\tName : ");
            sb.Append(Name);
            sb.Append("\r\n");
            sb.Append("\tInterfaceName : ");
            sb.Append(InterfaceName);
            sb.Append("\r\n");
            sb.Append("\tInterfaceType : ");
            sb.Append(InterfaceType);
            sb.Append("\r\n");
            sb.Append("\tMediaType : ");
            sb.Append(MediaType);
            sb.Append("\r\n");
            if (getAllDetails)
            {
                sb.Append("\tBytesPerSector : ");
                sb.Append(BytesPerSector);
                sb.Append("\r\n");
                sb.Append("\tTotalCylinders : ");
                sb.Append(TotalCylinders);
                sb.Append("\r\n");
                sb.Append("\tTotalHeads : ");
                sb.Append(TotalHeads);
                sb.Append("\r\n");
                sb.Append("\tTotalSectors : ");
                sb.Append(TotalSectors);
                sb.Append("\r\n");
                sb.Append("\tTotalTracks : ");
                sb.Append(TotalTracks);
                sb.Append("\r\n");
                sb.Append("\tPartitions : ");
                sb.Append(Partitions);
                sb.Append("\r\n");
            }
            sb.Append("\r\n"); 
            return sb.ToString();
        }
    }

    public class DiskPartitionInfo
    {
        public String DeviceId;
        public UInt64 BlockSize;
        public Boolean Bootable;
        public Boolean BootPartition;
        public UInt32 DiskIndex;
        public UInt64 NumberOfBlocks;
        public Boolean PrimaryPartition;
        public UInt64 Size;
        public String Type;

        public string ToString(bool getAllDetails, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", index)); 
            sb.Append("\tDeviceId : ");
            sb.Append(DeviceId);
            sb.Append("\r\n");
            sb.Append("\tBlockSize : ");
            sb.Append(BlockSize);
            sb.Append("\r\n");
            sb.Append("\tBootable : ");
            sb.Append(Bootable);
            sb.Append("\r\n");
            sb.Append("\tBootPartition : ");
            sb.Append(BootPartition);
            sb.Append("\r\n");
            sb.Append("\tDiskIndex : ");
            sb.Append(DiskIndex);
            sb.Append("\r\n");
            sb.Append("\tNumberOfBlocks : ");
            sb.Append(NumberOfBlocks);
            sb.Append("\r\n");
            sb.Append("\tPrimaryPartition : ");
            sb.Append(PrimaryPartition);
            sb.Append("\r\n");
            sb.Append("\tSize : ");
            sb.Append(Size);
            sb.Append("\r\n");
            sb.Append("\tType : ");
            sb.Append(Type);
            sb.Append("\r\n");
            sb.Append("\r\n");
            return sb.ToString();
        }
    }

    public class LogicalDiskInfo
    {
        public UInt32 BlockSize;
        public String DeviceId;
        public UInt32 DriveType;
        public String FileSystem;
        public UInt64 FreeSpace;
        public UInt64 NumberOfBlocks;
        public UInt64 Size;
        public String VolumeSerialNumber;

        public string ToString(bool getAllDetails, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", index));
            if (getAllDetails)
            {
                sb.Append("\tBlockSize : ");
                sb.Append(BlockSize);
                sb.Append("\r\n");
            }
            sb.Append("\tDeviceId : ");
            sb.Append(DeviceId);
            sb.Append("\r\n");
            sb.Append("\tDriveType : ");
            sb.Append(DriveType);
            sb.Append("\r\n");
            sb.Append("\tFileSystem : ");
            sb.Append(FileSystem);
            sb.Append("\r\n");
            sb.Append("\tFreeSpace : ");
            sb.Append(FreeSpace);
            sb.Append("\r\n");
            if (getAllDetails)
            {
                sb.Append("\tNumberOfBlocks : ");
                sb.Append(NumberOfBlocks);
                sb.Append("\r\n");
            }
            sb.Append("\tSize : ");
            sb.Append(Size);
            sb.Append("\r\n");
            if (getAllDetails)
            {
                sb.Append("\tVolumeSerialNumber : ");
                sb.Append(VolumeSerialNumber);
                sb.Append("\r\n");
            }
            sb.Append("\r\n");
            return sb.ToString();
        }
    }

    public class PhysicalMemoryInfo
    {
        public String BankLabel;
        public UInt64 Capacity;
        public UInt16 DataWidth;
        public UInt16 TotalWidth;
        public String Manufacturer;
        public String Model;
        public UInt16 MemoryType;
        public UInt32 Speed;

        public string ToString(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            if (getAllDetails)
            {
                sb.Append("\tBankLabel : ");
                sb.Append(BankLabel);
                sb.Append("\r\n");
            }
            sb.Append("\tCapacity : ");
            sb.Append(Capacity);
            sb.Append("\r\n");
            sb.Append("\tDataWidth : ");
            sb.Append(DataWidth);
            sb.Append("\r\n");
            sb.Append("\tTotalWidth : ");
            sb.Append(TotalWidth);
            sb.Append("\r\n");
            if (getAllDetails)
            {
                sb.Append("\tManufacturer : ");
                sb.Append(Manufacturer);
                sb.Append("\r\n");
                sb.Append("\tModel : ");
                sb.Append(Model);
                sb.Append("\r\n");
            }
            sb.Append("\tMemoryType : ");
            sb.Append(MemoryType);
            sb.Append("\r\n");
            sb.Append("\tSpeed : ");
            sb.Append(Speed);
            sb.Append("\r\n");
            return sb.ToString();
        }
    }
        
    public class VideoInfo
    {
        public String DeviceId;
        public UInt32 AdapterRAM;
        public String VideoModeDescription;
        public String VideoProcessor;
        public UInt32 CurrentRefreshRate;
        public String AdapterDACType;

        public string ToString(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\tDeviceId : ");
            sb.Append(DeviceId);
            sb.Append("\r\n");
            sb.Append("\tAdapterRAM : ");
            sb.Append(AdapterRAM);
            sb.Append("\r\n");
            sb.Append("\tVideoModeDescription : ");
            sb.Append(VideoModeDescription);
            sb.Append("\r\n");
            sb.Append("\tVideoProcessor : ");
            sb.Append(VideoProcessor);
            sb.Append("\r\n");
            sb.Append("\tCurrentRefreshRate : ");
            sb.Append(CurrentRefreshRate);
            sb.Append("\r\n");
            sb.Append("\tAdapterDACType : ");
            sb.Append(AdapterDACType);
            sb.Append("\r\n");
            return sb.ToString();
        }
    }

    public class NetworkInfo
    { 
        public string DNSHostName;
        public string Description;
        public string[] IPAddresses;
        public string[] IPSubnets;
        public string[] DefaultIPGateways;

        public string ToString(bool getAllDetails, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", index)); 
            sb.Append("\tDNSHostName : ");
            sb.Append(DNSHostName);
            sb.Append("\r\n");
            sb.Append("\tDescription : ");
            sb.Append(Description);
            sb.Append("\r\n");
            sb.Append("\tIPAddresses: ");
            if (this.IPAddresses == null)
                sb.Append("\t--");
            else
            {
                int indexIP = 1;
                foreach (string IPAddress in IPAddresses)
                {
                    sb.Append("\r\n");
                    sb.Append(string.Format("\t[{0}] ", indexIP++));
                    sb.Append(IPAddress);
                }
            }
            sb.Append("\r\n");

            sb.Append("\tIPSubnets : ");
            if (this.IPSubnets == null)
                sb.Append("\t--");
            else
            {
                int indexIPSubnet = 1;
                foreach (string IPSubnet in IPSubnets)
                {
                    sb.Append("\r\n");
                    sb.Append(string.Format("\t[{0}] ", indexIPSubnet++));
                    sb.Append(IPSubnet);
                }
            }
            sb.Append("\r\n");


            sb.Append("\tDefaultIPGateways : ");
            if (this.DefaultIPGateways == null)
                sb.Append("\t--");
            else
            {
                int indexDefaultIPGateway = 1;
                foreach (string DefaultIPGateway in DefaultIPGateways)
                {
                    sb.Append("\r\n");
                    sb.Append(string.Format("\t[{0}] ", indexDefaultIPGateway++));
                    sb.Append(DefaultIPGateway);
                }
            }
            sb.Append("\r\n");
            sb.Append("\r\n");
            return sb.ToString();
        }
    }

    public class WindowsSysInfo
    {
        #region Members

        private ConnectionOptions connectionOptions;
        private ManagementScope managementScope;

        #endregion Members

        #region Constructor

        public WindowsSysInfo() 
        {
            this.connectionOptions = new ConnectionOptions();
            this.managementScope = new ManagementScope(@"\\localhost", this.connectionOptions);
        }

        #endregion Constructor

        #region Public Methods

        public string GetBiosInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Bios Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    BIOS currentBios = new BIOS(mgmtObj);
                    
                    if (getAllDetails)
                    {
                        sb.Append("\tSerialNumber : ");
                        sb.Append(currentBios.SerialNumber);
                        sb.Append("\r\n");
                        sb.Append("\tManufacturer : ");
                        sb.Append(currentBios.Manufacturer);
                        sb.Append("\r\n");
                        sb.Append("\tIdentificationCode : ");
                        sb.Append(currentBios.IdentificationCode);
                        sb.Append("\r\n");
                        sb.Append("\tVersion : ");
                        sb.Append(currentBios.Version);
                        sb.Append("\r\n");
                        sb.Append("\tInstallDate : ");
                        sb.Append(currentBios.InstallDate.ToString());
                        sb.Append("\r\n");
                    }
                    
                }
                return sb.ToString();
            }
            catch 
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetProcessorInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Processor");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Processor Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Processor count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    Processor processor = new Processor(mgmtObj);
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tDeviceId : ");
                    sb.Append(processor.DeviceID);
                    sb.Append("\r\n");
                    sb.Append("\tManufacturer : ");
                    sb.Append(processor.Manufacturer);
                    sb.Append("\r\n");
                    sb.Append("\tProcessorType : ");
                    sb.Append(processor.ProcessorType);
                    sb.Append("\r\n");
                    sb.Append("\tFamily : ");
                    sb.Append(processor.Family);
                    sb.Append("\r\n");

                    if (getAllDetails)
                    {
                        sb.Append("\tArchitecture : ");
                        sb.Append(processor.Architecture);
                        sb.Append("\r\n");
                        sb.Append("\tAddressWidth : ");
                        sb.Append(processor.AddressWidth);
                        sb.Append("\r\n");
                        sb.Append("\tDataWidth : ");
                        sb.Append(processor.DataWidth);
                        sb.Append("\r\n");
                        sb.Append("\tCurrentClockSpeed : ");
                        sb.Append(processor.CurrentClockSpeed);
                        sb.Append("\r\n");
                        sb.Append("\tMaxClockSpeed : ");
                        sb.Append(processor.MaxClockSpeed);
                        sb.Append("\r\n");
                        sb.Append("\tCurrentVoltage : ");
                        sb.Append(processor.CurrentVoltage);
                        sb.Append("\r\n");
                    }
                    sb.Append("\r\n");
                }

                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetCacheInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_CacheMemory");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Cache Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Cache count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    CacheMemory currentCache = new CacheMemory(mgmtObj);
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tAccess : ");
                    sb.Append(currentCache.Access);
                    sb.Append("\r\n");
                    sb.Append("\tLevel : ");
                    sb.Append(currentCache.Level);
                    sb.Append("\r\n");
                    sb.Append("\tBlockSize : ");
                    sb.Append(currentCache.BlockSize);
                    sb.Append("\r\n");
                    sb.Append("\tCacheSpeed : ");
                    sb.Append(currentCache.CacheSpeed);
                    sb.Append("\r\n");
                    sb.Append("\tCacheType : ");
                    sb.Append(currentCache.CacheType);
                    sb.Append("\r\n");
                    sb.Append("\tMaxCacheSize : ");
                    sb.Append(currentCache.MaxCacheSize);
                    sb.Append("\r\n");
                    sb.Append("\r\n");
                }

                return sb.ToString();
            }
            catch 
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetBusInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Bus");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Bus Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Bus count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;

                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    Bus currentBus = new Bus(mgmtObj);
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tBusNum : ");
                        sb.Append(currentBus.BusNum);
                    sb.Append("\r\n");

                    sb.Append("\tBusType : ");
                        sb.Append(currentBus.BusType);
                    sb.Append("\r\n");
                    sb.Append("\tDeviceId : ");
                        sb.Append(currentBus.DeviceID);
                    sb.Append("\r\n");
                    sb.Append("\r\n");
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }

        }

        public string GetDiskDriveInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("DiskDrive Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Disk count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;

                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    DiskDrive currentDisk = new DiskDrive(mgmtObj);
                    
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tManufacturer : ");
                    sb.Append(currentDisk.Manufacturer);
                    sb.Append("\r\n");
                    sb.Append("\tModel : ");
                    sb.Append(currentDisk.Model);
                    sb.Append("\r\n");
                    sb.Append("\tName : ");
                    sb.Append(currentDisk.Name);
                    sb.Append("\r\n");
                    //sb.Append("\tInterfaceName : ");
                    //sb.Append(currentDisk.InterfaceName);
                    //sb.Append("\r\n");
                    sb.Append("\tInterfaceType : ");
                    sb.Append(currentDisk.InterfaceType);
                    sb.Append("\r\n");
                    sb.Append("\tMediaType : ");
                    sb.Append(currentDisk.MediaType);
                    sb.Append("\r\n");
                    if (getAllDetails)
                    {
                        sb.Append("\tBytesPerSector : ");
                        sb.Append(currentDisk.BytesPerSector);
                        sb.Append("\r\n");
                        sb.Append("\tTotalCylinders : ");
                        sb.Append(currentDisk.TotalCylinders);
                        sb.Append("\r\n");
                        sb.Append("\tTotalHeads : ");
                        sb.Append(currentDisk.TotalHeads);
                        sb.Append("\r\n");
                        sb.Append("\tTotalSectors : ");
                        sb.Append(currentDisk.TotalSectors);
                        sb.Append("\r\n");
                        sb.Append("\tTotalTracks : ");
                        sb.Append(currentDisk.TotalTracks);
                        sb.Append("\r\n");
                        sb.Append("\tPartitions : ");
                        sb.Append(currentDisk.Partitions);
                        sb.Append("\r\n");
                    }
                    sb.Append("\r\n");
                    
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetLogicalDiskInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_LogicalDisk");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Logical Disk Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Logical Disk count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;

                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    LogicalDisk currentDisk = new LogicalDisk(mgmtObj);
                    
                    sb.Append(string.Format("[{0}]", index++));
                    if (getAllDetails)
                    {
                        sb.Append("\tBlockSize : ");
                        sb.Append(currentDisk.BlockSize);
                        sb.Append("\r\n");
                    }
                    sb.Append("\tDeviceId : ");
                    sb.Append(currentDisk.DeviceID);
                    sb.Append("\r\n");
                    sb.Append("\tDriveType : ");
                    sb.Append(currentDisk.DriveType);
                    sb.Append("\r\n");
                    sb.Append("\tFileSystem : ");
                    sb.Append(currentDisk.FileSystem);
                    sb.Append("\r\n");
                    sb.Append("\tFreeSpace : ");
                    sb.Append(currentDisk.FreeSpace);
                    sb.Append("\r\n");
                    if (getAllDetails)
                    {
                        sb.Append("\tNumberOfBlocks : ");
                        sb.Append(currentDisk.NumberOfBlocks);
                        sb.Append("\r\n");
                    }
                    sb.Append("\tSize : ");
                    sb.Append(currentDisk.Size);
                    sb.Append("\r\n");
                    if (getAllDetails)
                    {
                        sb.Append("\tVolumeSerialNumber : ");
                        sb.Append(currentDisk.VolumeSerialNumber);
                        sb.Append("\r\n");
                    }
                    sb.Append("\r\n");
                   
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetDiskPartitionInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskPartition");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Disk Partition Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Disk Partition count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;

                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    DiskPartitionInfo current_partition = new DiskPartitionInfo();
                    
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tDeviceId : ");
                    sb.Append(current_partition.DeviceId);
                    sb.Append("\r\n");
                    sb.Append("\tBlockSize : ");
                    sb.Append(current_partition.BlockSize);
                    sb.Append("\r\n");
                    sb.Append("\tBootable : ");
                    sb.Append(current_partition.Bootable);
                    sb.Append("\r\n");
                    sb.Append("\tBootPartition : ");
                    sb.Append(current_partition.BootPartition);
                    sb.Append("\r\n");
                    sb.Append("\tDiskIndex : ");
                    sb.Append(current_partition.DiskIndex);
                    sb.Append("\r\n");
                    sb.Append("\tNumberOfBlocks : ");
                    sb.Append(current_partition.NumberOfBlocks);
                    sb.Append("\r\n");
                    sb.Append("\tPrimaryPartition : ");
                    sb.Append(current_partition.PrimaryPartition);
                    sb.Append("\r\n");
                    sb.Append("\tSize : ");
                    sb.Append(current_partition.Size);
                    sb.Append("\r\n");
                    sb.Append("\tType : ");
                    sb.Append(current_partition.Type);
                    sb.Append("\r\n");
                    sb.Append("\r\n");
                }
                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetVideoInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_VideoController");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();


                sb.Append("Video Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    VideoController currentVideodev = new VideoController(mgmtObj);
                    sb.Append("\tDeviceId : ");
                    sb.Append(currentVideodev.DeviceID);
                    sb.Append("\r\n");
                    sb.Append("\tAdapterRAM : ");
                    sb.Append(currentVideodev.AdapterRAM);
                    sb.Append("\r\n");
                    sb.Append("\tVideoModeDescription : ");
                    sb.Append(currentVideodev.VideoModeDescription);
                    sb.Append("\r\n");
                    sb.Append("\tVideoProcessor : ");
                    sb.Append(currentVideodev.VideoProcessor);
                    sb.Append("\r\n");
                    sb.Append("\tCurrentRefreshRate : ");
                    sb.Append(currentVideodev.CurrentRefreshRate);
                    sb.Append("\r\n");
                    sb.Append("\tAdapterDACType : ");
                    sb.Append(currentVideodev.AdapterDACType);
                    sb.Append("\r\n");
                    
                }
                            
                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetPhysicalMemoryInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("Physical Memory Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Physical Memory count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    PhysicalMemory currentMemory = new PhysicalMemory(mgmtObj);
                    
                    if (getAllDetails)
                    {
                        sb.Append("\tBankLabel : ");
                        sb.Append(currentMemory.BankLabel);
                        sb.Append("\r\n");
                    }
                    sb.Append("\tCapacity : ");
                    sb.Append(currentMemory.Capacity);
                    sb.Append("\r\n");
                    sb.Append("\tDataWidth : ");
                    sb.Append(currentMemory.DataWidth);
                    sb.Append("\r\n");
                    sb.Append("\tTotalWidth : ");
                    sb.Append(currentMemory.TotalWidth);
                    sb.Append("\r\n");
                    if (getAllDetails)
                    {
                        sb.Append("\tManufacturer : ");
                        sb.Append(currentMemory.Manufacturer);
                        sb.Append("\r\n");
                        sb.Append("\tModel : ");
                        sb.Append(currentMemory.Model);
                        sb.Append("\r\n");
                    }
                    sb.Append("\tMemoryType : ");
                    sb.Append(currentMemory.MemoryType);
                    sb.Append("\r\n");
                    sb.Append("\tSpeed : ");
                    sb.Append(currentMemory.Speed);
                    sb.Append("\r\n");
                   
                }

                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetNetworkInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string query = "SELECT * FROM Win32_NetworkAdapterConfiguration"
                                + " WHERE IPEnabled = 'TRUE'";
                ManagementObjectSearcher moSearch = new ManagementObjectSearcher(query);
                ManagementObjectCollection mgmtObjCollection = moSearch.Get();

                sb.Append("Network Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("Network  count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;
                // Every record in this collection is a network interface
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    NetworkAdapterConfiguration currentNetwork = new NetworkAdapterConfiguration(mgmtObj);

                   
                    sb.Append(string.Format("[{0}]", index));
                    sb.Append("\tDNSHostName : ");
                    sb.Append(currentNetwork.DNSHostName);
                    sb.Append("\r\n");
                    sb.Append("\tDescription : ");
                    sb.Append(currentNetwork.Description);
                    sb.Append("\r\n");

                    sb.Append("\tDHCPEnabled : ");
                    sb.Append(currentNetwork.DHCPEnabled);
                    sb.Append("\r\n");

                    sb.Append("\tDHCPServer : ");
                    sb.Append(currentNetwork.DHCPServer);
                    sb.Append("\r\n");

                    sb.Append("\tDNSDomain : ");
                    sb.Append(currentNetwork.DNSDomain);
                    sb.Append("\r\n");

                    sb.Append("\tMACAddress : ");
                    sb.Append(currentNetwork.MACAddress);
                    sb.Append("\r\n");

                    sb.Append("\tIPAddresses: ");
                    if (currentNetwork.IPAddress == null)
                        sb.Append("\t--");
                    else
                    {
                        int indexIP = 1;
                        foreach (string IPAddress in currentNetwork.IPAddress)
                        {
                            sb.Append("\r\n");
                            sb.Append(string.Format("\t[{0}] ", indexIP++));
                            sb.Append(IPAddress);
                        }
                    }
                    sb.Append("\r\n");

                    sb.Append("\tIPSubnets : ");
                    if (currentNetwork.IPSubnet == null)
                        sb.Append("\t--");
                    else
                    {
                        int indexIPSubnet = 1;
                        foreach (string IPSubnet in currentNetwork.IPSubnet)
                        {
                            sb.Append("\r\n");
                            sb.Append(string.Format("\t[{0}] ", indexIPSubnet++));
                            sb.Append(IPSubnet);
                        }
                    }
                    sb.Append("\r\n");


                    sb.Append("\tDefaultIPGateways : ");
                    if (currentNetwork.DefaultIPGateway == null)
                        sb.Append("\t--");
                    else
                    {
                        int indexDefaultIPGateway = 1;
                        foreach (string DefaultIPGateway in currentNetwork.DefaultIPGateway)
                        {
                            sb.Append("\r\n");
                            sb.Append(string.Format("\t[{0}] ", indexDefaultIPGateway++));
                            sb.Append(DefaultIPGateway);
                        }
                    }
                    sb.Append("\r\n");
                    sb.Append("\r\n");
                   
                }

                try 
                {
                    string hostName = Dns.GetHostName();
                    IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);
                    if (ipAddresses != null)
                    {
                        sb.Append("Address Family:");
                        sb.Append("\r\n");
                        foreach (IPAddress IPA in ipAddresses)
                        {
                            sb.Append(IPA.ToString() + ":" + IPA.AddressFamily.ToString());
                            sb.Append("\r\n");
                        }
                        sb.Append("\r\n");
                    }
                }
                catch { }
                return sb.ToString();
            }
            catch 
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        public string GetSystemInfo()
        {
            return GetSystemInfo(true);
        }

        public string GetSystemInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("System Info : localhost");
            sb.Append("\r\n");
            if (getAllDetails)
                sb.Append(GetBiosInfo(getAllDetails));

            sb.Append(GetProcessorInfo(getAllDetails));
            sb.Append(GetCacheInfo(getAllDetails));
            if (getAllDetails)
                sb.Append(GetBusInfo(getAllDetails));
            sb.Append(GetDiskDriveInfo(getAllDetails));
            sb.Append(GetLogicalDiskInfo(getAllDetails));
            if (getAllDetails)
                sb.Append(GetDiskPartitionInfo(getAllDetails));
            if (getAllDetails)
                sb.Append(GetVideoInfo(getAllDetails));
            sb.Append(GetPhysicalMemoryInfo(getAllDetails));
            sb.Append(GetNetworkInfo(getAllDetails));
            sb.Append(GetSerialPortInfo(getAllDetails));
           
            return sb.ToString();
            /*
             # Win32_SerialPort

    * uint16 Availability;
    * string DeviceID;
    * uint32 MaxBaudRate;
    * string Name;
    * uint16 ProtocolSupported;
    * string ProviderType;
    * string Status;

*/
        }

        public string GetSerialPortInfo(bool getAllDetails)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_SerialPort");
                ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(this.managementScope, query);
                ManagementObjectCollection mgmtObjCollection = mgmtObjSearcher.Get();

                sb.Append("SerialPort Info : ");
                if (mgmtObjCollection.Count == 0)
                {
                    sb.Append("--");
                    return sb.ToString();
                }
                sb.Append("\r\n");
                sb.Append("SerialPort count : " + mgmtObjCollection.Count);
                sb.Append("\r\n");
                int index = 1;
                foreach (ManagementObject mgmtObj in mgmtObjCollection)
                {
                    SerialPort serialPort = new SerialPort(mgmtObj);
                    sb.Append(string.Format("[{0}]", index++));
                    sb.Append("\tDeviceId : ");
                    sb.Append(serialPort.DeviceID);
                    sb.Append("\r\n");
                    sb.Append("\tName : ");
                    sb.Append(serialPort.Name);
                    sb.Append("\r\n");
                    sb.Append("\tMax BaudRate : ");
                    sb.Append(serialPort.MaxBaudRate);
                    sb.Append("\r\n");
                    sb.Append("\tMaximumInputBufferSize : ");
                    sb.Append(serialPort.MaximumInputBufferSize);
                    sb.Append("\tMaximumOutputBufferSize : ");
                    sb.Append(serialPort.MaximumOutputBufferSize);
                    sb.Append("\r\n");

                    if (getAllDetails)
                    {
                        sb.Append("\tSupports16BitMode : ");
                        sb.Append(serialPort.Supports16BitMode);
                        sb.Append("\r\n");
                        sb.Append("\tProtocolSupported : ");
                        sb.Append(serialPort.ProtocolSupported);
                        sb.Append("\r\n");
                        sb.Append("\tSettableBaudRate: ");
                        sb.Append(serialPort.SettableBaudRate);
                        sb.Append("\r\n");
                        sb.Append("\tDescription : ");
                        sb.Append(serialPort.Description);
                        sb.Append("\r\n");
                        sb.Append("\tPowerManagementSupported: ");
                        sb.Append(serialPort.PowerManagementSupported);
                        sb.Append("\r\n");
                        sb.Append("\tAvailability : ");
                        sb.Append(serialPort.Availability);
                        sb.Append("\r\n");
                    }
                    sb.Append("\r\n");
                }

                return sb.ToString();
            }
            catch
            {
                sb.Append("\r\n");
                return sb.ToString();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void SetValue(object source, ref String dest) {
            if (source == null || ((String)source).Trim().Equals(""))
            {
                dest = null;
            }
            else
            {
                dest = (String)source;
            }
        }
        private void SetValue(object source, ref Boolean dest)
        {
            if (source == null)
            {
                dest = false;
            }
            else
            {
                dest = (Boolean)source;
            }
        }
        private void SetValue(object source, ref UInt16 dest)
        {
            if (source == null)
            {
                dest = 0;
            }
            else
            {
                dest = (UInt16)source;
            }
        }

        private void SetValue(object source, ref UInt32 dest)
        {
            if (source == null)
            {
                dest = 0;
            }
            else
            {
                dest = (UInt32)source;
            }
        }

        private void SetValue(object source, ref UInt64 dest)
        {
            if (source == null)
            {
                dest = 0;
            }
            else
            {
                dest = (UInt64)source;
            }
        }

        private void SetValue(object source, ref DateTime dest)
        {
            if (source == null)
            {
                dest = new DateTime(1753, 1, 1);
            }
            else
            {
                dest = (DateTime)source;
            }
        }

        #endregion Private Methods
    }

    public class ProcessorsInfo
    {
        public static void GetCoreCount(out int physicalProc, out int logicalProc) 
        {
            physicalProc = 1;
            logicalProc = 1;
            ManagementObject mo = new ManagementObject("Win32_ComputerSystem.Name=\"" + Environment.MachineName + "\"");
            try
            {
                physicalProc = Convert.ToInt32(mo["NumberOfProcessors"]);
                logicalProc = Convert.ToInt32(mo["NumberOfLogicalProcessors"]);
            }
            catch
            {
                physicalProc = 1;
                logicalProc = 1;
            }
        }
    }
}