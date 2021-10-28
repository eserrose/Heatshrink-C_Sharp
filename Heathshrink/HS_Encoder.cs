using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Heathshrink
{
    class HS_Encoder
    {
        const int MAX_OUTPUT_SIZE = 40 * 1024 * 1024;

        [DllImport("heatshrink_e.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern IntPtr HeatshrinkEncode(byte* input, int size, out int out_size);
        public unsafe byte[] Compress(byte[] data)
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;

            fixed (byte* input = &data[0])
            {
               startPtr = HeatshrinkEncode(input, data.Length, out out_size);
            }

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            return buffer;
        }
        public unsafe byte[] Compress(string filename)
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;
            var data = File.ReadAllBytes(filename);

            fixed (byte* input = &data[0])
            {
                startPtr = HeatshrinkEncode(input, data.Length, out out_size);
            }

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            return buffer;
        }
        public unsafe void CompressToFile(byte[] data, string outfile = "compressed.hs")
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;

            fixed (byte* input = &data[0])
            {
                startPtr = HeatshrinkEncode(input, data.Length, out out_size);
            }

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            File.WriteAllBytes(outfile, buffer);
        }
        public unsafe void CompressToFile(string infile, string outfile = "compressed.hs")
        {
            byte[] buffer;
            IntPtr startPtr;
            int out_size = MAX_OUTPUT_SIZE;
            var data = File.ReadAllBytes(infile);

            fixed (byte* input = &data[0])
            {
                startPtr = HeatshrinkEncode(input, data.Length, out out_size);
            }

            buffer = new byte[out_size];

            Marshal.Copy(startPtr, buffer, 0, out_size);
            File.WriteAllBytes(outfile, buffer);
        }

    }
}
