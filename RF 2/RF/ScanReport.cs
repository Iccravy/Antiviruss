using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class ScanReport
    {
        public string initscan;
        public string start_scan;
        public string end_scan;
        public double scaned_files;
        public double scaned_objects;
        public double danger_count;
        public List<string> viruses = new List<string>();
        public List<string> file_path = new List<string>();
        
        public void insert_Report(string initscan, string start, string end, double scaned_files, double scaned_objects, double danger_count,  string vir_type, string file_path )
        {
            this.initscan = initscan;
            this.start_scan = start;
            this.end_scan = end;
            this.scaned_files = scaned_files;
            this.scaned_objects = scaned_objects;
            this.danger_count=danger_count;
            this.viruses.Add(vir_type);
            this.file_path.Add(file_path);
            
        }

        
    }
}
