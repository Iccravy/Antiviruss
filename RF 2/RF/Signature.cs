using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class Signature : IComparable<Signature>
    {

        public string name;
        public int signature_length; // k символов сигнатуры (n<=k)
        public string signature_prefix; // n символов сигнатуры, которые хранятся в базе для поиска вхождения
        public string signature_hash; // SHA256-хэш сигнатуры
        public int offset_begin; //минимальное смещение первого символа сигнатуры относительно начала
        public int offset_end; //максимальное смещение от начала файла
        private Dictionary<string, Signature> signature_dict = new Dictionary<string, Signature>(); //словарь для хранения Ключа-Префикса и Значения-Экземпляра класса
        private Tree tree = new Tree();
        private int line = 0;
        readonly Work_with_data wwd = new Work_with_data();
        public Signature() { } // конструктор

        // конструктор класса по атрибутам
        //private Signature(string name, int signature_length, string signature_prefix, string signature_hash, int offset_begin, int offset_end)
        //{
        //    this.name = name;
        //    this.signature_length = signature_length;
        //    this.signature_prefix = signature_prefix;
        //    this.signature_hash = signature_hash;
        //    this.offset_begin = offset_begin;
        //    this.offset_end = offset_end;

        //}

        public string ret(string tmp)
        {
            string[] pre = tmp.Split(new char[] { '_' });
            byte[] arr = new byte[pre.Length];
            for (int i = 0; i < pre.Length; i++)
            {
                int tm = Convert.ToInt32(pre[i]);
                arr[i] = Convert.ToByte(tm);

            }
            string lol = Encoding.Default.GetString(arr);
            return Encoding.Default.GetString(arr);
        }

        //метод класса для разбиения прочитанной строки и создания экземпляра класса
        // с последующим добавлением этого экземпляра с словарь и сохранением префикса в узле бинарного дерева
        public void LineSplit(string line)
        {
            string[] tmp = line.Split(new char[] { ' ' });

            try
            {
                signature_dict.Add(ret(tmp[2]), new Signature
                {
                    name = tmp[0],
                    signature_length = Convert.ToInt16(tmp[1]),
                    //signature_prefix = tmp[2],
                    signature_prefix = ret(tmp[2]),
                    signature_hash = tmp[3],
                    offset_begin = Convert.ToInt16(tmp[4]),
                    offset_end = Convert.ToInt16(tmp[5])
                });
                //tree.Add(tmp[2]);

                tree.Add(ret(tmp[2]));
            }
            catch (Exception) { }
        }

        //загрузка всех записей из файла в список
        public void Load_all_line_in_base()
        {
            for (int i = 0; i < System.IO.File.ReadAllLines(wwd.path_to_db_file).Length; i++)
            {
                LineSplit(wwd.Read_from_file_one_line(i));
            }
        }
      
        //Использует алгоритм двоичного поиска для нахождения определенного элемента в отсортированном списке
        private string Find_prefix(string region)
        {
            string find_signature = "";

            Node current = tree.Root;


            int size = region.Length;
            for (int i = 0; i < region.Length;)
            {
                char kek = current.Data;
                char lol = region[i];


                current.Data.CompareTo(region[i]);
                if (current.Data.CompareTo(region[i]) == 0)
                {
                    find_signature += current.Data;
                    if (current.End)
                        return find_signature;
                    i++;
                    current = current.Equal;
                }
                else
                {
                    if (region[i] < current.Data)
                        current = current.Left;
                    else
                        if (region[i] > current.Data)
                        current = current.Right;
                }
                if (current == null)
                    break;
            }
            return "";
        }

        public bool FindSignature(string tmp, string path)
        {
            // Load_all_line_in_base();
            string buffer = tmp, to_hash_line = "";

            string wm_str = Find_prefix(tmp);
            while (wm_str == "" && tmp != "")
            {

                tmp = tmp.Substring(1);
                wm_str = Find_prefix(tmp);

            }
            if (wm_str != "") //добавить сверку хэша найденного фрагмента и сигнатуры
            {
                if (signature_dict.TryGetValue(wm_str, out var value))
                {
                    if (tmp.Length >= value.signature_length)
                    {
                        for (int i = 0; i < value.signature_length; i++)
                            to_hash_line += tmp[i];

                        Calculating_hash calc_hash = new Calculating_hash();
                        bool rep = calc_hash.Check_hash(to_hash_line, value.signature_hash);
                        if (rep)
                        {
                            ScanEngine.Report(value.name, path);
                        }
                        return calc_hash.Check_hash(to_hash_line, value.signature_hash);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }


        public int CompareTo(Signature other)                                    //   Этот метод использует Array.Sort, который применяет сортировку гибридности следующим образом:
        {                                                                        //   Если размер секции меньше 16 элементов или равен ему, он использует алгоритм сортировки вставки.
            return signature_prefix.CompareTo(other.signature_prefix);           //   Если количество секций превышает 2 log n, где n — диапазон входного массива, используется алгоритм хеапсорт.
        }                                                                        //   В противном случае используется алгоритм QuickSort.

        ////доп. метод по сравнению элементов для бинарного поиска
        //public int Compare(Signature x, Signature y)
        //{
        //    return x.signature_prefix.CompareTo(y.signature_prefix);
        //}
    }
}
