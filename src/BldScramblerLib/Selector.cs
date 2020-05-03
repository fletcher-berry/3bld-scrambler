using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public static class Selector
    {
        /// <summary>
        /// Randomly selects an edge and corner permutation based on the probabilities of the combination leaves, and the probabilities of the permutations in the nodes
        /// </summary>
        /// <param name="combinationNode">A node specifying the combinations of numbers of algorithms for edges and corners</param>
        /// <param name="edgeNodes">All edge permutations and their relative probabilities</param>
        /// <param name="cornerNodes">All corner permutations and their relative probabilities</param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Tuple<FinalLeaf, FinalLeaf> SelectPermutation(this CombinationNode combinationNode, List<Node> edgeNodes, List<Node> cornerNodes, Random random)
        {
            var combinationLeaf = combinationNode.Leaves.SelectRandom(x => x.Probability, random);
            var edgeNode = edgeNodes.Single(x => x.NumAlgs == combinationLeaf.EdgeAlgs);
            var cornerNode = cornerNodes.Single(x => x.NumAlgs == combinationLeaf.CornerAlgs);
            return SelectPermuataion(edgeNode, cornerNode, random);
        }

        /// <summary>
        /// Randomly selects an edge and corner permutation based on the probabilities of the permutations in the nodes
        /// </summary>
        /// <param name="edgeNodes">The set of edge permutations and their probabilities</param>
        /// <param name="cornerNodes">The set of corner permutations and their probabilities</param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Tuple<FinalLeaf, FinalLeaf> SelectPermuataion(Node edgeNode, Node cornerNode, Random rand)
        {
            var edgeLeaf = edgeNode.Leaves.SelectRandom(x => x.Probability, rand);
            var cornerLeaf = cornerNode.Leaves.SelectRandom(x => x.Probability, rand);
            return Tuple.Create(edgeLeaf, cornerLeaf);
        }
    }
}
