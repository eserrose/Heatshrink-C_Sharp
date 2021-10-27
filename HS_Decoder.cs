using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharjahSat_Ground_Station.Utilities
{
    class HS_Decoder
    {
        const int MAX_OUTPUT_SIZE = 40 * 1024 * 1024;

        [DllImport("heatshrink.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern IntPtr HeatshrinkDecode(byte* input, int size, out int out_size);
        public unsafe byte[] Decompress(byte[] data)
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;

            fixed (byte* input = &data[0])
                startPtr = HeatshrinkDecode(input, data.Length, out out_size);

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            return buffer;
        }
        public unsafe byte[] Decompress(string filename)
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;
            var data = File.ReadAllBytes(filename);

            fixed (byte* input = &data[0])
                startPtr = HeatshrinkDecode(input, data.Length, out out_size);

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            return buffer;
        }
        public unsafe void DecompressToFile(byte[] data, string outfile = "output.txt")
        {
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;

            fixed (byte* input = &data[0])
                startPtr = HeatshrinkDecode(input, data.Length, out out_size);

            var buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            File.WriteAllBytes(outfile, buffer);
        }
        public unsafe void DecompressToFile(string infile, string outfile = "output.txt")
        {
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;
            var data = File.ReadAllBytes(infile);

            fixed (byte* input = &data[0])
                startPtr = HeatshrinkDecode(input, data.Length, out out_size);

            var buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            File.WriteAllBytes(outfile, buffer);
        }

    }
}
