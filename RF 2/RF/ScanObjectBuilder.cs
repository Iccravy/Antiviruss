
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Http;

namespace RF
{
    class ScanObjectBuilder
    {
        public static void main(string path)
        {

            if (PEScanObjectBuilder.IsExeFile(path))
            {
                ScanObject scanObject = new ScanObject();
                scanObject.Block_split(path);
            }

             if (ZipScanObjectBuilder.IsZIPFile(path))
            {
                ZipScanObjectBuilder.ScanZipFile(path);
            }


        }

    }
}
