using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class PEScanObjectBuilder
    {

        public static bool IsExeFile(string path)
        {
            var twoBytes = new byte[2];
            try
            {
                using (var fileStream = File.Open(path, FileMode.Open))
                {
                    fileStream.Read(twoBytes, 0, 2);
                }

                return Encoding.UTF8.GetString(twoBytes) == "MZ";
            }
            catch
            {
                return false;
            }
        }
    }
}
