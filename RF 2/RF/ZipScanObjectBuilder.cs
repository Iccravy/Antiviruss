using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class ZipScanObjectBuilder
    {
        public static void ScanZipFile(string path)
        {
            ZipInputStream zip = new ZipInputStream(File.OpenRead(path));
            ZipEntry item;
            while ((item = zip.GetNextEntry()) != null)
            {
                int size = Convert.ToInt32(item.Size);
                byte[] f = new byte[size];
                int offset = Convert.ToInt32(item.Offset);

                zip.Read(f, offset, size);
                ScanObject scanObject = new ScanObject();
                scanObject.Block_split_zip(f, item.Size, path);

            }
        }
        public static bool IsZIPFile(string path)
        {
            try
            {
                var twoBytes = new byte[2];
                var threeBytes = new byte[3];
                using (var fileStream = File.Open(path, FileMode.Open))
                {
                    fileStream.Read(twoBytes, 0, 2);
                    fileStream.Read(threeBytes, 0, 3);

                }
                if (Encoding.UTF8.GetString(twoBytes) == "PK")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
             }
        }
    }
}
