using BldScramblerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Generates scrambles based on number of flipped edges specified on a line in a scramble file
    /// </summary>
    class EdgeScramblerService : AbstractScramblerService
    {
        private Node cornerNode;
        private Node edgeNode;

        public EdgeScramblerService(List<Node> edgeNodes, List<Node> cornerNodes, int numFlipped, bool canBeGreater, int weight)
        {
            Weight = weight;

            var cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            cornerNode = new Node(cornerLeaves, 0);

            var allLeaves = edgeNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            var edgeLeaves = canBeGreater ? allLeaves.Where(x => x.NumTwisted >= numFlipped).ToList() : allLeaves.Where(x => x.NumTwisted == numFlipped).ToList();
            edgeNode = new Node(edgeLeaves, 0);
        }

        public override string Scramble(Random rand)
        {
            return Scrambler.GetScramble(edgeNode, cornerNode, rand, null);
        }
    }
}
