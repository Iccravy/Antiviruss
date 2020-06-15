using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RF
{
    class ScanSession
    {

        bool type_scan = false;
        public Mutex mutex_scan = new Mutex(false, "mutex_scan");
        public void start_session(string file_path)
        {
            mutex_scan.WaitOne();
            ScanObjectBuilder.main(file_path);
            if (Pause._pause_== true)
            mutex_scan.ReleaseMutex();
        }
    }
}
