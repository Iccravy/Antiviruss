using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class Calculating_hash
    {
        public bool Check_hash(string line, string hash_record)
        {
            string hash_in_hex = Generate_hash(line);
            if (string.Compare(hash_in_hex, hash_record) == 0)
                return true;
            else
                return false;
        }

        public string Generate_hash(string line)
        {
            var msgBytes = Encoding.ASCII.GetBytes(line);
            var sha = new System.Security.Cryptography.SHA256Managed();
            var hash = sha.ComputeHash(msgBytes);

            string hash_in_hex = "";
            foreach (byte b in hash)
            {
                hash_in_hex += b.ToString("x2");
            }
            return hash_in_hex;
        }
    }
}
