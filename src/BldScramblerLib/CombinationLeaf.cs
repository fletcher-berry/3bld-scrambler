using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BldScramblerLib
{
    /// <summary>
    /// Represents a possible number of edge algorithms and corner algorithms to solve a position, along with its relative probability
    /// An example would be '5 edge algorithms and 4 corner algorithms'
    /// </summary>
    public class CombinationLeaf
    {
        [JsonConstructor]
        public CombinationLeaf(int edgeAlgs, int cornerAlgs, Fraction probability)
        {
            EdgeAlgs = edgeAlgs;
            CornerAlgs = cornerAlgs;
            Probability = probability;
        }

        internal CombinationLeaf(Node edgeNode, Node cornerNode)
        {
            var prob = new Fraction(0, 1);
            EdgeAlgs = edgeNode.NumAlgs;
            CornerAlgs = cornerNode.NumAlgs;
            Probability = edgeNode.Probability * cornerNode.Probability;
        }

        public int EdgeAlgs { get; set; }

        public int CornerAlgs { get; set; }

        public Fraction Probability { get; set; }

        public int NumAlgs => EdgeAlgs + CornerAlgs;
    }
}
