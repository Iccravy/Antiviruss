using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
 
namespace RF
{
    class MonitoringDirectoriesRepository
    {
        public Mutex mutex_DirRep = new Mutex();
        public string DirRep = "DirRep.txt";
        bool sys_write = true;
        public void insert(DirectoryMonitor.list Insert_date)
        {
            mutex_DirRep.WaitOne();
            if (!File.Exists(DirRep))
            {
                using (StreamWriter FD = new StreamWriter(DirRep, sys_write))
                    FD.WriteLine(Convert.ToString(Insert_date.path), "|", Convert.ToString(Insert_date.Edit_time), "|\n");
            }
            mutex_DirRep.ReleaseMutex();
        }
        public List<DirectoryMonitor.list> read()
        {
            string str;
            string temp_path=null;
            string temp_time = null;
            mutex_DirRep.WaitOne();
            List<DirectoryMonitor.list> Queue = new List<DirectoryMonitor.list>();
            using (StreamReader FD = File.OpenText(DirRep))
            {
                while (FD.Read() != -1)
                {
                    while ((str = Convert.ToString((char)FD.Read())) != "|")
                        temp_path = temp_path + str;
                    while ((str = Convert.ToString((char)FD.Read())) != "|")
                        temp_time = temp_time + str;
                    Queue.Add(new DirectoryMonitor.list() { path = temp_path, Edit_time = temp_time });
                }
            }
            mutex_DirRep.ReleaseMutex();
            return Queue;
        }

        public void edit_file(int numb_row)
        {
            List<DirectoryMonitor.list> Queue = read();
            int N = Queue.Count-1;
            if (numb_row == N)
                N--;
            sys_write = false;
            for (int i = 0; i < N; i++)
            {
                if (i == numb_row)
                    i++;
                insert(Queue[i]);
            }
            sys_write = true;

        }

    }
}
