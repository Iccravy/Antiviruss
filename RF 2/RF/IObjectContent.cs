using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    interface IObjectContent
    {

        long Size_AP(); //Метод получения размера содержимого


        void Block_split(string path); //Метод блочного чтения данных с заданной позиции, 
                           //Метод должен возвращать количество реально прочитанных байт
    }
}
