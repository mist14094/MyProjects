using System;
using System.Collections.Generic;
using System.Text;

namespace KTone.RFIDGlobal.CRC
{
    public class CRCAlgorithms
    {
        public static void CompareCRC_CCITT(byte[] responseBytes)
        {
            if (responseBytes == null || responseBytes.Length < 2)
                throw new ApplicationException("Invalid response");
            byte[] bytes = new byte[responseBytes.Length - 2];
            Array.Copy(responseBytes, bytes, responseBytes.Length - 2);
            byte[] crcBytes = new byte[2];
            GenCRC_CCITT(bytes, out crcBytes);
            for (int i = responseBytes.Length - 2, j = 0; i < responseBytes.Length; i++, j++)
            {
                if (responseBytes[i] != crcBytes[j])
                    throw new ApplicationException("Checksum mismatch");
            }
        }

        /// <summary>
        /// Algorithm to generate CRC 
        /// </summary>
        /// <param name="bytes"></param>
        public static void GenCRC_CCITT(byte[] bytes, out byte[] crcBytes)
        {
            ushort crc = ushort.MaxValue;
            foreach (byte b in bytes)
            {
                Generate_CRC(b, ref crc);
            }

            crcBytes = new byte[2];
            crcBytes[0] = (byte)((crc & 0x00FF) ^ 0xFF); // LSB
            crcBytes[1] = (byte)(((crc >> 8) & 0x00FF) ^ 0xFF); //MSB 
        }

        public static void Generate_CRC(byte c, ref ushort crc)
        {
            byte d, e, f, g, h;

            d = 0;

            e = (byte)((crc & 0x00FF) ^ c);

            //                byte "d"              byte "e"	
            //           x x x x x x x 1      x x x x x x x 1
            //           lsb of "d" is "g"     lsb of "e" is "f" 			  

            for (h = 0; h < 8; h++)			// for all 8 bits
            {
                g = (byte)(d & 0x01);
                d >>= 1;
                d &= 0x7F;

                f = (byte)(e & 0x01);
                e >>= 1;
                e &= 0x7F;

                if (g > 0)				// if shifted "1" from lsb of d
                    e |= 0x80;

                if (f > 0)				// if shifted "1" from lsb of e
                {
                    d ^= 0x84;
                    e ^= 0x08;
                }
            }

            e ^= (byte)((crc >> 8) & 0x00FF);  	// new crc low byte

            crc = (ushort)(((ushort)((d << 8) & 0xFF00)	// new crc high byte
                        +				//  +
              (ushort)e));				// low byte

        }
    }
}
