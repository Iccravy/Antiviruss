using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF
{
    class Node
    {

        public char Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Equal { get; set; }

        public bool End;
        public Node(char data)
        {
            Data = data;
        }

        public Node(char data, Node left, Node right, Node equal, bool end)
        {
            Data = data;
            Left = left;
            Right = right;
            Equal = equal;
            End = end;
        }

        public void Add(string prefix)
        {
            Node current = this;
            int comparer;
            for (int i = 0; i < prefix.Length; i++)
            {
                comparer = prefix[i].CompareTo(current.Data);
                if (comparer == 0)
                {
                    if (i == prefix.Length - 1)
                    {
                        current.End = true;
                        return;
                    }

                    if (current.Equal == null)
                        current.Equal = new Node(prefix[i + 1]);
                    current = current.Equal;
                }
                else
                {
                    if (comparer < 0)
                    {
                        if (current.Left == null)
                            current.Left = new Node(prefix[i]);
                        current = current.Left;
                        i--;
                    }
                    else
                    {
                        if (current.Right == null)
                            current.Right = new Node(prefix[i]);
                        current = current.Right;
                        i--;
                    }

                    if (i == prefix.Length - 1)
                    {
                        current.End = true;
                        return;
                    }
                }
            }
        }
    }
}
