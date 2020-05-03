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
    /// generates scrambles based on the number of twisted corners
    /// </summary>
    public partial class TwistedCornerScrambler : ScramblerControl
    {
        private List<FinalLeaf> cornerLeaves;
        private List<FinalLeaf> floatingLeaves;
        private List<FinalLeaf> nonFloatingLeaves;
        private Node edgeNode;
        private Random rand;

        private const string Equal = "Equal";
        private const string EqualOrGreater = "At least";

        public const string Yes = "yes";
        public const string No = "no";
        public const string Idk = "don't care";

        private static Dictionary<int, Fraction> FloatingProb = new Dictionary<int, Fraction>();

        public TwistedCornerScrambler(List<Node> edgeNodes, List<Node> cornerNodes, Random rand)
        {
            InitializeComponent();
           
            cornerLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            floatingLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability * Corners.ProbFloating(leaf.NumTwisted), leaf.NumTwisted))).ToList();
            nonFloatingLeaves = cornerNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability * (Fraction.One + -Corners.ProbFloating(leaf.NumTwisted)), leaf.NumTwisted))).ToList();

            var edgeLeaves = edgeNodes.SelectMany(node => node.Leaves.Select(leaf => new FinalLeaf(leaf.Cycles, leaf.Probability * node.Probability, leaf.NumTwisted))).ToList();
            edgeNode = new Node(edgeLeaves, 0);

            this.rand = rand;

            CardinalityBox.Items.Add(Equal);
            CardinalityBox.Items.Add(EqualOrGreater);
            CardinalityBox.SelectedItem = Equal;

            FloatingInput.Items.Add(Yes);
            FloatingInput.Items.Add(No);
            FloatingInput.Items.Add(Idk);
            FloatingInput.SelectedItem = Idk;
        }

        public override string Scramble()
        {
            if (!int.TryParse(TwistedCornersInput.Text, out int twistedCorners))
            {
                MessageBox.Show("Enter the number of twisted corners in the box", "Invalid number of twisted corners", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if (twistedCorners < 0 || twistedCorners > 7)
            {
                MessageBox.Show("Number of twisted corners must be between 0 and 7", "Invalid number of twisted corners", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if (twistedCorners == 0 && FloatingInput.SelectedItem.Equals(No))
            {
                MessageBox.Show("0-twists must be floating", "Invalid corners", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if (twistedCorners == 1 && FloatingInput.SelectedItem.Equals(Yes))
            {
                MessageBox.Show("1-twists must not be floating", "Invalid corners", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            var canBeGreater = CardinalityBox.SelectedItem.Equals(EqualOrGreater);

            bool? isFloatingTwist;
            List<FinalLeaf> baseLeaves;
            switch ((string)FloatingInput.SelectedItem) {
                case Yes:
                    isFloatingTwist = true;
                    baseLeaves = floatingLeaves;
                    break;
                case No:
                    isFloatingTwist = false;
                    baseLeaves = nonFloatingLeaves;
                    break;
                default:
                    isFloatingTwist = null;
                    baseLeaves = cornerLeaves;
                    break;
            }

            var possibleLeaves = canBeGreater ? baseLeaves.Where(x => x.NumTwisted >= twistedCorners).ToList() : cornerLeaves.Where(x => x.NumTwisted == twistedCorners).ToList();
            var cornerNode = new Node(possibleLeaves, 0);

            var scramble = Scrambler.GetScramble(edgeNode, cornerNode, rand, isFloatingTwist);
           
            return scramble;
        }

        private void CardinalityBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if(CardinalityBox.SelectedItem.Equals(EqualOrGreater))
            {
                FloatingInput.Visible = false;
                FloatingInput.SelectedItem = Idk;
                FloatingLabel.Visible = false;
            }
            else
            {
                FloatingInput.Visible = true;
                FloatingLabel.Visible = true;
            }
        }
    }
}
