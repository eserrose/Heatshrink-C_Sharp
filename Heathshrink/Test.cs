using System.IO;
using System.Text;

namespace Heathshrink
{
    class Test
    {
        const string fileName = "compressed.hs";
        static void Main(string[] args)
        {
            HS_Encoder HSENC = new HS_Encoder();
            HS_Decoder HSDCD = new HS_Decoder();

            byte[] data = Encoding.ASCII.GetBytes("Please don't compress and then decompress me, thanks");

            HSENC.CompressToFile(data, fileName);
            HSDCD.DecompressToFile(fileName);
        }
    }
}
