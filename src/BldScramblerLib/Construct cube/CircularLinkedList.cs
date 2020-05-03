using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Represents a generic circular linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularLinkedList<T>
    {
        /// <summary>
        /// Gets or sets the head of this list
        /// </summary>
        public ListNode<T> Head;

        /// <summary>
        /// Creates an empty circular linked list
        /// </summary>
        public CircularLinkedList()
        {
            Head = null;
        }

        /// <summary>
        /// Creates a circular linked list and populates it with the given values
        /// </summary>
        /// <param name="values">The initial values of the list in order</param>
        public CircularLinkedList(params T[] values)
        {
            if(values == null || values.Length == 0)
            {
                Head = null;
            }
            else
            {
                ListNode<T> first = new ListNode<T>(values[0]);
                ListNode<T> last = first;
                for(int k = 1; k < values.Length; k++)
                {
                    ListNode<T> curr = new ListNode<T>(values[k]);
                    curr.Left = last;
                    last.Right = curr;
                    last = curr;
                }
                last.Right = first;
                first.Left = last;
                Head = first;
            }
        }
    }
}
