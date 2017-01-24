using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedGunReader
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i <= 16; i++)
                {
                    SerialPort mySerialPort = new SerialPort("COM1");
                    mySerialPort.BaudRate = 1200;
                    mySerialPort.Parity = Parity.None;
                    mySerialPort.StopBits = StopBits.One;
                    mySerialPort.DataBits = 8;
                    mySerialPort.Handshake = Handshake.None;
                    mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    mySerialPort.Open();
                    Console.WriteLine("Press any key to continue...");
                    Console.WriteLine();
                    Console.ReadKey();
                    mySerialPort.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception.InnerException + Environment.NewLine);

            }

        }
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Debug.Print("Data Received:");
            Debug.Print(indata);
        }
    }
}
