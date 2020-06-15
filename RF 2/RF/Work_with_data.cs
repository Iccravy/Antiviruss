using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RF
{
    class Work_with_data
    {

        public Mutex mutex_wwd = new Mutex(false, "mutex_wwd");
        public string signature_db_file_name = "SignatureDB.txt", path_to_db_file;

        public Work_with_data()
        {
            path_to_db_file = Path.Combine(Directory.GetCurrentDirectory(), signature_db_file_name);

        }
       
        public void Write_to_end_file(string virus_name, int signature_length, string signature_prefix, string signature_hash, int offset_bigin, int offset_end)
        {
            string text = virus_name + " " + signature_length.ToString() + " " + signature_prefix + " " +
                signature_hash + " " + offset_bigin.ToString() + " " + offset_end.ToString();
            byte[] zap = System.Text.Encoding.UTF8.GetBytes(text);
            mutex_wwd.WaitOne();
            try
            {
                /* TextWriter file = File.(path_to_db_file);
                 for (int i = 0; i < zap.Length; i++)
                     file.WriteLine(zap[i]);
                 file.WriteLine(" ----------- ");
                 file.Close();*/

                using (StreamWriter writer = new StreamWriter(path_to_db_file, true, System.Text.Encoding.Unicode))
                {
                    writer.WriteLine(text);
                }
                /*
                using (StreamWriter writer = new StreamWriter(path_to_db_file, true, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < zap.Length; i++)
                    writer.Write(zap[i]);
                    writer.WriteLine("");
                }*/
            }
            catch (Exception)
            { }
            finally
            {
                mutex_wwd.ReleaseMutex();
            }

        }

        public string Read_from_file_one_line(int number_line)
        {
            try
            {
                mutex_wwd.WaitOne();
                string line = File.ReadLines(path_to_db_file).ElementAt(number_line);



                return line;
            }
            catch (Exception)
            {
                return "-1";
            }
            finally
            {
                mutex_wwd.ReleaseMutex();
            }
        }
    }
}
