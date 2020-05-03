using BldScramblerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerUi
{
    /// <summary>
    /// Generates scrambles based on number of algorithms specified on a line in a scramble file
    /// </summary>
    public class AlgScramblerService : AbstractScramblerService
    {
        private CombinationNode combinationNode;
        private List<Node> edgeNodes;
        private List<Node> cornerNodes;

        public AlgScramblerService(CombinationNode combinationNode, List<Node> edgeNodes, List<Node> cornerNodes, int weight)
        {
            this.combinationNode = combinationNode;
            this.edgeNodes = edgeNodes;
            this.cornerNodes = cornerNodes;
            this.Weight = weight;
        }

        public override string Scramble(Random rand)
        {
            var scramble = Scrambler.GetScramble(edgeNodes, cornerNodes, combinationNode, rand);
            return scramble;
        }
    }
}
