using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace KTone.RFIDGlobal
{
    public class BeepSystem
    {
        /// <summary>
        /// used to beep the System speaker for specified Time and Frequency.
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(UInt32 frequency, UInt32 duration);

        public static void BeepPC(uint frequency, uint durationInMS)
        {
            //uint Frequency = 2000;
            //uint DurationInMS = 5000;
            Beep(frequency, durationInMS);
        }
    }
}
