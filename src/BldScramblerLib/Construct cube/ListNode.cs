using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public class ListNode<T>
    {
        public T Value;
        public ListNode<T> Right;
        public ListNode<T> Left;

        public ListNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }

        public ListNode(T value, ListNode<T> left, ListNode<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }
}
