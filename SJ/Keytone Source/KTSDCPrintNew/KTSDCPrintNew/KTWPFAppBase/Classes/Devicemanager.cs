using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace KTWPFAppBase.Classes
{
    class Devicemanager
    {
        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
           int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        static ArrayList devices = new ArrayList();

        public static Device[] GetAllDevices()
        {
            string Header = "Devicemanager::GetAllDevices: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                String dName = "".PadRight(100);
                String dVersion = "".PadRight(100);
                devices.Clear();
                for (short i = 0; i < 10; i++)
                {
                    if (capGetDriverDescriptionA(i, ref dName, 100, ref dVersion, 100))
                    {
                        Device d = new Device(i);
                        d.Name = dName.Trim();
                        d.Version = dVersion.Trim();

                        devices.Add(d);
                    }
                }

                return (Device[])devices.ToArray(typeof(Device));
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public static Device GetDevice(int deviceIndex)
        {
            return (Device)devices[deviceIndex];
        }

        public static Device GetDeviceIndex(string deviceName)
        {
            string Header = "Devicemanager::GetDeviceIndex: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                Device d = null;
                if(devices.Count <= 0)
                    GetAllDevices();

                for (int i = 0; i < devices.Count; i++)
                {
                    if (((Device)(devices[i])).Name.Trim('\0').Equals(deviceName))
                    {
                        int idx = ((Device)(devices[i])).Index;
                        if (idx != -1)
                            d = (Device)GetDevice(idx);
                    }
                }
                return d;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public static int showIndex(string deviceName)
        {
            string Header = "Devicemanager::GetDeviceIndex: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                Device d = null;
                int idx=0;
                if (devices.Count <= 0)
                    GetAllDevices();
                for (int i = 0; i < devices.Count; i++)
                {
                    if (((Device)(devices[i])).Name.Trim('\0').Equals(deviceName))
                    {
                        idx = ((Device)(devices[i])).Index;
                        if (idx != -1)
                            d = (Device)GetDevice(idx);
                    }
                }
                return idx;
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
                throw ex;
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public static void ClearList()
        {
            devices.Clear();
        }
    }
}
