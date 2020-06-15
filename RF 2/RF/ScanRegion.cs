using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class ScanRegion
    {
        Signature tmp = new Signature();


        int segment_size, //Размер сегмента
            scan_offset_begin; //Смещение относительно начала контента

        const int BLOCK_SIZE = 8 * 1024; // 1 кбайт



        public void Block_read(string path, long FileSize, long offset) //Метод блочного чтения заданного региона
        {
            byte[] arr = new byte[8 * 1024];
            string file_name = Path.GetFileName(path);
            using (var mmf = MemoryMappedFile.CreateFromFile(path, FileMode.Open, file_name))
            {
                using (var accessor = mmf.CreateViewAccessor(offset, FileSize))
                {
                    bool rep;
                    accessor.ReadArray(BLOCK_SIZE, arr, 0, arr.Length);
                    tmp.Load_all_line_in_base();
                   tmp.FindSignature(Encoding.Default.GetString(arr), path);
                    

                    //тут передаем куда нужно и освобождаем потоки
                    mmf.Dispose();
                    accessor.Dispose();
                }
            }


        }


        public void Block_read_zip(byte[] file) //Метод блочного чтения заданного региона
        {


            //tmp.zap();
            tmp.Load_all_line_in_base();
            tmp.FindSignature(Encoding.Default.GetString(file), "zip");




        }
    }
}
