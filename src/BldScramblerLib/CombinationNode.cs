using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Weights the relative probabilities of a set of Combination Leaves.
    /// This is generally used to represent possibilities of the numbers of edge algorithms and corner algorithms to solve a position given the total number of algorithms 
    /// </summary>
    public class CombinationNode
    {
        public CombinationNode(List<CombinationLeaf> leaves, int numAlgs)
        {
            var prob = new Fraction(0, 1);
            foreach (var leaf in leaves)
            {
                prob += leaf.Probability;
            }
            Probability = prob;
            foreach (var leaf in leaves)
            {
                leaf.Probability *= prob.GetReciprical();
            }
            NumAlgs = numAlgs;
            Leaves = leaves;
        }

        public List<CombinationLeaf> Leaves { get; set; }

        public int NumAlgs { get; set; }

        public Fraction Probability { get; set; }

    }
}
