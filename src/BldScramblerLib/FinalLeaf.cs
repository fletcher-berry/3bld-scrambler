using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Represents the permutation of a set of pieces and the number of pieces correctly permuted, but misoriented.
    /// The permutation is represented as list of ints where each int is the number of pieces in a cycle.  The first value in the list is the cycle contining the buffer.
    /// Ex: {4, 1, 1, 2} with 1 twist - there is a 4 piece cycle containing the buffer, a 2 piece cycle, a solved piece, and a piece twisted in place.
    /// </summary>
    public class FinalLeaf
    {
        public List<int> Cycles;

        public Fraction Probability;
        public int NumTwisted;

        public FinalLeaf(List<int> cycles, Fraction probability, int numTwisted)
        {
            Cycles = cycles;
            Probability = probability;
            NumTwisted = numTwisted;
        }

        public override string ToString()
        {
            return string.Join(" - ", Cycles) + "    " + Probability.ToString() + "   " + NumTwisted;
        }

        /// <summary>
        /// Gets the number of algorithms to solve the permutation based on using the 3-style method, and the ability to solve 2 twisted pieces outside the buffer at a time.
        /// </summary>
        /// <returns></returns>
        public int GetNumAlgs()
        {
            var targets = 0;
            var algs = 0;
            targets += Cycles[0] - 1;
            for(int k = 1; k < Cycles.Count; k++)
            {
                if (Cycles[k] != 1)
                    targets += Cycles[k] + 1;
            }
            algs = (targets + 1) / 2;
            algs += (NumTwisted + 1) / 2;
            return algs;
        }
    }
}
