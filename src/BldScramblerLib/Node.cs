using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Groups a set of permutations and weights their probabilities
    /// This is generally used to group the permutations by their number of algorithms, but can also be used to group them by other criteria suce number of twisted pieces.
    /// </summary>
    public class Node
    {
        public Node(List<FinalLeaf> leaves, int numAlgs)
        {
            var prob = new Fraction(0, 1);
            foreach (var leaf in leaves)
            {
                prob += leaf.Probability;
            }
            Probability = prob;
            Leaves = leaves.Select(x => new FinalLeaf(x.Cycles, x.Probability * prob.GetReciprical(), x.NumTwisted)).ToList();
            NumAlgs = numAlgs;
        }

        public List<FinalLeaf> Leaves { get; set; }

        public int NumAlgs { get; set; }

        public Fraction Probability { get; set; }
    }
}
