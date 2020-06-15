using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class Tree
    {

        public Node Root { get; set; }
        public int Count { get; set; }

        public void Add(string prefix)
        {

            if (Root == null)
            {
                Root = new Node(prefix[0]);
                Root.Add(prefix);
            }
            else
            {
                Root.Add(prefix);
            }
            Count++;
        }
    }
}
