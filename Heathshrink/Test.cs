using System;
using System.IO;

namespace Heathshrink
{
    class Test
    {
        static void Main(string[] args)
        {
            HS_Decoder HSDCD = new HS_Decoder();

            HSDCD.DecompressToFile(Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "compressed.hs"));
        }
    }
}
