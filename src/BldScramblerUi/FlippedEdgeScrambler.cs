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
    /// Generates scrambles based no the number of flipped edges
    /// </summary>
    public partial class FlippedEdgeScrambler : ScramblerControl
    {
        private List<FinalLeaf> edgeLeaves;
        private Node cornerNode;
        private Random rand;

        private const string Equal = "Equal";
        private const string EqualOrGreater = "At least";

        public FlippedEdgeScrambler(List<Node> edgeNodes, List<Node> cornerNodes, Random rand)
        {
            InitializeComponent();

            CardinalityBox.Items.Add(Equal);
            CardinalityBox.Items.Add(EqualOrGreater);
            CardinalityBox.SelectedItem = Equal;

            edgeLeaves = edgeNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            var cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            cornerNode = new Node(cornerLeaves, 0);

            this.rand = rand;
        }

        public override string Scramble()
        {
            if (!int.TryParse(FlippedEdgesInput.Text, out int flippedEdges))
            {
                MessageBox.Show("Enter the number of flipped edges in the box", "Invalid number of flipped edges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if (flippedEdges < 0 || flippedEdges > 11)
            {
                MessageBox.Show("Number of flipped edges must be between 0 and 11", "Invalid number of flipped edges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            var canBeGreater = CardinalityBox.SelectedItem.Equals(EqualOrGreater);
            var possibleLeaves = canBeGreater ? edgeLeaves.Where(x => x.NumTwisted >= flippedEdges).ToList() : edgeLeaves.Where(x => x.NumTwisted == flippedEdges).ToList();
            var edgeNode = new Node(possibleLeaves, 0);
            var scramble = Scrambler.GetScramble(edgeNode, cornerNode, rand, null);

            return scramble;
        }
    }
}
