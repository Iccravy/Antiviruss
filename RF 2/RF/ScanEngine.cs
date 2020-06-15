using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace RF
{
    class ScanEngine
    {
        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        ///    ВСТАВЬ СЮДА ПУТЬ И НАИМЕНОВАНИЕ ВИРУСА
        /// </summary>
        public static string vir_path=null;
        public static string vir_type=null;

        public static void Report(string name, string path)
        {
            vir_path = name;
            vir_type = name;
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        /// </summary>
        public static double scaned_files;
        public static double scaned_objects;

        List<string> Info = new List<string>();
        List<bool> type = new List<bool>();

        ScanSession ini = new ScanSession();


        public string inintScan(bool value)
        {
            if (value == true)
                return "User's init";
            else
                return "Autoscan";
        }
        public void file_step(string file_path)
        {
            try
            {
                foreach (string f in Directory.GetFiles(file_path))
                {
                    ini.start_session(f);
                }
                foreach (string d in Directory.GetDirectories(file_path))
                {
                    file_step(d);
                }
            }
            catch { }
        }

        public void start_scan()
        {
            Mutex scan_Mutex = new Mutex();
            scan_Mutex.WaitOne();
            ScanReport Scaning = new ScanReport();
            //ScanSession ini = new ScanSession();
            
            for (int i = 0; i < Info.Count(); i++)
            {
                string file_path = Info[i];
                bool type_scan = type[i];
                string save_time;
                int count_vir = 0;
                save_time = Get_Time();
                file_step(file_path);               
                if (vir_path != null && vir_type != null)
                {
                    count_vir++;
                    Scaning.insert_Report(inintScan(type_scan), save_time, Get_Time(), scaned_files, scaned_objects, count_vir, vir_type, vir_path);
                    vir_path = null;
                    vir_type = null;
                }
                ScanReportsRepository inserter = new ScanReportsRepository();
                inserter.insert(Scaning);
            }
            scan_Mutex.ReleaseMutex();
        }

        public void make_Queue(string str, bool typ)
        {
            Info.Add(str);
            type.Add(typ);
        }

        public void vir_insert(string vir_path_ins, string vir_type_ins)
        {
            vir_path = vir_path_ins;
            vir_type = vir_type_ins;
        }

        public string Get_Time()
        {
            DateTime Curr_Time = DateTime.Now;
            return Curr_Time.ToString("dd-mm-yyyy hh:mm:ss");
        }
    }
}
