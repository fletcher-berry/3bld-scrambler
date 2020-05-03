using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Represents the permutation of some pieces in a set.
    /// The permutation is represented as list of ints where each int is the number of pieces in a cycle.  The first value in the list is the cycle contining the buffer.
    /// The last value in the list is the number of pieces where the cycles are not yet known
    /// Ex: {4, 4} - there is a 4 piece cycle containing the buffer, and some permutation of the other 4 pieces.  It could be a 4-cycle, 2 2-swaps, or any other valid permutation
    /// </summary>
    public class Perm
    {
        public List<int> Cycles;

        public Fraction Probability;

        public Perm(List<int> cycles, Fraction probability)
        {
            Cycles = cycles;
            Probability = probability;
        }

    }
}
