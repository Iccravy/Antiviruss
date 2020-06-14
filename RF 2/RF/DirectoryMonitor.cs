using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace RF
{
    class DirectoryMonitor
    {
        static Mutex mutexObj = new Mutex();


        public class list
        {
            public string Edit_time, path;
            public void list_insert(string path, string edit_time)
            {
                this.Edit_time = edit_time;
                this.path = path;
            }
        }

        public list Queue;


        private List<string> GetFiles(string path)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                    files.AddRange(GetFiles(directory));
            }
            catch (UnauthorizedAccessException) { }

            return files;
        }


        void add_to_queue(string file_path)
        {
            Queue.list_insert(file_path, Get_time_change(file_path));
            MonitoringDirectoriesRepository insert = null;
            insert.insert(Queue);
            //отправляем куда надо 
        }
        
        string Get_time_change(string path) 
        {
            return Convert.ToString(File.GetLastWriteTime(path));
        }

    }
}

