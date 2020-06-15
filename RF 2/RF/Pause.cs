using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class Pause
    {
        public static bool _pause_ = true;
        public void pause_all(bool ins)
        {
            _pause_ = ins;
        }
    }
}
