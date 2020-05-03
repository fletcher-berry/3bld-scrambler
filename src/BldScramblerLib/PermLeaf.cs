using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public class PermLeaf
    {
        public List<int> Cycles;

        public Fraction Probability;

        /// <summary>
        /// Represents the permutation of a set of pieces and its relative probability.
        /// The permutation is represented as list of ints where each int is the number of pieces in a cycle.  The first value in the list is the cycle contining the buffer.
        /// Ex: {4, 1, 1, 2} with 1 twist - there is a 4 piece cycle containing the buffer, a 2 piece cycle, and 2 correctly permuted pieces
        /// </summary>
        public PermLeaf(List<int> cycles, Fraction probability)
        {
            Cycles = cycles;
            Probability = probability;
        }

        public override string ToString()
        {
            return string.Join(" - ", Cycles) + "    " + Probability.ToString();
        }
    }
}
