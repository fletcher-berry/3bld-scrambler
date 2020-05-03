using BldScramblerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Generates scrambles based on number of twisted corners specified on a line in a scramble file
    /// </summary>
    public class CornerScramblerService : AbstractScramblerService
    {
        private Node cornerNode;
        private Node edgeNode;
        private bool? isFloatingTwist;

        public CornerScramblerService(List<Node> edgeNodes, List<Node> cornerNodes, int numTwisted, bool canBeGreater, bool? isFloating, int weight)
        {
            Weight = weight;
            this.isFloatingTwist = isFloating;

            var edgeLeaves = edgeNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            edgeNode = new Node(edgeLeaves, 0);

            List<FinalLeaf> cornerLeaves;
            switch (isFloating)
            {
                case true:
                    cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability * Corners.ProbFloating(leaf.NumTwisted), leaf.NumTwisted))).ToList();
                    break;
                case false:
                    cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability * (Fraction.One + -Corners.ProbFloating(leaf.NumTwisted)), leaf.NumTwisted))).ToList();
                    break;
                default:
                    cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
                    break;
            }
            var possibleLeaves = canBeGreater ? cornerLeaves.Where(x => x.NumTwisted >= numTwisted).ToList() : cornerLeaves.Where(x => x.NumTwisted == numTwisted).ToList();
            cornerNode = new Node(possibleLeaves, 0);
        }

        public override string Scramble(Random rand)
        {
            return Scrambler.GetScramble(edgeNode, cornerNode, rand, isFloatingTwist);
        }
    }
}
