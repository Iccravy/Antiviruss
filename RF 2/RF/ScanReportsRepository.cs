using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;


namespace RF
{
    class ScanReportsRepository 
    {
        public Mutex mutex_SRR = new Mutex();
        public string SRR = "c:/temp/SRR.txt";

        public void insert(ScanReport report)
        {
            int i = 0;
            mutex_SRR.WaitOne();
            int counter = report.viruses.Count();
            if (File.Exists(SRR))
            {
                using (StreamWriter FD = new StreamWriter(SRR, true))
                {
                    FD.WriteLine(report.initscan + ", " + report.start_scan + ", " + report.end_scan + ", " + report.scaned_files + ", " + report.scaned_objects + ", " + report.danger_count + "|");
                    while (i < counter)
                    {
                        FD.WriteLine(Convert.ToString(report.viruses[i]) + ", " + Convert.ToString(report.file_path[i]));
                        i++;
                    }
                }
                i = 0;
            }
            mutex_SRR.ReleaseMutex();
        }
        public string read()
        {
            string str;
            int i = 0;
            mutex_SRR.WaitOne();
            using (StreamReader FD = new StreamReader(SRR))
            {
                str = FD.ReadToEnd();
            }
            i = 0;
            mutex_SRR.ReleaseMutex();
            return (str);

        }
    }
}
