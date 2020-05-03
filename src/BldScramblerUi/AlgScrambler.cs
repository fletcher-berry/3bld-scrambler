using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BldScramblerLib;

namespace BldScramblerUi
{
    /// <summary>
    /// Generates scrambles based on the number of algorithms.
    /// </summary>
    public partial class AlgScrambler : ScramblerControl
    {
        private Random rand;
        private List<Node> edgeNodes;
        private List<Node> cornerNodes;
        private List<CombinationNode> combinationNodes;

        public AlgScrambler(List<Node> edgeNodes, List<Node> cornerNodes, List<CombinationNode> combinationNodes, Random rand)
        {
            InitializeComponent();
            this.rand = rand;
            this.edgeNodes = edgeNodes;
            this.cornerNodes = cornerNodes;
            this.combinationNodes = combinationNodes;
        }


        public override string Scramble()
        {
            if(!int.TryParse(NumAlgsInput.Text, out int numAlgs))
            {
                MessageBox.Show("Enter the number of algs in the box", "Invalid number of algs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if(numAlgs < 1 || numAlgs > 14)
            {
                MessageBox.Show("Number of algs must be between 1 and 14", "Invalid number of algs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            var combinationNode = combinationNodes.Single(x => x.NumAlgs == numAlgs);
            var scramble = Scrambler.GetScramble(edgeNodes, cornerNodes, combinationNode, rand);

            return scramble;
        }

    }
}
