using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    public class Composer
    {
        /// <summary>
        /// Given a list of permutations, group them into nodes based on thier numbers of algorithms.
        /// </summary>
        /// <param name="leaves"></param>
        /// <returns></returns>
        public static List<Node> GetNodes(List<FinalLeaf> leaves)
        {
            var dict = new Dictionary<int, List<FinalLeaf>>();
            var nodes = new List<Node>();
            foreach (var leaf in leaves)
            {
                var algs = leaf.GetNumAlgs();
                if (!dict.ContainsKey(algs))
                    dict.Add(algs, new List<FinalLeaf>());
                dict[algs].Add(leaf);
            }
            foreach (var pair in dict)
            {
                var node = new Node(pair.Value, pair.Key);
                nodes.Add(node);
            }
            return nodes;
        }

        /// <summary>
        /// From a set of edge nodes and corner nodes, creates Combination Leaves based on the probabilities in the nodes.
        /// </summary>
        /// <param name="edgeNodes"></param>
        /// <param name="cornerNodes"></param>
        /// <returns></returns>
        public static List<CombinationLeaf> GetCombinationLeaves(List<Node> edgeNodes, List<Node> cornerNodes)
        {
            var leaves = new List<CombinationLeaf>();
            foreach (var edgeNode in edgeNodes)
            {
                foreach (var cornerNode in cornerNodes)
                {
                    leaves.Add(new CombinationLeaf(edgeNode, cornerNode));
                }
            }
            return leaves;
        }

        /// <summary>
        /// Given a list of combination leaves, group them into Combination Nodes based on their numbers of algorithms
        /// </summary>
        /// <param name="leaves"></param>
        /// <returns></returns>
        public static List<CombinationNode> GetCombinationNodes(List<CombinationLeaf> leaves)
        {
            var dict = new Dictionary<int, List<CombinationLeaf>>();
            var nodes = new List<CombinationNode>();
            foreach (var leaf in leaves)
            {
                var algs = leaf.NumAlgs;
                if (!dict.ContainsKey(algs))
                    dict.Add(algs, new List<CombinationLeaf>());
                dict[algs].Add(leaf);
            }
            foreach (var pair in dict)
            {
                var node = new CombinationNode(pair.Value, pair.Key);
                nodes.Add(node);
            }
            return nodes;
        }
    }
}
