using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class ScanObject : IObjectContent
    {
        string file_name, //для файлов имя файла
             file_path; //путь до объекта




        bool start_scan_object = false, //флаги, сигнализирующие о начале и конце файла
            end_scan_object = false,  // ..
            start_object_region = false, //флаги, сигнализирующие о начале и конце блока 
            end_object_region = false; // ..



        List<ScanRegion> region_list = new List<ScanRegion> { }; //Список регионов ScanRegion для сканирования
        ScanRegion Scan_Region = new ScanRegion();

        public void Block_split(string path)
        {
            start_scan_object = true; end_scan_object = false; // флаги на начало объекта
            long file_size = Size_file(path),
                fs,
            SAP = /*Size_AP()*/1024 * 1024, // установила размер блока как 1мб
            offset = 0;

            if (SAP >= file_size)                               // если блок больше, чем размер файла, то просто отправляем на отображение
                Scan_Region.Block_read(path, file_size, offset);
            else
            {

                for (int i = 0; file_size > 0; i++) // пока размер файла не больше нуля, смотрим смещение, размер передающего блока, вычитаем размер блока из размера файла
                {
                    offset = SAP * i;

                    if (file_size < SAP)
                        fs = file_size;
                    else
                        fs = SAP;
                    start_object_region = true; end_object_region = false;  //флаги, сигнализирующие о начале блока 

                    Scan_Region.Block_read(path, fs, offset);

                    start_object_region = false; end_object_region = true;  //флаги, сигнализирующие о конце блока 

                    file_size -= fs;

                }
                end_scan_object = true; start_scan_object = false; // флаги на конец объекта

            }


        }


        public void Block_split_zip(byte[] file, long size, string path)
        {

            start_scan_object = true; end_scan_object = false; // флаги на начало объекта
                                                               //  long file_size = Size_file(path),
            long fs,
            SAP = /*Size_AP()*/1024 * 1024, // установила размер блока как 1мб
            offset = 0;

            if (SAP >= size)                               // если блок больше, чем размер файла, то просто отправляем на отображение
                Scan_Region.Block_read_zip(file);
            else
            {

                for (int i = 0; size > 0; i++) // пока размер файла не больше нуля, смотрим смещение, размер передающего блока, вычитаем размер блока из размера файла
                {
                    offset = SAP * i;

                    if (size < SAP)
                        fs = size;
                    else
                        fs = SAP;
                    start_object_region = true; end_object_region = false;  //флаги, сигнализирующие о начале блока 

                    Scan_Region.Block_read_zip(file);

                    start_object_region = false; end_object_region = true;  //флаги, сигнализирующие о конце блока 

                    size -= fs;

                }
                end_scan_object = true; start_scan_object = false; // флаги на конец объекта

            }
        }


        public struct MEMORYSTATUS
        {
            public UInt32 dwLength;
            public UInt32 dwMemoryLoad;
            public UInt32 dwTotalPhys;
            public UInt32 dwAvailPhys;
            public UInt32 dwTotalPageFile;
            public UInt32 dwAvailPageFile;
            public UInt32 dwTotalVirtual;
            public UInt32 dwAvailVirtual;
        }

        const UInt32 memproc = 734003200;
        [DllImport("kernel32.dll")]
        public static extern void GlobalMemoryStatus(ref MEMORYSTATUS lpBuffer);

        public long Size_AP()
        {
            MEMORYSTATUS memStatus = new MEMORYSTATUS();
            GlobalMemoryStatus(ref memStatus);

            // label2.Text = Convert.ToString(memStatus.dwAvailPhys);
            return memStatus.dwAvailPhys - memproc;
        }
        public long Size_file(string path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            return file.Length;
        }


    }
}
