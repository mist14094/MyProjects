using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KTWPFAppBase.Classes
{
    class Device
    {
        private const short WM_CAP = 0x400;

        public const int WM_CAP_GET_FRAME = 1084;
        public const int WM_CAP_COPY = 1054;

        private const int WM_CAP_DRIVER_CONNECT = 0x40a;
        private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = 0x432;
        private const int WM_CAP_SET_OVERLAY = 0x433;
        private const int WM_CAP_SET_PREVIEWRATE = 0x434;
        private const int WM_CAP_SET_SCALE = 0x435;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;

        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        [DllImport("user32", EntryPoint = "OpenClipboard")]
        public static extern int OpenClipboard(int hWnd);

        [DllImport("user32", EntryPoint = "EmptyClipboard")]
        public static extern int EmptyClipboard();

        [DllImport("user32", EntryPoint = "CloseClipboard")]
        public static extern int CloseClipboard();

        int index;
        int deviceHandle;
        private IDataObject tempObj;
        private System.Drawing.Image tempImg;

        public Device(int index)
        {
            this.index = index;
        }

        private string _name;


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Index
        {
            get { return index; }
        }

        private string _version;

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }
        /// <summary>
        /// To Initialize the device
        /// </summary>
        /// <param name="windowHeight">Height of the Window</param>
        /// <param name="windowWidth">Width of the Window</param>
        /// <param name="handle">The Control Handle to attach the device</param>
        public void Init(int windowHeight, int windowWidth, int handle)
        {
            string Header = "Device::Init: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                string deviceIndex = Convert.ToString(this.index);
                deviceHandle = capCreateCaptureWindowA(ref deviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, windowWidth, windowHeight, handle, 0);

                if (SendMessage(deviceHandle, WM_CAP_DRIVER_CONNECT, this.index, 0) > 0)
                {
                    SendMessage(deviceHandle, WM_CAP_SET_SCALE, -1, 0);
                    SendMessage(deviceHandle, WM_CAP_SET_PREVIEWRATE, 0x42, 0);
                    SendMessage(deviceHandle, WM_CAP_SET_PREVIEW, -1, 0);

                    SetWindowPos(deviceHandle, 1, 0, 0, windowWidth, windowHeight, 6);
                }
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        /// <summary>
        /// Shows the webcam preview in the control
        /// </summary>
        /// <param name="windowsControl">Control to attach the webcam preview</param>
        public void ShowWindow(global::System.Windows.Forms.Control windowsControl)
        //public void ShowWindow(global::System.Windows.Controls.Image windowsControl)
        {
            string Header = "Device::ShowWindow: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                Init(windowsControl.Height, windowsControl.Width, windowsControl.Handle.ToInt32());
                //Init(windowsControl.Height, windowsControl.Width, Convert.ToInt32(100));
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        /// <summary>
        /// Stop the webcam and destroy the handle
        /// </summary>
        public void Stop()
        {
            string Header = "Device::Stop: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                SendMessage(deviceHandle, WM_CAP_DRIVER_DISCONNECT, this.index, 0);

                DestroyWindow(deviceHandle);
            }
            catch (Exception ex)
            {
                BaseAppSettings.m_Log.ErrorException(Header + "Error occured." + ex.Message, ex);
            }
            finally
            {
                BaseAppSettings.m_Log.Trace(Header + "Leaving..");
            }
        }

        public System.Drawing.Image GetPicture()
        {
            string Header = "Device::GetPicture: ";
            BaseAppSettings.m_Log.Trace(Header + "Entering.. ");
            try
            {
                // get the next frame;
                SendMessage(deviceHandle, WM_CAP_GET_FRAME, 0, 0);

                // copy the frame to the clipboard
                SendMessage(deviceHandle, WM_CAP_COPY, 0, 0);

                // paste the frame into the event args image
                // get from the clipboard
                tempObj = Clipboard.GetDataObject();
                tempImg = (System.Drawing.Bitmap)tempObj.GetData(System.Windows.Forms.DataFormats.Bitmap);

                return tempImg;
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
    }
}
